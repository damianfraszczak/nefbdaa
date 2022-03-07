import {Component, Inject, OnInit} from '@angular/core';
import {MODEL_MAPPER_SERVICE} from '../../../shared-constants';
import {ModelMapperService} from '../../../services/api/model-mapper-service';
import {ITableConfig} from '../../smart-table-utilities/smart-table/smart-table-utils';
import {ActivatedRoute} from '@angular/router';
import {getParamFromRoute} from '../../../utils/utils';

@Component({
  selector: 'ngx-default-table-component',
  template: `
    <ngx-config-based-table-component *ngIf='tableConfig'
                                      [tableConfig]='tableConfig'></ngx-config-based-table-component>
  `,
})
export class DefaultTableComponentComponent implements OnInit {
  tableConfig: ITableConfig;

  constructor(@Inject(MODEL_MAPPER_SERVICE)
              private readonly crudMapperApiService: ModelMapperService,
              private readonly route: ActivatedRoute,
  ) {
  }

  ngOnInit() {
    getParamFromRoute(this.route, 'model_type')
      .subscribe((modelType) => {
        this.tableConfig = {
          // TODO title
          title: modelType,
          modelType: modelType,
          filterType: 'basic',
        };

      });

  }

}
