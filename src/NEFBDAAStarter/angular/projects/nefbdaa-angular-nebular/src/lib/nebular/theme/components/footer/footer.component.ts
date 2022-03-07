import {Component, Input} from '@angular/core';

@Component({
  selector: 'ngx-footer',
  styleUrls: ['./footer.component.scss'],
  template: `
    <span class="created-by">Copyrights by <b>{{companyName}}</b> {{date | date:'yyyy'}}</span>
    <span>Designed by <b>{{companyName}}</b></span>

  `,
})
export class FooterComponent {
  date = new Date();
  @Input() companyName = 'Digital Trading LTD';
}
