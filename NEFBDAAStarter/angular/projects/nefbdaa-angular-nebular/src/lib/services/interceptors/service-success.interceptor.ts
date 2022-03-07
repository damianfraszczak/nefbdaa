import {ErrorHandler, Injectable} from '@angular/core';
import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpResponse,
} from '@angular/common/http';
import {Observable} from 'rxjs';
import {tap} from 'rxjs/operators';

@Injectable()
export class ServiceSuccessInterceptor implements HttpInterceptor {

  constructor(private errorHanlder: ErrorHandler) {
  }


  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {


    return next.handle(req).pipe(
      tap((event: HttpEvent<any>) => {
        if (event instanceof HttpResponse && event.status === 200) {

          if (!event.body.success && event.url.indexOf('.') < 0) {
            this.errorHanlder.handleError(event.body);
            throw new HttpErrorResponse({
              error: event.body,
              headers: event.headers,
              status: 500,
              statusText: '',
              url: event.url,
            });
          }
        }
        return event;
      }));
  }
}
