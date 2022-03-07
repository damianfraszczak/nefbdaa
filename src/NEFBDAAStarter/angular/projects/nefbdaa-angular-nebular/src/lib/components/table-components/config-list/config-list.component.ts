import {Component, Inject, Input, OnInit} from '@angular/core';
import {ApiCrudService} from '../../../services/api/api-crud-service';
import {ConfigModel} from '../../../models/entities/config-model';
import {getDefaultTableSettings} from '../../smart-table-utilities/smart-table/smart-table-utils';
import {CONFIG_CRUD_SERVICE} from '../../../shared-constants';

@Component({
  selector: 'ngx-config',
  template: '<ngx-table-component title="Config" [service]="service" ' +
    '[tableSettings]="tableSettings"></ngx-table-component>',
})
export class ConfigListComponent implements OnInit {

  @Input()
  modelType: string;
  tableSettings;

  constructor(@Inject(CONFIG_CRUD_SERVICE) readonly service: ApiCrudService<ConfigModel>) {

  }

  ngOnInit() {
    this.tableSettings = getDefaultTableSettings(
      this.modelType,
      true,
      true,
      true);
  }

}
