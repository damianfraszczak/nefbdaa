import {ChangeDetectorRef, Component, Inject, OnInit} from '@angular/core';
import {NB_AUTH_OPTIONS, NbAuthService, NbRegisterComponent} from '@nebular/auth';
import {Router} from '@angular/router';

@Component({
  selector: 'ngx-register',
  templateUrl: './register.component.html',
})
export class NgxRegisterComponent extends NbRegisterComponent implements OnInit {

  public phoneNumberCode: number;
  public phoneNumber: number;

  constructor(
              protected readonly service: NbAuthService,
              @Inject(NB_AUTH_OPTIONS) protected readonly options = {},
              protected cd: ChangeDetectorRef,
              protected readonly router: Router) {
    super(service, options, cd, router);
  }

  ngOnInit(): void {

  }



  public onChange(event: any): void {
    if (this.phoneNumber && this.phoneNumberCode) {
      this.user.phoneNumber = String(this.phoneNumberCode) + String(this.phoneNumber);
    }
  }
}
