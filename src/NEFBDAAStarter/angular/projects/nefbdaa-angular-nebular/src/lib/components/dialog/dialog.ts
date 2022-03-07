import {NbDialogRef} from '@nebular/theme';
import {merge, Observable, Subject} from 'rxjs';
import {Input} from '@angular/core';

export class Dialog<T, R> {

  @Input() public dialogContent: R;
  public confirm = new Subject<R>();
  public cancel = new Subject<R>();

  constructor(protected readonly dialogRef: NbDialogRef<T>) {
  }

  public dismiss(): void {
    this.dialogRef.close();
    this.cancel.next(this.dialogContent);
  }

  public proceed(): void {
    this.dialogRef.close();
    this.updateResult(this.dialogContent);
    this.confirm.next(this.dialogContent);
  }

  public onConfirm(action: (content: R) => void): Dialog<T, R> {
    this.confirm.asObservable().subscribe(action);
    return this;
  }


  public onCancel(action: (content: R) => void): Dialog<T, R> {
    this.cancel.asObservable().subscribe(action);
    return this;
  }

  public resultAsObservable(): Observable<R> {
    return merge(this.cancel.asObservable(), this.confirm.asObservable());
  }

  protected updateResult(dialogContent: R) {

  }
}

