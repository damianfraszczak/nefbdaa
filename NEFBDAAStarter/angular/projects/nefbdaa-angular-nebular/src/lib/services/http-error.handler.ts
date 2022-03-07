import {ErrorHandler, Injectable, Injector, NgZone} from '@angular/core';
import {DialogService} from '../components/dialog/dialog.service';
import {NbAuthService} from '@nebular/auth';
import {AppLoggerService} from './app-logger.service';
import {NbToastrService} from '@nebular/theme';
import {HttpErrorResponse} from '@angular/common/http';
import {BaseApiResponse} from '../models/api/base-api-response';
import {constants} from '../shared-constants';

const IGNORED_ERRORS = [
  '',

];

@Injectable()
export class HttpErrorHandler implements ErrorHandler {
  constructor(
    private readonly appLogService: AppLoggerService,
    private readonly dialogsService: DialogService,
    private readonly toastrService: NbToastrService,
    private readonly injector: Injector) {
  }

  handleError(error: any) {
    console.log(error);
    this.appLogService.error('Raw error', error);
    let header = 'Api error';
    let message = '';
    let timeout = 2000;
    let type = 'notification';
    let showForUser = true;
    const additionalInfo = [];
    // when response from server it has ok param
    if (error.rejection && error.rejection.ok) {
      return;
    } else if (error.rejection && error.rejection instanceof HttpErrorResponse) {
      header = 'Api error';
      const serverError = error.rejection.error as BaseApiResponse<any>;
      message = serverError.message;
      additionalInfo.push(serverError.detail);
      if (serverError.uiConfig) {
        timeout = serverError.uiConfig.timeout;
        type = serverError.uiConfig.type;
      }
    } else if (error instanceof HttpErrorResponse) {
      header = 'Api error';
      const serverError = error.error as BaseApiResponse<any>;
      message = serverError.message;
      additionalInfo.push(serverError.detail);
      if (serverError.uiConfig) {
        timeout = serverError.uiConfig.timeout;
        type = serverError.uiConfig.type;
      }
    } else if (error.status) {
      if (error.status === 401) {
        this.logout();
      }
      header = 'Unauthorized';
      message = error;
      // ui error
    } else if (error.stack && error.message) {
      header = 'UI error';
      message = error.message;
      showForUser = false;
      additionalInfo.push(JSON.stringify(error.stack));
    } else if (error.message) {
      header = 'Api error';
      message = error.message;
      if (error.uiConfig) {
        timeout = error.uiConfig.timeout;
        type = error.uiConfig.type;
      }
    } else {
      showForUser = false;
      header = 'Undefined error';
      message = JSON.stringify(error);
    }
    if (showForUser) {
      this.showToast(header, message, timeout, type);
    }
    this.appLogService.error(`${header} - ${message}`, additionalInfo);

  }

  private logout() {
    const service = this.injector.get(NbAuthService);
    const ngZone = this.injector.get(NgZone);
    // TODO
    ngZone.run(() => service.logout(constants.securityStrategyName));
  }

  private showToast(header: string, message: string, timeout = 2000, type = 'notification') {
    this.toastrService.danger(
      message, `${header}`, {destroyByClick: true, duration: timeout});
    if (type === 'dialog') {
      this.dialogsService.showAlertDialog({
        status: 'danger',
        header: header,
        message: message,
      });
    }

  }
}
