/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { ApiConfiguration as __Configuration } from '../api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { StringApiResponse } from '../models/string-api-response';
import { AuthenticateUserViewModel } from '../models/authenticate-user-view-model';
import { RegisterUserViewModel } from '../models/register-user-view-model';
import { ObjectOkApiResponse } from '../models/object-ok-api-response';
import { ResetPasswordViewModel } from '../models/reset-password-view-model';
import { ChangePasswordViewModel } from '../models/change-password-view-model';
@Injectable({
  providedIn: 'root',
})
class ApiAuthenticationService extends __BaseService {
  static readonly AuthenticatePath = '/api/ApiAuthentication/Authenticate';
  static readonly RegisterPath = '/api/ApiAuthentication/Register';
  static readonly LogoutPath = '/api/ApiAuthentication/Logout';
  static readonly ResetPasswordPath = '/api/ApiAuthentication/ResetPassword';
  static readonly ChangePasswordPath = '/api/ApiAuthentication/ChangePassword';
  static readonly LoginAsOtherUserPath = '/api/ApiAuthentication/LoginAsOtherUser';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * @param body undefined
   * @return Success
   */
  AuthenticateResponse(body?: AuthenticateUserViewModel): __Observable<__StrictHttpResponse<StringApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/ApiAuthentication/Authenticate`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<StringApiResponse>;
      })
    );
  }
  /**
   * @param body undefined
   * @return Success
   */
  Authenticate(body?: AuthenticateUserViewModel): __Observable<StringApiResponse> {
    return this.AuthenticateResponse(body).pipe(
      __map(_r => _r.body as StringApiResponse)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  RegisterResponse(body?: RegisterUserViewModel): __Observable<__StrictHttpResponse<StringApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/ApiAuthentication/Register`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<StringApiResponse>;
      })
    );
  }
  /**
   * @param body undefined
   * @return Success
   */
  Register(body?: RegisterUserViewModel): __Observable<StringApiResponse> {
    return this.RegisterResponse(body).pipe(
      __map(_r => _r.body as StringApiResponse)
    );
  }

  /**
   * @return Success
   */
  LogoutResponse(): __Observable<__StrictHttpResponse<ObjectOkApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    let req = new HttpRequest<any>(
      'DELETE',
      this.rootUrl + `/api/ApiAuthentication/Logout`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ObjectOkApiResponse>;
      })
    );
  }
  /**
   * @return Success
   */
  Logout(): __Observable<ObjectOkApiResponse> {
    return this.LogoutResponse().pipe(
      __map(_r => _r.body as ObjectOkApiResponse)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  ResetPasswordResponse(body?: ResetPasswordViewModel): __Observable<__StrictHttpResponse<ObjectOkApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/ApiAuthentication/ResetPassword`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ObjectOkApiResponse>;
      })
    );
  }
  /**
   * @param body undefined
   * @return Success
   */
  ResetPassword(body?: ResetPasswordViewModel): __Observable<ObjectOkApiResponse> {
    return this.ResetPasswordResponse(body).pipe(
      __map(_r => _r.body as ObjectOkApiResponse)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  ChangePasswordResponse(body?: ChangePasswordViewModel): __Observable<__StrictHttpResponse<ObjectOkApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/ApiAuthentication/ChangePassword`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ObjectOkApiResponse>;
      })
    );
  }
  /**
   * @param body undefined
   * @return Success
   */
  ChangePassword(body?: ChangePasswordViewModel): __Observable<ObjectOkApiResponse> {
    return this.ChangePasswordResponse(body).pipe(
      __map(_r => _r.body as ObjectOkApiResponse)
    );
  }

  /**
   * @param userId undefined
   * @return Success
   */
  LoginAsOtherUserResponse(userId?: number): __Observable<__StrictHttpResponse<StringApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (userId != null) __params = __params.set('userId', userId.toString());
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/ApiAuthentication/LoginAsOtherUser`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<StringApiResponse>;
      })
    );
  }
  /**
   * @param userId undefined
   * @return Success
   */
  LoginAsOtherUser(userId?: number): __Observable<StringApiResponse> {
    return this.LoginAsOtherUserResponse(userId).pipe(
      __map(_r => _r.body as StringApiResponse)
    );
  }
}

module ApiAuthenticationService {
}

export { ApiAuthenticationService }
