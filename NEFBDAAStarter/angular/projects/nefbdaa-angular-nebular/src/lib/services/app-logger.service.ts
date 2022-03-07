import {Injectable} from '@angular/core';
import {NGXLogger, NGXLoggerMonitor, NGXLogInterface} from 'ngx-logger';
import {ReplaySubject} from 'rxjs';
import {HttpHeaders} from '@angular/common/http';
import {NbAuthService} from '@nebular/auth';


@Injectable({
  providedIn: 'root',
})
export class AppLoggerService implements NGXLoggerMonitor {

  public logs: ReplaySubject<NGXLogInterface> = new ReplaySubject<NGXLogInterface>(128);

  constructor(private logger: NGXLogger, private authService: NbAuthService) {
    logger.setWithCredentialsOptionValue(true);
    logger.registerMonitor(this);
    // TODO move it to auth service

    this.authService.onTokenChange().subscribe(token => {
      logger.setCustomHttpHeaders(new HttpHeaders({Authorization: `Bearer ${token}`}));
    });


  }

  public trace(message: any, ...additional: any[]): void {
    this.logger.trace(message, additional);
  }

  public debug(message: any, ...additional: any[]): void {
    this.logger.debug(message, additional);
  }


  public info(message: any, ...additional: any[]): void {
    this.logger.info(message, additional);
  }


  public log(message: any, ...additional: any[]): void {
    this.logger.log(message, additional);
  }


  public warn(message: any, ...additional: any[]): void {
    this.logger.warn(message, additional);
  }


  public error(message: any, ...additional: any[]): void {
    this.logger.error(message, additional);
  }


  public fatal(message: any, ...additional: any[]): void {
    this.logger.fatal(message, additional);
  }

  onLog(logObject: NGXLogInterface): void {
    this.logs.next(logObject);
  }

}
