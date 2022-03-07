import {Component} from '@angular/core';
import {BaseFormGroupComponent} from '../base-form-group/base-form-group.component';

@Component({
  selector: 'ngx-tabs-form-group',
  templateUrl: './tabs-form-group.component.html',
  styleUrls: ['./tabs-form-group.component.scss'],
})
export class TabsFormGroupComponent<T> extends BaseFormGroupComponent <T> {
  constructor() {
    super();
  }
}
