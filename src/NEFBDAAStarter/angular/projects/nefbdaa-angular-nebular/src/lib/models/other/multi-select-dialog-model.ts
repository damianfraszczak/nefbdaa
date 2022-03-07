import {ListViewModel} from './list-view-model';

export class MultiSelectDialogModel {

  constructor(public title: string,
              public items: ListViewModel[],
              public selected: string[] | string = [],
              public okButtonText: string = 'Ok',
              public multiple: boolean = true) {
  }
}
