import {Directive, ElementRef, Input, OnChanges} from '@angular/core';

@Directive({
  selector: '[ngxScrollIntoView]',
})
export class ScrollIntoViewDirective implements OnChanges {

  private static readonly options = {behavior: 'smooth', block: 'nearest', inline: 'center'};

  @Input('ngxScrollIntoView') scrollIntoView = false;

  constructor(public elementRef: ElementRef) {
  }

  ngOnChanges() {
    if (this.scrollIntoView) {
      this.scroll();
    }
  }

  private scroll() {
    this.elementRef.nativeElement.scrollIntoView(ScrollIntoViewDirective.options);
  }
}
