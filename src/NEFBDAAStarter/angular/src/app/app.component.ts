import {Component, OnInit} from '@angular/core';
import {ConfigService} from '../../projects/nefbdaa-angular-nebular/src/public-api';
import {NbIconLibraries} from '@nebular/theme';

@Component({
  selector: 'ngx-app',
  template: '<router-outlet></router-outlet>',
})
export class AppComponent implements OnInit {

  constructor(
    private configService: ConfigService,
    private iconLibraries: NbIconLibraries) {
  }

  ngOnInit() {
    this.configService.ngOnInit();
    this.iconLibraries.registerFontPack('nebular', {iconClassPrefix: 'nb'});
    this.iconLibraries.setDefaultPack('nebular');
  }
}
