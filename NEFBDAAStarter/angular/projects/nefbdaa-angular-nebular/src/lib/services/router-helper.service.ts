import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, Params, Router, RouterStateSnapshot} from '@angular/router';
import {Location} from '@angular/common';
import {Observable, of} from 'rxjs';
import {filter, map, withLatestFrom} from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class RouterHelperService {

  constructor(protected readonly router: Router,
              protected readonly location: Location) {
  }

  goToLastPageOrDefault(defaultPage = '') {
    this.location.back();
  }


  getParamFromRoute(name: string): Observable<string> {
    return this.getParamsFromRoute().pipe(map(x => x[name]));
  }

  private getAllParams(snapshot: ActivatedRouteSnapshot, currentParams: { [key: string]: string }):
    { [key: string]: string } {
    if (snapshot) {

      if (snapshot.queryParams) {
        for (const param in snapshot.queryParams) {
          currentParams[param] = snapshot.queryParams[param];
        }

      }
      if (snapshot.params) {
        for (const param in snapshot.params) {
          currentParams[param] = snapshot.params[param];
        }
      }
      if (snapshot.children) {
        for (let i = 0; i < snapshot.children.length; i++) {
          this.getAllParams(snapshot.children[i], currentParams);
        }

      }
    }


    return currentParams;
  }

  getParamsFromRoute(): Observable<{ [key: string]: string }> {
    return of(this.getAllParams(this.router.routerState.snapshot.root, {}));
  }

}
