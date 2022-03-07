import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/Observable';
import {map} from 'rxjs/operators/map';

import {NbAuthJWTToken, NbAuthService} from '@nebular/auth';
import {NbRoleProvider} from '@nebular/security';

@Injectable({
  providedIn: 'root',
})
export class RoleProvider implements NbRoleProvider {

  constructor(private authService: NbAuthService) {
  }

  getRole(): Observable<string | string[]> {
    return this.authService.onTokenChange()
      .pipe(
        map((token: NbAuthJWTToken) => {
          const roles = token.isValid() ? token.getPayload()['role'].split(',') : ['guest'];
          return roles;
        }),
      );
  }
}
