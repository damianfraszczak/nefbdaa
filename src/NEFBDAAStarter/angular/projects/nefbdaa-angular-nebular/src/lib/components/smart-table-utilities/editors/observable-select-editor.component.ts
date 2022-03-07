import {Component, Inject, OnInit, ViewChild} from '@angular/core';
import {DefaultEditor} from 'ng2-smart-table';
import {Observable, Subject} from 'rxjs';
import {SelectFieldOptionModel} from '../../../models/dynamic-form/select-field-option-model';
import {MODEL_MAPPER_SERVICE} from '../../../shared-constants';
import {ModelMapperService} from '../../../services/api/model-mapper-service';
import {FieldDefinitionModel} from '../../../models/dynamic-form/field-definition-model';
import {filter, map, tap} from 'rxjs/operators';
import {getOrDefault} from '../../../utils/utils';
import {extractContent} from '../../../utils/mappers';

@Component({
  selector: 'ngx-table-observable-select-editor',
  template: `
    <ng-select #inputControl
               [name]='cell.getId()'
               [(ngModel)]="selected"
               [readonly]='!cell.isEditable()'
               [loading]='dropdownLoading'
               [typeToSearchText]="'Write to filter'"
               (click)='onClick.emit($event)'
               [dropdownPosition]="'auto'"
               appendTo="body"
               bindLabel='optionText'
               bindValue='optionId'
               [ngClass]='inputClass'
               [items]="selectDataSource$ | async"
               (keydown.enter)='onEdited.emit($event)'
               (keydown.esc)='onStopEditing.emit();'
               (change)="this.cell.newValue = this.selected">

      >

      <!-- <ng-option *ngFor='let option of selectDataSource$ | async'
                  [value]='option.optionId'>{{option.optionText}}</ng-option>-->
    </ng-select>
  `,
  styles: [],
})
export class ObservableSelectEditorComponent extends DefaultEditor implements OnInit {
  selected: any;
  dropdownLoading: boolean;
  selectDataSource$: Observable<SelectFieldOptionModel[]>;
  searchInput = new Subject<string>();


  constructor(@Inject(MODEL_MAPPER_SERVICE) private crudMapperService: ModelMapperService) {
    super();
  }

  ngOnInit() {
    const fieldDefinitionModel = this.cell.getColumn().getConfig().fieldDefinition as FieldDefinitionModel;
    const modelType = this.cell.getColumn().getConfig().modelType;
    const crudService = this.crudMapperService.getCrudServiceForModel(fieldDefinitionModel.modelType);
    const staticData = this.cell.getColumn().getConfig().listProvider() as Observable<SelectFieldOptionModel[]>;
    // options values are stored as strings
    // cant edit multiple
    this.selected = getOrDefault(this.cell.newValue, '') + '';
    this.selectDataSource$ = this.cell.getColumn().getConfig().listProvider();
    this.selectDataSource$ = staticData;
    if (crudService != null) {
      // this.selectDataSource$ = concat(
      //   crudService.Search('').pipe(
      //     map(x => x.content),
      //   ), // default items
      //   this.searchInput.pipe(
      //     distinctUntilChanged(),
      //     debounceTime(300),
      //     tap(() => this.dropdownLoading = true),
      //     switchMap(term => crudService.Search(term).pipe(
      //       map(x => x.content),
      //       catchError(() => of([] as SelectFieldOptionModel[])), // empty list on error
      //       tap(() => this.dropdownLoading = false),
      //     )),
      //   ),
      // );
      this.selectDataSource$ = crudService
        .GetOptionsForEdit({
          editedObject: this.cell.getRow().getData(),
          fieldName: fieldDefinitionModel.parameterName,
          modelType: modelType,

        }).pipe(
          extractContent(),
          map(x => {
            return x.filter(y => !y.hideFromEdit);
          }),
          tap(() => this.dropdownLoading = false));
    }

  }

}
