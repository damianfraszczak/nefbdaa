import {Component, Input, OnInit} from '@angular/core';

import {ViewCell} from 'ng2-smart-table';
import {truncateStringByLetters} from '../../../utils/utils';


@Component({
  template: `
    <span *ngIf="showTooltip" [nbTooltip]="fullValue" nbTooltipPlacement="top">{{renderValue}}</span>
    <span *ngIf="!showTooltip">{{renderValue}}</span>
  `,
})
export class CustomRenderComponent implements ViewCell, OnInit {

  renderValue: string;
  fullValue: string;
  showTooltip: boolean;

  @Input() value: any;
  @Input() rowData: any;

  constructor() {

  }

  ngOnInit() {
    if (this.value !== null) {
      const stringVal = String(this.value);
      const truncatedText = truncateStringByLetters(stringVal);
      if (truncatedText.length !== stringVal.length) {
        this.renderValue = truncatedText;
        this.fullValue = stringVal;
        this.showTooltip = true;
      } else {
        this.renderValue = stringVal;
      }
    } else {
      this.renderValue = '';
    }
  }

}
