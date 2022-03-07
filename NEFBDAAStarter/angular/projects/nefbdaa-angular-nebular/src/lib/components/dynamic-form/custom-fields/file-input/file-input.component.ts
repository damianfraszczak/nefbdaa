import {Component, forwardRef, Inject, Input} from '@angular/core';
import {DOCUMENTS_CRUD_SERVICE} from '../../../../shared-constants';
import {DocumentsApiCrudService} from '../../../../services/api/documents-api-crud-service';
import {DocumentModel} from '../../../../models/entities/document-model';
import {AbstractField} from '../abstract-field';
import {NG_VALUE_ACCESSOR} from '@angular/forms';

@Component({
  selector: 'ngx-file-input',
  templateUrl: './file-input.component.html',
  styleUrls: ['./file-input.component.scss'],
  providers: [
    {provide: NG_VALUE_ACCESSOR, useExisting: forwardRef(() => FileInputComponent), multi: true},
  ],
})
export class FileInputComponent extends AbstractField<number | string> {


  @Input()
  owner?: any;
  @Input()
  type?: string;
  document: DocumentModel;


  files: File[] = [];
  uploadingFiles: boolean;

  constructor(@Inject(DOCUMENTS_CRUD_SERVICE) private readonly service: DocumentsApiCrudService) {
    super();
  }

  uploadFiles() {
    this.uploadingFiles = true;
    this.files.forEach(file =>
      this.service.Upload({file, ownerGuid: this.owner['guid'], ownerDocumentType: this.type})
        .subscribe((result) => {
            this.uploadingFiles = false;
            this.document = result.content;
            this.value = this.document.id;
          },
        ));
  }

  writeValue(value: number | string) {
    super.writeValue(value);
    this.loadDocumentIfNeeded(value);
  }

  loadDocumentIfNeeded(value: number | string): void {
    if (value && (this.document == null || this.document.id !== Number(value))) {
      this.service.GetWithEditData({id: Number(this.value)}).subscribe(result => {
        this.document = result.content.object;
      });
    }
  }

  clearSelection() {
    this.document = null;
    this.value = '';
  }
}
