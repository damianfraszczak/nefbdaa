import {Component} from '@angular/core';
import {BaseFormGroupComponent} from '../base-form-group/base-form-group.component';

@Component({
  selector: 'ngx-card-form-group',
  templateUrl: './card-form-group.component.html',
  styleUrls: ['./card-form-group.component.scss'],
})
export class CardFormGroupComponent<T> extends BaseFormGroupComponent <T> {
  constructor() {
    super();
  }
}
