import {LocalDataSource} from 'ng2-smart-table';
import {BehaviorSubject, Observable} from 'rxjs';
import {deepExtend} from '@nebular/auth';

import {debounceTime, distinctUntilChanged, map, tap} from 'rxjs/operators';
import {QueryBuilderConfig} from 'angular2-query-builder';
import {
  buildQueryFieldsConfig,
  getDefaultQueryFilterRule,
  getFieldDefinition,
  getFieldNameForFilter,
  getQueryOperatorTypeForField,
  getQueryTypeForField,
  getQueryValue,
} from './query-builder';
import {FieldDefinitionModel} from '../models/dynamic-form/field-definition-model';
import {OptionsFieldDefinitionModel} from '../models/dynamic-form/options-field-definition-model';
import {SelectFieldOptionModel} from '../models/dynamic-form/select-field-option-model';
import {PagedList} from '../models/api/paged-list';
import {PagedListWithOptions} from '../models/api/paged-list-with-options';
import {FormModel} from '../models/dynamic-form/edit-object-model';
import {getOptionsForField} from './field-definitions-utils';
import {DocumentModel} from '../models/entities/document-model';
import {ExportDataParams} from '../models/api/export-data-params';
import {PagedDataWithFilterParams} from '../models/api/paged-data-with-filter-params';
import {FilterRuleModel} from '../models/dynamic-form/filter-rule-model';
import {PagedDataParams} from '../models/api/paged-data-params';
import {InputType} from '../models/dynamic-form/input-type-model';


export interface IDataSourceDataProvider<T> {
  adder?: (value: T) => Observable<T>;
  updater?: (value: T) => Observable<T>;
  bulkUpdater?: (value: T, toUpdate: Array<number>) => Observable<Array<T>>;
  remover?: (value: T) => Observable<T>;
  bulkRemover?: (value: Array<number>) => Observable<Array<T>>;
  editor?: (value: T) => Observable<FormModel<T>>;
  exporter?: (config: ExportDataParams) => Observable<DocumentModel>;
}

export interface ISimpleDSProvider<T> extends IDataSourceDataProvider<T> {
  loader: () => Observable<Array<T>>;
}

export interface IDataSourcePagedDataAndQueryDataProvider<T>
  extends IDataSourceDataProvider<T> {
  loader: (config: PagedDataWithFilterParams) => Observable<PagedListWithOptions<T>>;

}

export interface IDataSourcePagedDataAndAdvancedQueryDataProvider<T>
  extends IDataSourceDataProvider<T> {
  loader: (config: PagedDataWithFilterParams) => Observable<PagedList<T>>;
}

export interface SmartTableFilterWithCellOptions {
  optionText: string;
  optionId: string;
  // smarttable filters
  value: string;
  title: string;
  hideFromFilter: boolean;
  hideFromEdit: boolean;
}

export abstract class BaseDataSource<T> extends LocalDataSource {
  public loading: boolean;
  public loadingSubject$ = new BehaviorSubject<boolean>(true);

  protected constructor(public provider: IDataSourceDataProvider<T>) {
    super();
    this.loadingSubject$.asObservable().pipe(
      debounceTime(100),
      distinctUntilChanged())
      .subscribe(loading => this.loading = loading);
  }


  prepend(element: any): Promise<any> {
    if (!this.provider.adder) {
      return super.prepend(element);
    }
    return this.provider.adder(element).toPromise()
      .then(v => super.prepend(v));
  }

  append(element: any): Promise<any> {
    if (!this.provider.adder) {
      return super.append(element);
    }
    return this.provider.adder(element).toPromise()
      .then(v => super.append(v));
  }

  add(element: any): Promise<any> {
    if (!this.provider.adder) {
      return super.add(element);
    }
    return this.provider.adder(element).toPromise()
      .then(v => super.add(v));
  }

  update(element: any, values: any): Promise<any> {
    if (!this.provider.updater) {
      return super.update(element, values);
    }
    return this.find(element)
      .then(found => deepExtend(found, values))
      .then(extended => this.provider.updater(extended).toPromise())
      // .then(saved => super.update(element, saved))
      .then(saved => this.refresh());
  }

  remove(element: any): Promise<any> {
    if (!this.provider.remover) {
      return super.remove(element);
    }
    return this.provider.remover(element).toPromise()
      .then(v => super.remove(element));
  }


  isEmpty(): boolean {
    return this.data && this.data.length === 0;
  }

  isExportable(): boolean {
    return this.provider != null && this.provider.exporter != null;
  }

  bulkDelete(toDelete: Array<T>) {
    this.provider.bulkRemover(toDelete.map(x => x['id'])).subscribe(() => this.refresh());
  }

  bulkUpdate(updateBy: T, toUpdate: Array<T>) {
    this.provider.bulkUpdater(updateBy, toUpdate.map(x => x['id'])).subscribe(() => this.refresh());
  }

  getLoadedData(): Array<T> {
    return this.data;
  }

  find(element: any): Promise<any> {
    let found = this.data.find(el => el === element);
    if (found) {
      return Promise.resolve(found);
    }
    found = this.data.find(x => x['id'] === element['id']);
    if (found) {
      return Promise.resolve(found);
    }
    return Promise.reject(new Error('Element was not found in the dataset'));
  }

  // private customFind(element: any): Promise<T> {
  //   let found = this.data.find(x => x['id'] === element['id']);
  //   if (found != null) {
  //     return of(found).toPromise()
  //   } else {
  //     return super.find(element);
  //   }
  //
  // }
}

// used to local data with cell options
export abstract class SmartTableDS<T> extends BaseDataSource<T> {

  private _cellOptionsByField = new Map<string, BehaviorSubject<Array<SmartTableFilterWithCellOptions>>>();


  protected constructor(
    public readonly provider: IDataSourceDataProvider<T>,
    public cellOptions: Array<OptionsFieldDefinitionModel>,
    public fieldsDefinitions: FieldDefinitionModel[] = [],
  ) {
    super(provider);
    this.reloadCellOptions(cellOptions);

  }

  public getOptionsForField(field: FieldDefinitionModel): Observable<Array<SmartTableFilterWithCellOptions>> {
    const name = field.parameterName;
    let options = this._cellOptionsByField[name];
    if (options == null) {
      const rawOptions = getOptionsForField(field, this.cellOptions);
      this.reloadOptionsForField(field.parameterName, rawOptions);
      options = this._cellOptionsByField[name];
    }
    return options.asObservable();
  }

  public reloadCellOptions(cellOptions: Array<OptionsFieldDefinitionModel>) {
    if (cellOptions != null) {
      cellOptions.forEach(x => {
        let currentArray = this._cellOptionsByField[x.parameterName];
        if (currentArray == null) {
          currentArray = new BehaviorSubject<Array<SmartTableFilterWithCellOptions>>([]);
          this._cellOptionsByField[x.parameterName] = currentArray;
        }
        const newVal = [];
        x.options.forEach(option => newVal.push({
          value: option.optionId,
          optionId: option.optionId,
          title: option.optionText,
          optionText: option.optionText,
          hideFromFilter: option.hideFromFilter,
          hideFromEdit: option.hideFromEdit,
        }));
        currentArray.next(newVal);
      });
    }

    this.cellOptions = cellOptions;
  }

  private reloadOptionsForField(name: string, cellOptions: Array<SelectFieldOptionModel>) {

    let subject: BehaviorSubject<Array<SmartTableFilterWithCellOptions>> = this._cellOptionsByField[name];
    if (subject == null) {
      subject = new BehaviorSubject<Array<SmartTableFilterWithCellOptions>>([]);
    }
    const newVal = [];
    if (cellOptions != null) {
      cellOptions.forEach(option => newVal.push({
        value: option.optionId,
        optionId: option.optionId,
        title: option.optionText,
        optionText: option.optionText,
        hideFromFilter: option.hideFromFilter,
        hideFromEdit: option.hideFromEdit,
      }));
    }

    subject.next(newVal);
    this._cellOptionsByField[name] = subject;
  }

}

// used to local data and all loaded
export class SmartTableSimpleDS<T> extends SmartTableDS<T> {


  constructor(provider: ISimpleDSProvider<T>,
              cellOptions: Array<OptionsFieldDefinitionModel>) {
    super(provider, cellOptions);

  }


  getElements(): Promise<any> {
    this.loadingSubject$.next(true);
    return (this.provider as ISimpleDSProvider<T>)
      .loader().pipe(
        tap(() => this.loadingSubject$.next(false)),
        tap((result) => this.data = result),
      )
      .toPromise();
  }
}

abstract class FilterRuleBasesSmartTable<T> extends SmartTableDS<T> {
  lastRequestCount = 0;

  protected constructor(provider: IDataSourcePagedDataAndQueryDataProvider<T>) {
    super(provider, []);
  }

  count(): number {
    return this.lastRequestCount;
  }

  exportData(type: string, toExport: Array<T>, tableSettings: any) {
    const config = this.getCurrentQueryConfig();
    config.pagingParams = {page: {index: 0, size: Number.MAX_SAFE_INTEGER}, order: {}};
    this.provider.exporter({
      type,
      rule: config.rule,
      selectedElements: toExport.map(x => x['id']),
      exportableFields: tableSettings.exportableColumns,
    })
      .subscribe(document => {
        window.open(document.filePath, '_blank');
      });
  }

  getElements(): Promise<any> {
    this.loadingSubject$.next(true);
    return (this.provider as IDataSourcePagedDataAndQueryDataProvider<T>).loader(this.getCurrentQueryConfig())
      .pipe(
        map((res: PagedListWithOptions<T>) => {
          this.lastRequestCount = res.result.count;
          this.data = res.result.list;
          this.reloadCellOptions(res.options);
          this.loadingSubject$.next(false);
          return this.data;
        })).toPromise();
  }

  protected abstract getCurrentQueryConfig(): PagedDataWithFilterParams;
}


export class SmartTablePagedAndFilterFormModelBased<T> extends FilterRuleBasesSmartTable<T> {
  public config: FilterRuleModel;

  constructor(provider: IDataSourcePagedDataAndQueryDataProvider<T>) {
    super(provider);
  }

  protected getCurrentQueryConfig(): PagedDataWithFilterParams {
    const queryConfig: PagedDataWithFilterParams = {
      pagingParams: {
        page: {
          index: this.pagingConf['page'],
          size: this.pagingConf['perPage'],
        },
        order: {},
      },
      rule: {rules: [], condition: 'and'},
    };
    // ordering
    if (this.sortConf && this.sortConf.length > 0) {
      if (queryConfig.pagingParams == null) {
        queryConfig.pagingParams = new PagedDataParams();
        queryConfig.pagingParams.order = {};
      }
      queryConfig.pagingParams.order.property = this.sortConf[0].field;
      queryConfig.pagingParams.order.isAscending = this.sortConf[0].direction.toUpperCase() === 'ASC';
    }
    if (this.config == null) {
      this.config = getDefaultQueryFilterRule();
    }
    queryConfig.rule = this.config;
    return queryConfig;
  }


}

export class SmartTablePagedAndAdvancedQueryDS<T>
  extends SmartTableDS<T> {
  public query = {
    condition: 'and',
    rules: [],
  };
  public config: QueryBuilderConfig = {
    fields: {},
  };
  lastRequestCount = 0;

  constructor(provider: IDataSourcePagedDataAndAdvancedQueryDataProvider<T>) {
    super(provider, [], []);
    this.initQueryConfig();

  }

  count(): number {
    return this.lastRequestCount;
  }

  getElements(): Promise<any> {
    // this.loadingSubject$.next(true);
    // return this.provider.cellOptionsLoader().pipe(
    //   // firstly load cell options
    //   map(cellOptions => {
    //     const queryConfig: PagedDataWithFilterParams = {
    //       rule: buildServerQuery(this.query, {fieldsDefinitions: this.fieldsDefinitions, cellOptions}),
    //       pageIndex: this.pagingConf['page'],
    //       pageSize: this.pagingConf['perPage']
    //     };
    //     // ordering
    //     if (this.sortConf && this.sortConf.length > 0) {
    //       queryConfig.orderProperty = this.sortConf[0].field;
    //       queryConfig.orderIsAscending = this.sortConf[0].direction.toUpperCase() === 'ASC';
    //     }
    //     return queryConfig;
    //   }),
    //   // bas
    //   map(queryConfig => {
    //     return (this.provider as IDataSourcePagedDataAndQueryDataProvider<T>).loader(queryConfig)
    //       .pipe(
    //         map((res: PagedListResponse<T>) => {
    //           this.lastRequestCount = res.count;
    //           this.data = res.bids-list;
    //           this.initQueryConfig();
    //           return this.data;
    //         }),
    //         tap(() => this.loadingSubject$ = false));
    //   })
    // ).toPromise();
    return null;
  }

  private initQueryConfig() {
    if (this.config == null) {
      this.config = buildQueryFieldsConfig({cellOptions: this.cellOptions, fieldsDefinitions: this.fieldsDefinitions});
    }
  }

}

export class SmartTablePagedAndSimplyQueryDS<T>
  extends FilterRuleBasesSmartTable<T> {

  constructor(provider: IDataSourcePagedDataAndQueryDataProvider<T>) {
    super(provider);
  }

  protected getCurrentQueryConfig(): PagedDataWithFilterParams {
    const queryConfig: PagedDataWithFilterParams = {
      pagingParams: {
        page: {
          index: this.pagingConf['page'],
          size: this.pagingConf['perPage'],
        },
        order: {},
      },
      rule: {rules: [], condition: 'and'},
    };
    // ordering
    if (this.sortConf && this.sortConf.length > 0) {
      if (queryConfig.pagingParams == null) {
        queryConfig.pagingParams = {order: {}};
      }
      queryConfig.pagingParams.order.property = this.sortConf[0].field;
      queryConfig.pagingParams.order.isAscending = this.sortConf[0].direction.toUpperCase() === 'ASC';
    }
    // default smart table filtering
    // TODO
    if (this.filterConf.filters) {
      this.filterConf.filters.forEach((fieldConf) => {
        if (fieldConf['search']) {
          const fieldDefinition = getFieldDefinition(this.fieldsDefinitions, fieldConf['field']);
          const currentValue = fieldConf['search'];

          if (fieldDefinition.inputType === InputType.DATE && Array.isArray(currentValue)) {
            // range selection
            for (let i = 0; i < currentValue.length; i++) {
              const val = currentValue[i];
              if (val) {
                queryConfig.rule.rules.push({
                  field: getFieldNameForFilter(this.fieldsDefinitions, fieldConf['field']),
                  value: getQueryValue(val, fieldDefinition),
                  operator: i === 0 ? '>=' : '<=',
                  type: getQueryTypeForField(this.fieldsDefinitions, fieldConf['field']),
                  condition: 'and',
                });
              }

            }
          } else {
            const currentVal = getQueryValue(currentValue, fieldDefinition);
            if (!(Array.isArray(currentVal) && currentVal.length === 0)) {
              queryConfig.rule.rules.push({
                field: getFieldNameForFilter(this.fieldsDefinitions, fieldConf['field']),
                value: currentVal,
                operator: getQueryOperatorTypeForField(this.fieldsDefinitions, fieldConf['field']),
                type: getQueryTypeForField(this.fieldsDefinitions, fieldConf['field']),
                condition: 'and',
              });
            }

          }

        }
      });
    }
    return queryConfig;
  }

}

