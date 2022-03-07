import {Component} from '@angular/core';
import {BaseFormGroupComponent} from '../base-form-group/base-form-group.component';

@Component({
  selector: 'ngx-simple-form-group',
  templateUrl: './simple-form-group.component.html',
  styleUrls: ['./simple-form-group.component.scss'],
})
export class SimpleFormGroupComponent<T> extends BaseFormGroupComponent <T> {
  constructor() {
    super();
  }
}
