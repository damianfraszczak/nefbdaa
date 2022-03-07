import {Component} from '@angular/core';
import {BaseFormGroupComponent} from '../base-form-group/base-form-group.component';

@Component({
  selector: 'ngx-accordion-form-group',
  templateUrl: './accordion-form-group.component.html',
  styleUrls: ['./accordion-form-group.component.scss'],
})
export class AccordionFormGroupComponent<T> extends BaseFormGroupComponent <T> {
  constructor() {
    super();
  }
}
