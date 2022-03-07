import {HttpRequest, HttpResponse} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {AppLoggerService} from './app-logger.service';
import {CacheService} from './cache.service';

const maxAge = 2000; // maximum cache age (ms)

@Injectable()
export class RequestCacheService {

  constructor(
    private messenger: AppLoggerService,
    private cache: CacheService) {
  }

  get(req: HttpRequest<any>, loadExpired: boolean = false): HttpResponse<any> | undefined {
    const url = req.urlWithParams;
    const cached = this.cache.get(url);

    if (!cached) {
      return undefined;
    }

    const isExpired = cached.lastRead < (Date.now() - maxAge);
    const expired = isExpired ? 'expired ' : '';
    this.messenger.debug(
      `Found ${expired} cached response for '${url}'.`);
    return isExpired && !loadExpired ? undefined : cached.val;
    // return cached.val;
  }

  put(req: HttpRequest<any>, response: HttpResponse<any>): void {
    if (this.canBeCached(req)) {
      const url = req.urlWithParams;
      this.messenger.debug(`Caching response from '${url}'.`);
      this.cache.add(url, response);
    }
  }

  private canBeCached(req: HttpRequest<any>) {
    return req.method === 'GET' || req.method === 'OPTIONS';
  }
}
