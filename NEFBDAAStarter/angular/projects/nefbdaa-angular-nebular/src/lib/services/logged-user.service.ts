import {Inject, Injectable} from '@angular/core';
import {NbAuthService} from '@nebular/auth';
import {BehaviorSubject, Observable} from 'rxjs';
import {extractContent} from '../utils/mappers';
import {LoggedUserModel} from '../models/entities/logged-user-model';
import {LoggedUserApiService} from './api/logged-user-api-service';
import {LOGGED_USER_SERVICE} from '../shared-constants';
import {distinctUntilChanged} from "rxjs/operators";

@Injectable({
  providedIn: 'root',
})
export class LoggedUserService {

  private loggedUser = new BehaviorSubject<LoggedUserModel>(null);

  constructor(
    private authService: NbAuthService,
    @Inject(LOGGED_USER_SERVICE)
    private usersService: LoggedUserApiService,
  ) {
    this.init();
  }

  public getLoggedUser(): Observable<LoggedUserModel> {
    return this.loggedUser.asObservable();
  }

  private init(): void {
    this.authService.onAuthenticationChange().pipe(distinctUntilChanged()).subscribe(isAuth => {
      if (isAuth) {
        this.usersService.GetMyAccount().pipe(extractContent()).subscribe(user => {
          this.loggedUser.next(user);
        });
      } else {
        this.loggedUser.next(null);
      }
    });
  }
}
