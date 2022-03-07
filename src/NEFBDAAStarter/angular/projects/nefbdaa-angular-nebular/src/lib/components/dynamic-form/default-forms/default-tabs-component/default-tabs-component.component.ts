import {Component, Input, OnInit} from '@angular/core';
import {BaseDynamicFormComponent} from '../base-dynamic-form.component';
import {ApiCrudService} from '../../../../services/api/api-crud-service';

@Component({
  selector: 'ngx-default-tabs-component',
  templateUrl: './default-tabs-component.component.html',
  styleUrls: ['./default-tabs-component.component.scss'],
})
export class DefaultTabsComponentComponent extends BaseDynamicFormComponent<any> implements OnInit {

  @Input()
  modelType: string;
  @Input()
  detailsTabTitle = 'Details';

  ngOnInit() {
    super.ngOnInit();
  }

  protected getService(): ApiCrudService<any> {

    return this.crudMapperApiService.getCrudServiceForModel(this.modelType);
  }

}
