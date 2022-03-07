import {Injectable} from '@angular/core';
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse} from '@angular/common/http';


import {RequestCacheService} from '../request-cache.service';
import {OnlineOfflineService} from '../online-offline.service';
import {Observable, of} from 'rxjs';
import {tap} from 'rxjs/operators';


@Injectable()
export class CachingInterceptor implements HttpInterceptor {
  constructor(
    private cache: RequestCacheService,
    private networkService: OnlineOfflineService) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const cachedResponse = this.cache.get(req, !this.networkService.isOnline);
    // TODO verify if it works correctly
    // return cachedResponse && !this.networkService.isOnline ? of(cachedResponse) : this.sendRequest(req, next);
    return cachedResponse ? of(cachedResponse) : this.sendRequest(req, next);
  }

  sendRequest(
    req: HttpRequest<any>,
    next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      tap(event => {
        if (event instanceof HttpResponse) {
          // TODO fix problems with caching requests
          // this.cache.put(req, event);
        }
      }),
    );
  }
}
