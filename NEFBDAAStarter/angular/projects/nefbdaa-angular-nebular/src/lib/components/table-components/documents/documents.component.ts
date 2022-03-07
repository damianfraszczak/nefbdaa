import {Component, Inject, Input, OnInit, ViewChild} from '@angular/core';
import {ITableConfig} from '../../smart-table-utilities/smart-table/smart-table-utils';
import {DocumentsApiCrudService} from '../../../services/api/documents-api-crud-service';
import {DOCUMENTS_CRUD_SERVICE} from '../../../shared-constants';
import {ConfigBasedTableComponent} from '../../table-components/config-based-table-component/config-based-table.component';

@Component({
  selector: 'ngx-documents',
  templateUrl: './documents.component.html',
  styleUrls: ['./documents.component.scss'],
})
export class DocumentsComponent implements OnInit {
  @Input()
  public showTitle = true;
  @Input()
  public title = 'Documents';
  @Input()
  public cardStatus = 'light';

  @Input()
  public ownerGuid: string;

  @Input()
  public maxFilesCount = Number.MAX_SAFE_INTEGER;

  @Input()
  public acceptFileTypes = '*';
  @Input()
  public maxFileSize = Number.MAX_SAFE_INTEGER;

  files: File[] = [];

  tableConfig: ITableConfig;
  @ViewChild(ConfigBasedTableComponent, {static: false})
  table: ConfigBasedTableComponent;

  uploadingFiles: boolean;

  constructor(@Inject(DOCUMENTS_CRUD_SERVICE) private readonly service: DocumentsApiCrudService) {

  }

  ngOnInit() {
    this.tableConfig = {
      modelType: 'document',
      title: this.title,
      selectionMode: 'single',
      showBulkUpdate: false,
      editModes: ['MODAL'],
      referencedProperties: [{value: this.ownerGuid, field: 'ownerGuid', type: 'guid', operator: '='}],
      customTableActions: [{name: 'download', title: '<i class="nb-arrow-dropdown"></i>'}],
    };
  }

  uploadFiles() {
    this.uploadingFiles = true;
    this.files.forEach(file =>
      this.service.Upload({file, ownerGuid: this.ownerGuid})
        .subscribe((result) => {
            this.table.dataSource.prepend(result.content);
            this.uploadingFiles = false;
          },
        ));
  }

  onCustomAction(event) {
    window.open(event.data.filePath, '_blank');
  }

}
