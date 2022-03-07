import {Component, Inject, Input, OnInit, ViewChild} from '@angular/core';
import {SmartTablePagedAndSimplyQueryDS, SmartTableSimpleDS} from '../../../utils/service-data-source';
import {SmartTableComponent} from '../../smart-table-utilities/smart-table/smart-table.component';
import {getDefaultTableSettings, ITableConfig} from '../../smart-table-utilities/smart-table/smart-table-utils';
import {extractContent} from '../../../utils/mappers';
import {PagedDataWithFilterParams} from '../../../models/api/paged-data-with-filter-params';
import {ApiCrudService} from '../../../services/api/api-crud-service';
import {NoteModel} from '../../../models/entities/note-model';
import {NOTES_CRUD_SERVICE} from '../../../shared-constants';


@Component({
  selector: 'ngx-notes',
  templateUrl: './notes.component.html',
  styleUrls: ['./notes.component.scss'],
})
export class NotesComponent implements OnInit {
  @Input()
  public showTitle = true;
  @Input()
  public title = 'Notes';
  @Input()
  public cardStatus = 'light';
  @Input()
  ownerGuid: string;

  tableConfig: ITableConfig;


  constructor() {
  }

  ngOnInit() {
    this.tableConfig = {
      modelType: 'note',
      title: this.title,
      selectionMode: 'single',
      showBulkUpdate: false,
      editModes: ['MODAL'],
      referencedProperties: [{value: this.ownerGuid, field: 'ownerGuid', type: 'guid', operator: '='}],
    };
  }


}
