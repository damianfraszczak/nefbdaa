import {Injectable} from '@angular/core';
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs';
import {NbAuthService, NbAuthToken, NbTokenService} from '@nebular/auth';
import {Router} from '@angular/router';
import {tap} from 'rxjs/operators';
import {constants} from '../../../shared-constants';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  private token: NbAuthToken;

  constructor(private readonly tokenService: NbTokenService,
              private readonly authService: NbAuthService,
              private readonly router: Router) {
    this.tokenService.get().subscribe((t: NbAuthToken) => this.token = t);
    this.tokenService.tokenChange().subscribe((t: NbAuthToken) => this.token = t);
  }

  // TODO
  // verify https://github.com/akveo/nebular/blob/master/src/framework/auth/services/interceptors/jwt-interceptor.ts#L31
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const url = req.url;

    if (this.token) {
      req = req.clone({
        setHeaders: {Authorization: `Bearer ${this.token}`},
        url,
      });
    }
    return next.handle(req).pipe(
      tap(event => {
      }, error => {
        // logout only when auth exception
        if (error instanceof HttpErrorResponse && error.status === 401) {
          this.authService.logout(constants.securityStrategyName);
          this.tokenService.clear();
          this.router.navigateByUrl('auth/login');
        }
      }),
    );
  }
}
