import {Component, EventEmitter, Inject, Input, OnInit, Output, ViewChild} from '@angular/core';
import {
  SmartTablePagedAndFilterFormModelBased,
  SmartTablePagedAndSimplyQueryDS,
  SmartTableSimpleDS,
} from '../../../utils/service-data-source';
import {SmartTableComponent} from '../../smart-table-utilities/smart-table/smart-table.component';
import {extractContent} from '../../../utils/mappers';
import {ApiCrudService} from '../../../services/api/api-crud-service';
import {Router} from '@angular/router';
import {PagedDataWithFilterParams} from '../../../models/api/paged-data-with-filter-params';
import {getFunctionResultOrVal} from '../../dynamic-form/expression-utils';
import {MODEL_MAPPER_SERVICE} from '../../../shared-constants';
import {ModelMapperService} from '../../../services/api/model-mapper-service';
import {tap} from 'rxjs/operators';

@Component({
  selector: 'ngx-table-component',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss'],
})
export class TableComponent<T extends { id?: number }> implements OnInit {

  @Input()
  title: string;
  @Input()
  tableSettings: any;
  @Input()
  service: ApiCrudService<T>;
  @Input()
  filterType: 'form' | 'basic' | 'advanced' = 'basic';
  @Input()
  referencedProperties: Array<{ value: any | Function, field: string, type: string, operator?: string }> = null;
  @Output()
  custom = new EventEmitter();
  private tableRef: SmartTableComponent<T>;

  constructor(
    private readonly router: Router,
    @Inject(MODEL_MAPPER_SERVICE)
    private modelMapperService: ModelMapperService) {

  }

  get table(): SmartTableComponent<T> {
    return this.tableRef;
  }

  @ViewChild(SmartTableComponent, {static: false})
  set table(val: SmartTableComponent<T>) {
    this.tableRef = val;
  }

  private _dataSource: SmartTableSimpleDS<T>;

  public get dataSource() {
    return this._dataSource;
  }

  ngOnInit() {
    this.initDataSource();
    this.custom.subscribe(async event => {
      let data: any = null;
      if (event.action === 'addByLink') {
        data = {};
      } else if (event.action === 'editByLink') {
        data = event.data;
      } else if (event.action === 'delete') {
        // custom delete
        this.table.showDeleteForm(event.data);
        return;
      } else if (event.action === 'editInline') {
        this.table.editInline(event.data);
        return;
      }
      if (this.tableSettings.editModes.indexOf('PAGE') > -1 && data) {
        const queryParams = this.tableSettings.queryParamsFunction ? this.tableSettings.queryParamsFunction() : {};
        this.fillByAdditionalParams(queryParams);
        if (this.tableSettings.editFunction) {
          this.tableSettings.editFunction(this.router, data);
        } else {
          this.modelMapperService.navigateToDetails(this.tableSettings.modelType,
            data.id ? data.id : -1,
            queryParams);
        }
      }
    });
  }

  public refresh() {
    this.dataSource.refresh();
  }

  private initDataSource(): void {
    const service = this.service;
    if (this.filterType === 'form') {
      this._dataSource = new SmartTablePagedAndFilterFormModelBased(
        {
          loader: (config) => service.GetPagedWithOptionsAndFilter(this.prepareConfig(config)).pipe(extractContent()),
          adder: v => service.Post(v).pipe(extractContent(), tap(x => this.notify('ADDED', x))),
          updater: v => service.Put(v).pipe(extractContent(), tap(x => this.notify('EDITED', x))),
          remover: v => service.DeleteById(v.id).pipe(extractContent(), tap(x => this.notify('DELETED', x))),
          editor: v => service.GetWithEditData(this.getToEdit(v)).pipe(extractContent()),
          exporter: (config) => service.Export(this.prepareConfig(config)).pipe(extractContent()),
          bulkRemover: (elementsToDelete) => service.BulkDelete(elementsToDelete).pipe(extractContent(),
            tap(x => this.notify('BULK_DELETED', x))),
          bulkUpdater: (updateBy, elementsToUpdate) => service.BulkUpdate({
            updateBy,
            elementsToUpdate,
          }).pipe(extractContent(), tap(x => this.notify('BULK_EDITED', x))),
        },
      );
    } else {
      this._dataSource = new SmartTablePagedAndSimplyQueryDS(
        {
          loader: (config) =>
            service.GetPagedWithOptionsAndFilter(this.prepareConfig(config)).pipe(extractContent()),
          adder: v => service.Post(v).pipe(extractContent(), tap(x => this.notify('ADDED', x))),
          updater: v => service.Put(v).pipe(extractContent(), tap(x => this.notify('EDITED', x))),
          remover: v => service.DeleteById(v.id).pipe(extractContent(), tap(x => this.notify('DELETED', x))),
          editor: v => service.GetWithEditData(this.getToEdit(v)).pipe(extractContent()),
          exporter: (config) => service.Export(this.prepareConfig(config)).pipe(extractContent()),
          bulkRemover: (elementsToDelete) => service.BulkDelete(elementsToDelete).pipe(extractContent(),
            tap(x => this.notify('BULK_DELETED', x))),
          bulkUpdater: (updateBy, elementsToUpdate) => service.BulkUpdate({
            updateBy,
            elementsToUpdate,
          }).pipe(extractContent(),
            tap(x => this.notify('BULK_EDITED', x))),
        },
      );

    }

  }

  private prepareConfig(config: PagedDataWithFilterParams) {
    const referencedProperties = this.referencedProperties != null ?
      this.referencedProperties : this.tableSettings.referencedProperties;
    if (referencedProperties) {
      referencedProperties.forEach(referencedProperty => {
        config.rule.rules.push({
          field: referencedProperty.field,
          value: getFunctionResultOrVal(referencedProperty.value),
          operator: referencedProperty.operator ? referencedProperty.operator : '=',
          type: referencedProperty.type,
          condition: 'and',
        });
      });

    }
    const defaultRules = this.tableSettings.defaultFiltersFunction ? this.tableSettings.defaultFiltersFunction() : null;
    if (defaultRules) {
      defaultRules.forEach(rule => {
        if (config.rule.rules.filter(x => x.field.toLowerCase() === rule.field.toLowerCase()).length === 0) {
          config.rule.rules.unshift(rule);
        }

      });

    }
    return config;
  }


  private getToEdit(v: T) {
    const obj = {id: v.id, additionalParams: {}};
    this.fillByAdditionalParams(obj.additionalParams);
    return obj;
  }

  private fillByAdditionalParams(queryParams: {}) {
    if (this.tableSettings.referencedProperties) {
      this.tableSettings.referencedProperties.forEach(referencedProperty => {
        queryParams[referencedProperty.field] = getFunctionResultOrVal(referencedProperty.value);
      });
    }
    const queryFunctionResult = this.tableSettings.queryParamsFunction != null ?
      this.tableSettings.queryParamsFunction() : null;
    if (queryFunctionResult) {
      Object.assign(queryParams, queryFunctionResult);
    }
  }

  private notify(type: 'ADDED' | 'EDITED' | 'BULK_EDITED' | 'DELETED' | 'BULK_DELETED', obj: any) {
    if (this.tableSettings.dataChangedListener) {
      this.tableSettings.dataChangedListener(type, obj);
    }
  }
}
