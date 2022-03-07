import {Component, OnInit} from '@angular/core';
import {AppLoggerService} from '../../../services/app-logger.service';
import {NgxLoggerLevel} from 'ngx-logger';

@Component({
  selector: 'ngx-logging',
  template: `
    <nb-card>
      <nb-card-header>
        App logs
      </nb-card-header>
      <nb-card-body>

      </nb-card-body>
    </nb-card>
  `,
  styles: [],
})
export class AppLoggingComponent implements OnInit {


  constructor(private logService: AppLoggerService) {
  }

  ngOnInit(): void {
    // this.logStream$ = this.logService.logs.pipe(map(ngxLog => {
    //   return {
    //     timestamp: ngxLog.timestamp,
    //     type: this.mapLogLevel(ngxLog.level),
    //     message: `${ngxLog.fileName}:${ngxLog.lineNumber}:${ngxLog.message}:${ngxLog.additional}`
    //   };
    // }));
  }

  private mapLogLevel(level: NgxLoggerLevel): 'LOG' | 'INFO' | 'WARN' | 'ERR' | 'SUCCESS' {
    switch (level) {
      case NgxLoggerLevel.TRACE:
      case NgxLoggerLevel.DEBUG:
      case NgxLoggerLevel.INFO:
      case NgxLoggerLevel.LOG:
        return 'LOG';
      case NgxLoggerLevel.WARN:
        return 'WARN';
      case NgxLoggerLevel.ERROR:
      case NgxLoggerLevel.FATAL:
        return 'ERR';
      default:
        return 'LOG';
    }
  }


}
