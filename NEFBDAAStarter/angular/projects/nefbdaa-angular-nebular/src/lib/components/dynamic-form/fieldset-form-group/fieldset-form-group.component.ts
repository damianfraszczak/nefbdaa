import {Component} from '@angular/core';
import {BaseFormGroupComponent} from '../base-form-group/base-form-group.component';

@Component({
  selector: 'ngx-fieldset-form-group',
  templateUrl: './fieldset-form-group.component.html',
  styleUrls: ['./fieldset-form-group.component.scss'],
})
export class FieldsetFormGroupComponent<T> extends BaseFormGroupComponent <T> {
  constructor() {
    super();
  }
}
