import {Directive, Input, OnChanges, SimpleChanges} from '@angular/core';
import {AbstractControl, NG_VALIDATORS, Validator} from '@angular/forms';

@Directive({
  selector: '[ngxRequiredIf]',
  providers: [
    { provide: NG_VALIDATORS, useExisting: RequiredIfDirective, multi: true },
  ],
})
export class RequiredIfDirective implements Validator, OnChanges {
  @Input('ngxRequiredIf')
  requiredIf: boolean;

  private _onChange: () => void;

  constructor() { }
  validate(c: AbstractControl) {

    const value = c.value;
    if ((value === null || value === undefined || value === '') && this.requiredIf) {
      return {
        requiredIf: {condition: this.requiredIf},
      };
    }
    return null;
  }


  registerOnValidatorChange(fn: () => void): void { this._onChange = fn; }

  ngOnChanges(changes: SimpleChanges): void {
    if ('ngxRequiredIf' in changes) {
      if (this._onChange) {
        this._onChange();
      }

    }
  }
}
