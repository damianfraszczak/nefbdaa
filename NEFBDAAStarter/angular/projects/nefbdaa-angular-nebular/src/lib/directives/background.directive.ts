import {Directive, ElementRef, Input, OnChanges} from '@angular/core';
import {dataUrl} from 'angular-file/file-upload/fileTools';

@Directive({
  selector: '[ngxBackground]',
})
export class BackgroundDirective implements OnChanges {
  @Input('ngxBackground') file: any;
  @Input() defaultFile: any = '';

  constructor(public elementRef: ElementRef) {
  }

  ngOnChanges() {
    dataUrl(this.file)
      .then(src => {
        const urlString = 'url(\'' + (src || this.defaultFile) + '\')';
        this.elementRef.nativeElement.style.backgroundImage = urlString;
      });
  }


}

