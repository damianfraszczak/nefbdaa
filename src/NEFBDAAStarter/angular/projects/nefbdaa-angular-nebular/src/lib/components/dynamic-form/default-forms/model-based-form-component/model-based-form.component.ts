import {Component, ElementRef, Inject, OnInit, ViewChild} from '@angular/core';
import {BaseDynamicFormComponent} from '../base-dynamic-form.component';
import {ApiCrudService} from '../../../../services/api/api-crud-service';
import {FormModel} from '../../../../models/dynamic-form/edit-object-model';
import {DynamicFormComponent} from '../../dynamic-form/dynamic-form.component';
import {RouterHelperService} from '../../../../services/router-helper.service';
import {MODEL_MAPPER_SERVICE} from '../../../../shared-constants';
import {ModelMapperService} from '../../../../services/api/model-mapper-service';
import {fromEvent} from 'rxjs';
import {debounceTime, take} from 'rxjs/operators';
import {NbLayoutScrollService} from '@nebular/theme';

@Component({
  selector: 'ngx-model-based-form-component',
  template: `
    <ng-container *ngIf="formModel">
      <nb-card>
        <nb-card-header>{{formModel.formConfig.formMode === 'ADD' ? 'Create' : 'Edit'}} {{formModel.formConfig.title}}</nb-card-header>
      </nb-card>
      <ngx-dynamic-form #ngxForm [formModel]="formModel" [displayGlobalErrors]="debug"></ngx-dynamic-form>
      <nb-card>
        <nb-card-footer>
          <div class="d-inline-flex">
            <button (click)="onCancel()" [status]="'basic'" class="mr-2" hero nbButton>Cancel</button>

          </div>
          <div class="d-inline-flex float-right">
            <button (click)="processOkLogic(true)" [status]="'primary'"
                    class="mr-2" hero
                    nbButton
            >Save and close
            </button>
            <button (click)="processOkLogic(false)" [status]="'success'"
                    class="mr-2 " hero
                    nbButton
            >Save and and continue
            </button>
          </div>


        </nb-card-footer>
      </nb-card>
    </ng-container>
  `,
})
export class ModelBasedFormComponent extends BaseDynamicFormComponent<any> implements OnInit {
  private apiService: ApiCrudService<any>;
  public modelType: string;
  public debug = false;
  @ViewChild(DynamicFormComponent, {static: false})
  public ngxForm: DynamicFormComponent<any>;

  constructor(protected readonly routeHelper: RouterHelperService,
              @Inject(MODEL_MAPPER_SERVICE)
              protected readonly crudMapperApiService: ModelMapperService,
              private el: ElementRef,
              private scroll: NbLayoutScrollService) {
    super(routeHelper, crudMapperApiService);
  }

  ngOnInit() {
    this.routeHelper.getParamFromRoute('model_type').subscribe(model_type => {
      this.modelType = model_type;
      this.apiService = this.crudMapperApiService.getCrudServiceForModel(model_type);
      // load details from supercals
      super.ngOnInit();
    });
    this.routeHelper.getParamFromRoute('debug').subscribe(debug => {
      if (debug) {
        this.debug = true;
      }
    });
  }

  protected getService(): ApiCrudService<any> {
    return this.apiService;
  }

  protected loaded(details: FormModel<any>) {
    super.loaded(details);
    this.routeHelper.getParamsFromRoute().subscribe(params => {
      Object.keys(params).forEach(key => {
        if (details.object.hasOwnProperty(key)) {
          details.object[key] = params[key];
        }
      });
    });
  }

  processOkLogic(closePage: boolean = true) {
    this.ngxForm.markAllAsTouched();
    if (!this.ngxForm.form.invalid) {
      this.ngxForm.updateModel();
      this.save(closePage);
    } else {
      this.scrollToFirstInvalidControl();
    }
  }


  private scrollToFirstInvalidControl() {
    const firstInvalidControl: HTMLElement = this.el.nativeElement.querySelector(
      'form input.ng-invalid',
    );

    this.scroll.scrollTo(this.getTopOffset(firstInvalidControl), 0);

    fromEvent(window, 'scroll')
      .pipe(
        debounceTime(100),
        take(1),
      )
      .subscribe(() => firstInvalidControl.focus());
  }

  private getTopOffset(controlEl: HTMLElement): number {
    const labelOffset = 50;
    return controlEl.getBoundingClientRect().top + window.scrollY - labelOffset;
  }
}
