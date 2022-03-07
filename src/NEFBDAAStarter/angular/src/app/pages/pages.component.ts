import {Component, Inject} from '@angular/core';
import {NbAccessChecker} from '@nebular/security';
import {MENU_ITEMS} from './pages-menu';
import {NbMenuItem, NbSearchService} from '@nebular/theme';
import {HttpLoaderService} from '../../../projects/nefbdaa-angular-nebular/src/lib/services/http-loader.service';
import {environment} from '../../environments/environment';
import {DialogService, MODEL_MAPPER_SERVICE} from '../../../projects/nefbdaa-angular-nebular/src/public-api';
import {ModelMapperService} from '../../../projects/nefbdaa-angular-nebular/src/lib/services/api/model-mapper-service';
import {forkJoin} from 'rxjs';
import {debounceTime, distinctUntilChanged, map} from 'rxjs/operators';

@Component({
  selector: 'ngx-pages',
  template: `
    <nb-layout windowMode>
      <nb-layout-header fixed>
        <ngx-header [userMenu]="userMenu" [title]="appTitle">
          <ng-container actions>
            <nb-actions>
              <nb-action size="large">
                <nb-search type="rotate-layout" size="large" title="Search for items"
                           hint="Hit enter to search."></nb-search>

              </nb-action>
            </nb-actions>
          </ng-container>
        </ngx-header>
      </nb-layout-header>

      <nb-sidebar class="menu-sidebar" tag="menu-sidebar" responsive>
        <nb-menu *ngIf='menu != null' [items]="menu"></nb-menu>
      </nb-sidebar>

      <nb-layout-column [nbSpinner]="(loaderService.isLoading$ | async)">
        <router-outlet></router-outlet>
      </nb-layout-column>

      <nb-layout-footer fixed>
        <ngx-footer></ngx-footer>
      </nb-layout-footer>
    </nb-layout>
  `,
})
export class PagesComponent {

  menu = MENU_ITEMS;
  userMenu = [
    {
      title: 'My account',
      link: '/pages/settings/account',
    },

    {
      title: 'Log out',
      link: '/auth/logout',
    },
  ];
  appTitle = environment.appName;

  constructor(public accessChecker: NbAccessChecker,
              public loaderService: HttpLoaderService,
              private searchService: NbSearchService,
              private dialogService: DialogService,
              @Inject(MODEL_MAPPER_SERVICE) private crudMapperService: ModelMapperService,
  ) {
    this.authMenuItems();
    this.searchService.onSearchSubmit()
      .pipe(
        debounceTime(500),
        distinctUntilChanged())
      .subscribe((data: any) => {
        const term = data.term;
        const allServices = this.crudMapperService.getAllCrudServices();
        const searchQuery = Object.keys(allServices)
          .map((model) =>
            allServices[model].Search(term).pipe(
              map(x => x.content),
              map(x => {
                return {type: model, result: x};
              }),
            ));

        forkJoin(searchQuery)
          .subscribe(searchResult => {
            const list = [];
            searchResult.forEach(x => {
              x.result.forEach(val => {
                val.optionAdditionalInfo['model'] = x.type;
                list.push(val);
              });
            });


            this.dialogService.showSelectListEditorDialog({
              header: 'Search results',
              okButtonText: 'View',
              items: list,
              selected: '',
              multiple: false,
              namePropertyName: 'model',
            })
              .onConfirm(dialogContent => {
                const selected = list.find(x => x.optionId === dialogContent.selected);
                this.crudMapperService.navigateToDetails(selected.optionAdditionalInfo['model'],
                  dialogContent.selected,
                  {});
              });
          });
      })
  }

  authMenuItems() {
    this.menu.forEach(item => {
      this.authMenuItem(item);
    });
  }

  authMenuItem(menuItem: NbMenuItem) {
    if (menuItem.data && menuItem.data['permission'] && menuItem.data['resource']) {
      this.accessChecker.isGranted(menuItem.data['permission'], menuItem.data['resource']).subscribe(granted => {
        menuItem.hidden = !granted;
      });
    } else {
      menuItem.hidden = false;
    }
    if (!menuItem.hidden && menuItem.children != null) {
      menuItem.children.forEach(item => {
        if (item.data && item.data['permission'] && item.data['resource']) {
          this.accessChecker.isGranted(item.data['permission'], item.data['resource']).subscribe(granted => {
            item.hidden = !granted;
          });
        } else {
          // if child item do not config-list any `data.permission` and `data.resource`
          // just inherit parent item's config-list
          item.hidden = menuItem.hidden;
        }
      });
    }
  }
}
