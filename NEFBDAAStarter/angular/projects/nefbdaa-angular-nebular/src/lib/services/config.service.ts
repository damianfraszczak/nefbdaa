import {Inject, Injectable, OnInit} from '@angular/core';

import {Observable, of} from 'rxjs';
import {map, tap} from 'rxjs/operators';
import {FieldDefinitionModel} from '../models/dynamic-form/field-definition-model';
import {equalsIgnoringCase} from '../utils/utils';
import {AppConfigApiService} from './api/app-config-api-service';
import {AppConfigModel} from '../models/entities/app-config-model';
import {CONFIG_CRUD_SERVICE} from '../shared-constants';

@Injectable({
  providedIn: 'root',
})
export class ConfigService implements OnInit {

  private config: AppConfigModel;

  constructor(
    @Inject(CONFIG_CRUD_SERVICE)
    private configService: AppConfigApiService) {
  }

  ngOnInit(): void {
    this.loadConfig();
  }

  public getModelFieldDefinition(modelType: string): Observable<FieldDefinitionModel[]> {
    if (this.config == null) {
      return this.loadConfig()
        .pipe(map(config =>
          config.models.filter(x => equalsIgnoringCase(x.modelType, modelType)).pop().fieldDefinitions));
    } else {
      return of(this.config.models.filter(x => equalsIgnoringCase(x.modelType, modelType)).pop().fieldDefinitions);
    }
  }

  private loadConfig(): Observable<AppConfigModel> {
    return this.configService.Config()
      .pipe(map(x => x.content))
      .pipe(tap(config => this.config = config))
      ;
  }
}
