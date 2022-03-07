import {Component, Inject, OnInit, ViewChild} from '@angular/core';
import {catchError, debounceTime, distinctUntilChanged, map, skip, switchMap, tap} from 'rxjs/operators';
import {NgControl} from '@angular/forms';
import {DefaultFilter} from 'ng2-smart-table';
import {SmartTableFilterWithCellOptions} from '../../../utils/service-data-source';
import {concat, Observable, of, Subject} from 'rxjs';
import {SelectFieldOptionModel} from '../../../models/dynamic-form/select-field-option-model';
import {FieldDefinitionModel} from '../../../models/dynamic-form/field-definition-model';
import {MODEL_MAPPER_SERVICE} from '../../../shared-constants';
import {ModelMapperService} from '../../../services/api/model-mapper-service';

@Component({
  selector: 'ngx-observable-select-filter',
  template: `
    <ng-select #inputControl
               [(ngModel)]="selected"
               [items]="selectDataSource$ | async"
               [loading]="dropdownLoading"
               bindLabel="optionText"
               bindValue="optionId"
               placeholder="{{column.getFilterConfig().selectText }}"
               [ngClass]="inputClass"
               [typeahead]="searchInput"
               [typeToSearchText]="'Write to filter'"
               [multiple]="column.getFilterConfig().fieldDefinition.filterSelectMultiple"
    >

      <!--      <ng-option *ngFor="let option of selectDataSource$ | async"-->
      <!--                 [value]="option.optionId">{{option.optionText}}</ng-option>-->
    </ng-select>
  `,
  styles: [],
})
export class ObservableSelectFilterComponent extends DefaultFilter implements OnInit {

  selected: any;
  @ViewChild('inputControl', {read: NgControl, static: true}) inputControl: NgControl;
  list: Array<SmartTableFilterWithCellOptions>;
  dropdownLoading: boolean;
  selectDataSource$: Observable<Array<SelectFieldOptionModel>>;
  searchInput = new Subject<string>();

  constructor(@Inject(MODEL_MAPPER_SERVICE) private crudMapperService: ModelMapperService) {
    super();
  }

  ngOnInit() {
    const modelType = this.column.getFilterConfig().modelType;
    const fieldDefinitionModel = this.column.getFilterConfig().fieldDefinition as FieldDefinitionModel;
    const crudService = this.crudMapperService.getCrudServiceForModel(fieldDefinitionModel.modelType);
    const staticData = this.column.getFilterConfig().listProvider() as Observable<SelectFieldOptionModel[]>
    if (crudService != null) {
      this.selectDataSource$ = concat(
        crudService.Search({
            term: '',
            modelType,
            fieldName: fieldDefinitionModel.filterPropertyName
          },
        ).pipe(map(x => x.content)), // default items
        this.searchInput.pipe(
          distinctUntilChanged(),
          debounceTime(300),
          tap(() => this.dropdownLoading = true),
          switchMap(term => crudService.Search(
            {
              term,
              modelType,
              fieldName: fieldDefinitionModel.filterPropertyName
            },
          ).pipe(
            map(x => x.content),
            map(x => {
              return x.filter(y => !y.hideFromFilter);
            }),
            catchError(() => of([] as SelectFieldOptionModel[])), // empty list on error
            tap(() => this.dropdownLoading = false),
          )),
        ),
      )
      ;
    } else {
      this.selectDataSource$ = staticData;
    }


    this.inputControl.valueChanges
      .pipe(
        skip(1),
        distinctUntilChanged(),
        debounceTime(this.delay),
      )
      .subscribe((value: string) => {
        this.query = this.selected;
        this.setFilter();
      });
  }

}
