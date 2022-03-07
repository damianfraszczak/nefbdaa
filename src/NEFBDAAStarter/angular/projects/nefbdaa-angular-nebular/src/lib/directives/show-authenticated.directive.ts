import {Directive, Input, OnInit, TemplateRef, ViewContainerRef} from '@angular/core';

import {NbAuthService} from '@nebular/auth';

@Directive({
  selector: '[ngxShowAuthenticated]',

})
export class ShowAuthedDirective implements OnInit {
  condition: boolean;

  constructor(
    private templateRef: TemplateRef<any>,
    private readonly authService: NbAuthService,
    private viewContainer: ViewContainerRef,
  ) {
  }



  @Input() set ngxShowAuthenticated(condition: boolean) {
    this.condition = condition;
  }

  ngOnInit() {
    this.authService.isAuthenticated().subscribe(
      (isAuthenticated) => {
        if (isAuthenticated && this.condition || !isAuthenticated && !this.condition) {
          this.viewContainer.createEmbeddedView(this.templateRef);
        } else {
          this.viewContainer.clear();
        }
      },
    );
  }

}
