import {Subject} from 'rxjs';

export class SingleSelectionModel<T> {
  selected: T;

  private deselectionSubject = new Subject<T>();
  private selectionSubject = new Subject<T>();

  select(value: T) {
    if (this.selected) {
      this.deselect();
    }
    this.selected = value;
    this.selectionSubject.next(this.selected);
  }

  deselect() {
    const value = this.selected;
    this.selected = undefined;
    if (value) {
      this.deselectionSubject.next(value);
    }
  }

  onChange(onDeselect: (deselected: T) => void, onSelect: (selected: T) => void) {
    this.deselectionSubject.asObservable().subscribe(onDeselect);
    this.selectionSubject.asObservable().subscribe(onSelect);
  }

  onSelect(next: (selected: T) => void) {
    this.selectionSubject.asObservable().subscribe(next);
  }

  onDeselect(next: (deselected: T) => void) {
    this.deselectionSubject.asObservable().subscribe(next);
  }

  getSelected(): T {
    return this.selected;
  }

  isSelected(value: T) {
    return this.selected === value;
  }

  isAnySelected(): boolean {
    return this.selected != null;
  }
}
