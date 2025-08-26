import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
  ChangeDetectorRef
} from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '../../../../shared/app-component-base';
import {
  HomeBannerServiceProxy,
  HomeBannerDto,
  ContentServiceProxy,  
  CreateFooterDto,
  FooterServiceProxy
} from '../../../../shared/service-proxies/service-proxies';

@Component({
    templateUrl: './create-footer-links-dialog.component.html'
})
export class CreateFooterLinksDialogComponent extends AppComponentBase
  implements OnInit {
    saving = false;
    footer = new CreateFooterDto();

  @Output() onSave = new EventEmitter<any>();

  constructor(
      injector: Injector,
      public _footerServiceProxy: FooterServiceProxy,
    public bsModalRef: BsModalRef,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {}

  save(): void {
    this.saving = true;
      this.footer.type = 1;
      this._footerServiceProxy.createFooter(this.footer).subscribe(
      () => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      },
      () => {
        this.saving = false;
      }
    );
  }
}
