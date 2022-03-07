import {Component, OnInit} from '@angular/core';
import {Dialog} from '../dialog';
import {NbDialogRef} from '@nebular/theme';
import {SmartTableSimpleDS} from '../../../utils/service-data-source';
import {getDefaultTableSettings, initDataSource} from '../../smart-table-utilities/smart-table/smart-table-utils';
import {of} from 'rxjs';
import {TableSelectDialogModel} from '../dialog-models';
import {ConfigService} from '../../../services/config.service';
import {removeIfExists} from '../../../utils/filters';


@Component({
  selector: 'ngx-table-select-dialog',
  templateUrl: './table-select-dialog.component.html',
  styleUrls: ['./table-select-dialog.component.scss'],
})
export class TableSelectDialogComponent extends Dialog<TableSelectDialogComponent,
  TableSelectDialogModel> implements OnInit {

  currentData: any[];
  dataSource: SmartTableSimpleDS<any>;
  tableSettings: any;


  constructor(protected readonly dialogRef: NbDialogRef<TableSelectDialogComponent>,
              private  configService: ConfigService) {
    super(dialogRef);

  }

  ngOnInit(): void {
    this.initTableSettings();

  }

  deleteItem(item: any) {
    removeIfExists(item, this.currentData);
    this.dataSource.refresh();
  }

  protected updateResult(dialogContent: TableSelectDialogModel) {
    dialogContent.selected = this.dataSource.getLoadedData();
  }

  private initTableSettings() {
    this.currentData = this.dialogContent.data;
    const allSettings = getDefaultTableSettings(
      this.dialogContent.modelType,
      false,
      false,
      this.dialogContent.deleteButtonHidden != null ? !this.dialogContent.deleteButtonHidden : true,
      [],
      [],
      [
        {name: 'showBulkUpdate', value: false},
        {name: 'selectMode', value: 'single'},
        {name: 'pager', value: null},
        {name: 'showFilters', value: false},
        {name: 'editModes', value: []},

      ],
    );
    this.dataSource = new SmartTableSimpleDS(
      {
        loader: () => of(this.currentData),
      }, this.dialogContent.cellOptions);

    initDataSource(this.configService, this.dataSource, allSettings).subscribe((settings) => {
      this.tableSettings = settings;
    });
  }

}
