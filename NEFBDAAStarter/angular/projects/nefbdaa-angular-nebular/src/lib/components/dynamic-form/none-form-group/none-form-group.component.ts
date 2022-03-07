import {Component} from '@angular/core';
import {BaseFormGroupComponent} from '../base-form-group/base-form-group.component';

@Component({
  selector: 'ngx-none-form-group',
  templateUrl: './none-form-group.component.html',
  styleUrls: ['./none-form-group.component.scss'],
})
export class NoneFormGroupComponent<T> extends BaseFormGroupComponent <T> {
  constructor() {
    super();
  }
}
