import {Component, OnChanges, SimpleChanges} from '@angular/core';

import {Subscription} from 'rxjs';
import {ɵp} from 'ng2-smart-table';

@Component({
  selector: 'ngx-table-filers',
  templateUrl: './smart-table-filers.component.html',
  styleUrls: ['./smart-table-filers.component.scss'],
})
export class SmartTableFilersComponent extends ɵp implements OnChanges {
  query = '';
  protected dataChangedSub: Subscription;

  ngOnChanges(changes: SimpleChanges) {
    if (changes.source) {
      if (!changes.source.firstChange) {
        this.dataChangedSub.unsubscribe();
      }
      this.dataChangedSub = this.source.onChanged().subscribe((dataChanges) => {
        const filterConf = this.source.getFilter();
        if (filterConf && filterConf.filters && filterConf.filters.length === 0) {
          this.query = '';

          // add a check for existing filters an set the query if one exists for this column
          // this covers instances where the filter is set by user code while maintaining existing functionality
        } else if (filterConf && filterConf.filters && filterConf.filters.length > 0) {
          filterConf.filters.forEach((k: any, v: any) => {
            if (k.field === this.column.id) {
              this.query = k.search;
            }
          });
        }
      });
    }
  }
}
