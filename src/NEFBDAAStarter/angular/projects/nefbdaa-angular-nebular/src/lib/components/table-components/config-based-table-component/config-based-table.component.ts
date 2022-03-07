import {Component, EventEmitter, Inject, Input, OnInit, Output, ViewChild} from '@angular/core';
import {ApiCrudService} from '../../../services/api/api-crud-service';
import {MODEL_MAPPER_SERVICE, PERMISSION_CREATE, PERMISSION_EDIT, PERMISSION_REMOVE} from '../../../shared-constants';
import {ModelMapperService} from '../../../services/api/model-mapper-service';
import {
  getDefaultTableSettingsFromConfig,
  ITableConfig,
} from '../../smart-table-utilities/smart-table/smart-table-utils';
import {TableComponent} from '../table-component/table.component';
import {AppLoggerService} from '../../../services/app-logger.service';
import {combineLatest} from 'rxjs';
import {NbAccessChecker} from '@nebular/security';

@Component({
  selector: 'ngx-config-based-table-component',
  template: `
    <ngx-table-component *ngIf='initialized' [title]='localConfig.title' [service]='service'
                         [tableSettings]='tableSettings'
                         (custom)="custom.emit($event)"
                         [referencedProperties]="localConfig.referencedProperties"
                         [filterType]="localConfig.filterType"
                         #tableComponent
    ></ngx-table-component>

  `,
})
export class ConfigBasedTableComponent implements OnInit {

  service: ApiCrudService<any>;
  tableSettings: any;
  initialized = false;
  localConfig: ITableConfig;
  @Output()
  custom = new EventEmitter();
  private tableComponentRef: TableComponent<any>;

  constructor(@Inject(MODEL_MAPPER_SERVICE)
              private readonly crudMapperApiService: ModelMapperService,
              private readonly logger: AppLoggerService,
              private readonly accessChecker: NbAccessChecker,
  ) {
  }

  get tableComponent(): TableComponent<any> {
    return this.tableComponentRef;
  }

  @ViewChild(TableComponent, {static: false})
  set tableComponent(val: TableComponent<any>) {
    this.tableComponentRef = val;
  }

  @Input()
  set tableConfig(config: ITableConfig,
  ) {
    this.localConfig = config;
    this.service = this.crudMapperApiService.getCrudServiceForModel(config.modelType);
    if (this.service == null) {
      this.logger.error(`Service for model type ${config.modelType} doesn't exists`);
    }
    combineLatest(
      this.accessChecker.isGranted(PERMISSION_CREATE, config.modelType),
      this.accessChecker.isGranted(PERMISSION_EDIT, config.modelType),
      this.accessChecker.isGranted(PERMISSION_REMOVE, config.modelType),
    )
      .subscribe(([addPermission, editPermission, deletePermisssion]) => {
        config.addButtonEnabled = config.addButtonEnabled != null ?
          config.addButtonEnabled && addPermission : addPermission;
        config.editButtonEnabled = config.editButtonEnabled != null ?
          config.editButtonEnabled && editPermission : editPermission;
        config.deleteButtonEnabled = config.deleteButtonEnabled != null ?
          config.deleteButtonEnabled && deletePermisssion : deletePermisssion;
        this.tableSettings = getDefaultTableSettingsFromConfig(config);
        this.initialized = true;
      });

  }

  public get dataSource() {
    if (!this.tableComponent) {
      return null;
    }
    return this.tableComponent.dataSource;
  }

  ngOnInit() {

  }

  public refresh() {
    this.tableComponent.refresh();
  }

}
