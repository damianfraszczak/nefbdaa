import {Inject, OnInit} from '@angular/core';
import {FormModel} from '../../../models/dynamic-form/edit-object-model';
import {debounceTime, distinctUntilChanged, flatMap, withLatestFrom} from 'rxjs/operators';
import {extractContent} from '../../../utils/mappers';
import {ApiCrudService} from '../../../services/api/api-crud-service';
import {Observable} from 'rxjs';
import {RouterHelperService} from '../../../services/router-helper.service';
import {ActivatedRoute} from '@angular/router';
import {getAllParamsFromRoute, getParamFromRoute} from '../../../utils/utils';
import {FormMode} from '../../../models/dynamic-form/dynamic-field-enums';
import {MODEL_MAPPER_SERVICE} from '../../../shared-constants';
import {ModelMapperService} from '../../../services/api/model-mapper-service';

export abstract class BaseDynamicFormComponent<T> implements OnInit {

  public formModel: FormModel<T>;

  public constructor(
    protected readonly routeHelper: RouterHelperService,
    @Inject(MODEL_MAPPER_SERVICE)
    protected readonly crudMapperApiService: ModelMapperService,
  ) {
  }

  ngOnInit() {
    const subscription = this.getIdToLoad()
      .pipe(
        distinctUntilChanged(),
        debounceTime(100),
        withLatestFrom(this.routeHelper.getParamsFromRoute()),
      ).pipe(
        distinctUntilChanged(),
        flatMap(([id, queryParams]) => {
          return this.getService().GetWithEditData({id: id, additionalParams: queryParams});
        }),
        extractContent(),
      ).subscribe(details => {
        this.loaded(details);
        subscription.unsubscribe();
      });

  }

  public save(closePage: boolean = true): void {
    this.getService().AddOrUpdate(this.formModel.object)
      .pipe(extractContent())
      .subscribe(x => {
        this.formModel.object = x;
        this.afterSuccess(x, this.shouldClose(closePage, x));
      });
  }

  public onCancel() {
    this.goToLastPageOrDefault();
  }

  public goToLastPageOrDefault() {
    this.routeHelper.goToLastPageOrDefault();
  }

  protected abstract getService(): ApiCrudService<T>;

  protected afterSuccess(x: T, closeCurrentPage: boolean = true) {
    if (closeCurrentPage) {
      this.goToLastPageOrDefault();
    } else {
      if (this.formModel.formConfig.formMode !== FormMode.EDIT) {
        this.crudMapperApiService.navigateToDetails(this.formModel.formConfig.modelType,
          this.formModel.object['id'],
          {});
      }

    }

  }

  protected shouldClose(shouldCloseFromForm: boolean, value: T) {
    return shouldCloseFromForm;
  }

  protected getIdToLoad(): Observable<any> {
    return this.routeHelper.getParamFromRoute('id');
  }

  protected loaded(details: FormModel<T>) {
    this.formModel = null;
    setTimeout(() => this.formModel = details, 100);
  }
}
