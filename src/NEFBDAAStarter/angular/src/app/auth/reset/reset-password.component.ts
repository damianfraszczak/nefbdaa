import {Component} from '@angular/core';
import {NbRequestPasswordComponent} from '@nebular/auth';

@Component({
  selector: 'ngx-request-password-page',
  templateUrl: './reset-password.component.html',
})
export class NgxResetPasswordComponent extends NbRequestPasswordComponent {
  public email: string;

  public onChange(event: any): void {
    if (this.email) {
      this.user.loginName = this.email;
    }
  }
}
