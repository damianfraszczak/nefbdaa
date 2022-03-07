import {Injectable} from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';
import {delay, distinctUntilChanged} from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class HttpLoaderService {

  public isLoading$: Observable<boolean>;
  private loadingSubject = new BehaviorSubject<boolean>(false);

  constructor() {
    this.isLoading$ = this.loadingSubject.asObservable().pipe(
      delay(100),
      distinctUntilChanged());
  }

  toggle(show: boolean) {
    if (show) {
      this.show();
    } else {
      this.hide();
    }
  }

  show() {
    this.emit(true);
  }

  hide() {
    this.emit(false);
  }


  private emit(newVal: boolean) {
    if (this.loadingSubject.getValue() !== newVal) {
      this.loadingSubject.next(newVal);
    }
  }
}
