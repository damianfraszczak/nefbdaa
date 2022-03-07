import {Component, Inject, Input, OnInit, ViewChild} from '@angular/core';
import {Dialog} from '../dialog';
import {NbDialogRef} from '@nebular/theme';
import {DialogConfigModel} from '../dialog-models';
import {SmartTablePagedAndSimplyQueryDS, SmartTableSimpleDS} from '../../../utils/service-data-source';
import {DocumentModel} from '../../../models/entities/document-model';
import {SmartTableComponent} from '../../smart-table-utilities/smart-table/smart-table.component';
import {getDefaultTableSettings} from '../../smart-table-utilities/smart-table/smart-table-utils';
import {DOCUMENTS_CRUD_SERVICE} from '../../../shared-constants';
import {DocumentsApiCrudService} from '../../../services/api/documents-api-crud-service';
import {extractContent} from '../../../utils/mappers';
import {PagedDataWithFilterParams} from '../../../models/api/paged-data-with-filter-params';

export interface DocumentsDialogModel extends DialogConfigModel {
  ownerGuid: string;
  documentType: string;
  maxFilesCount: number;
  acceptFileTypes: string;
  maxFileSize: number;

}

@Component({
  selector: 'ngx-table-select-dialog',
  templateUrl: './documents-dialog.component.html',
  styleUrls: ['./documents-dialog.component.scss'],
})
export class DocumentsDialogComponent extends Dialog<DocumentsDialogComponent, DocumentsDialogModel> implements OnInit {

  @Input()
  public title = 'Documents';

  @Input()
  public ownerGuid: string;

  @Input()
  public maxFilesCount = Number.MAX_SAFE_INTEGER;

  @Input()
  public acceptFileTypes = '*';
  @Input()
  public maxFileSize = Number.MAX_SAFE_INTEGER;
  uploadingFiles: boolean;
  files: File[] = [];
  dataSource: SmartTableSimpleDS<DocumentModel>;
  @ViewChild(SmartTableComponent, {static: false}) table: SmartTableComponent<DocumentModel>;
  tableSettings = getDefaultTableSettings(
    'document',
    false, true,
    true,
    [{name: 'download', title: '<i class="arrow-circle-down-outline\n"></i>'}],
    [],
    [
      {name: 'showBulkUpdate', value: false},
      {name: 'selectMode', value: 'single'},
    ],
  );

  constructor(protected readonly dialogRef: NbDialogRef<DocumentsDialogComponent>,
              @Inject(DOCUMENTS_CRUD_SERVICE) private readonly service: DocumentsApiCrudService) {
    super(dialogRef);

  }

  ngOnInit() {
    this.initDataSource();
  }

  uploadFiles() {
    this.uploadingFiles = true;
    this.files.forEach(file =>
      this.service.Upload({file, ownerGuid: this.ownerGuid})
        .subscribe((result) => {
            this.dataSource.prepend(result.content);
            this.uploadingFiles = false;
          },
        ));
  }

  onCustomAction(event) {
    window.open(event.data.filePath, '_blank');
  }

  private initDataSource(): void {

    this.dataSource = new SmartTablePagedAndSimplyQueryDS(
      {
        loader: (config) => this.service.GetPagedWithOptionsAndFilter(this.prepareQueryConfig(config)).pipe(extractContent()),
        updater: v => this.service.Put(v).pipe(extractContent()),
        remover: v => this.service.DeleteById(v.id).pipe(extractContent()),
        editor: v => this.service.GetWithEditData({id: v.id}).pipe(extractContent()),
      },
    );
  }

  private prepareQueryConfig(config: PagedDataWithFilterParams) {
    config.rule.rules.push({
      field: 'ownerGuid',
      value: this.ownerGuid,
      operator: '=',
      type: 'guid',
      condition: 'and',
    });
    return config;
  }


}
