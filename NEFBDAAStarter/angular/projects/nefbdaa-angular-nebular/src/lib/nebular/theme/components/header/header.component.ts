import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {NbMediaBreakpointsService, NbMenuService, NbSidebarService, NbThemeService} from '@nebular/theme';
import {map, takeUntil} from 'rxjs/operators';
import {Subject} from 'rxjs';
import {ConfigService} from '../../../../services/config.service';
import {LoggedUserService} from '../../../../services/logged-user.service';
import {Location} from '@angular/common';
import {LoggedUserModel} from '../../../../models/entities/logged-user-model';
import {CacheService} from '../../../../services/cache.service';

@Component({
  selector: 'ngx-header',
  styleUrls: ['./header.component.scss'],
  templateUrl: './header.component.html',
})
export class HeaderComponent implements OnInit, OnDestroy {

  @Input() position = 'normal';
  @Input() userMenu = [];
  @Input() title: string;
  user: LoggedUserModel;

  themes = [
    {
      value: 'default',
      name: 'Theme: Light',
    },
    {
      value: 'dark',
      name: 'Theme: Dark',
    },
    {
      value: 'cosmic',
      name: 'Theme: Cosmic',
    },
    {
      value: 'corporate',
      name: 'Theme: Corporate',
    },
  ];
  currentTheme = 'corporate';
  private destroy$: Subject<void> = new Subject<void>();
  showUserMenuAction = true;
  showChangeThemeAction = true;

  constructor(private sidebarService: NbSidebarService,
              private menuService: NbMenuService,
              private themeService: NbThemeService,
              private breakpointService: NbMediaBreakpointsService,
              private loggedUserService: LoggedUserService,
              private cacheService: CacheService,
              private configService: ConfigService,
              private readonly location: Location) {
  }

  ngOnInit() {
    const userTheme = this.cacheService.get('user_theme');
    if (userTheme == null) {
      this.changeTheme(this.themeService.currentTheme);
    } else {
      this.changeTheme(userTheme.val);
    }

    this.loggedUserService.getLoggedUser().subscribe(user => {
      console.log("Logged user " + user);
      this.user = user;
    });
    this.themeService.onThemeChange()
      .pipe(
        map(({name}) => name),
        takeUntil(this.destroy$),
      )
      .subscribe(themeName => this.currentTheme = themeName);
    this.configService.ngOnInit();

  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }

  toggleSidebar(): boolean {
    this.sidebarService.toggle(true, 'menu-sidebar');
    return false;
  }

  navigateHome() {
    this.menuService.navigateHome();
    return false;
  }

  changeTheme(themeName: string) {
    this.currentTheme = themeName;
    if (this.themeService.currentTheme !== themeName) {
      this.themeService.changeTheme(themeName);
      this.cacheService.add('user_theme', themeName);
    }

  }

  navigateToPreviousView() {
    this.location.back();
  }
}
