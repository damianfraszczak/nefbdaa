import {Component, EventEmitter, forwardRef, OnInit, Output} from '@angular/core';
import {Subject} from 'rxjs';
import {NbToastrService} from '@nebular/theme';
import {AbstractField} from '../abstract-field';
import {NG_VALUE_ACCESSOR} from '@angular/forms';

@Component({
  selector: 'ngx-photo-field',
  templateUrl: './photo-field.component.html',
  styleUrls: ['./photo-field.component.scss'],
  providers: [
    {provide: NG_VALUE_ACCESSOR, useExisting: forwardRef(() => PhotoFieldComponent), multi: true},
  ],
})
export class PhotoFieldComponent extends AbstractField<number | string> implements OnInit {

  @Output() imageChanged = new EventEmitter<string>();

  dragging = false;
  private imageSubject = new Subject<string>();

  constructor(readonly toastrService: NbToastrService) {
    super();
  }


  ngOnInit(): void {
    this.subscribeImageChange();
  }

  loadFile({dataTransfer, target}) {
    const file = dataTransfer ? dataTransfer.files[0] : target.files[0];

    const pattern = /image-*/;
    const reader = new FileReader();

    if (!file || !file.type.match(pattern)) {
      this.toastrService.danger('Invalid image format', 'Error');
      return;
    }

    reader.onload = this.handleReaderLoaded.bind(this);
    reader.readAsDataURL(file);
  }

  handleDrop(event) {
    event.preventDefault();
    this.loadFile(event);
  }

  clear() {
    this.imageSubject.next('');
  }

  private subscribeImageChange() {
    this.imageSubject.asObservable().subscribe((img: string) => this.onImageChange(img));
  }

  private onImageChange(img: string) {
    this.value = img;
    this.imageChanged.emit(img);

  }

  private handleReaderLoaded({target}) {
    this.imageSubject.next(target.result);
  }

}
