import {Component, OnInit} from '@angular/core';
import {NbLoginComponent} from '@nebular/auth';
import {getParamValueQueryString} from '../../../../projects/nefbdaa-angular-nebular/src/lib';

@Component({
  selector: 'ngx-login',
  templateUrl: './login.component.html',
})
export class NgxLoginComponent extends NbLoginComponent implements OnInit {
  public phoneNumberCode: number;
  public phoneNumber: number;

  ngOnInit() {
    this.service.onTokenChange().subscribe((token) => {
      if (token.isValid()) {
        const returnUrl = getParamValueQueryString('returnUrl') || '/pages/examinations';
        // TODO jak to lepie zrobic
        setTimeout(() => { this.router.navigate([returnUrl]); }, 1200);
      }
    });
  }

  onChange(event: any): void {
    if (this.phoneNumberCode && this.phoneNumber) {
      this.user.loginName = String(this.phoneNumberCode) + String(this.phoneNumber);
    }
  }

  errorsOccurred(): boolean {
    return this.showMessages.error && this.errors && (this.errors.length) > 0 && (!this.submitted);
  }

  loginSucceeded(): boolean {
    return this.showMessages.success && this.messages && this.messages.length > 0 && !this.submitted;
  }

}
