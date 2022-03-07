import {Component, ElementRef, Input, OnInit} from '@angular/core';
import {FormArray, FormGroup, ValidationErrors} from '@angular/forms';
import {getFormErrors} from '../dynamic-form-utils';

interface AllValidationErrors {
  controlName: string;
  errorName: string;
  errorValue: any;
}

@Component({
  selector: 'ngx-form-errors',
  templateUrl: './form-errors.component.html',
  styleUrls: ['./form-errors.component.scss'],
})
export class FormErrorsComponent implements OnInit {

  @Input() form: FormGroup;
  @Input() formRef: HTMLFormElement;
  @Input() messages: Array<any>;

  public errors: AllValidationErrors[];

  constructor() {
    this.errors = [];
    this.messages = [];
  }

  ngOnInit() {
    this.form.valueChanges.subscribe(() => {
      this.errors = [];
      this.calculateErrors(this.form);
    });

    this.calculateErrors(this.form);
  }

  calculateErrors(form: FormGroup | FormArray) {
    Object.keys(form.controls).forEach(field => {
      const control = form.get(field);
      if (control instanceof FormGroup || control instanceof FormArray) {
        this.errors = this.errors.concat(this.calculateErrors(control));
        return;
      }

      const controlErrors: ValidationErrors = control.errors;
      if (controlErrors !== null) {
        Object.keys(controlErrors).forEach(keyError => {
          this.errors.push({
            controlName: field,
            errorName: keyError,
            errorValue: controlErrors[keyError],
          });
        });
      }
    });

    // This removes duplicates
    this.errors = this.errors.filter((error, index, self) => self.findIndex(t => {
      return t.controlName === error.controlName && t.errorName === error.errorName;
    }) === index);
    return this.errors;
  }

  getErrorMessage(error) {
    return getFormErrors(this.form.get(error.controlName));
  }
}
