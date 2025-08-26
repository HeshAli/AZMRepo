import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
  ChangeDetectorRef
} from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';
import {
  HomeBannerServiceProxy,
  HomeBannerDto,
  UpdateContentDto,
  ContentDto,
  ContentServiceProxy
} from '@shared/service-proxies/service-proxies';
import { NgForm } from '@angular/forms';
import { FooterServiceProxy, UpdateFooterDto } from '../../../../shared/service-proxies/service-proxies';

@Component({
    templateUrl: './edit-footer-links-dialog.component.html'
})
export class EditFooterLinksDialogComponent extends AppComponentBase
  implements OnInit {
    saving = false;
    footer = new UpdateFooterDto();
  id: number;

  @Output() onSave = new EventEmitter<any>();

  constructor(
      injector: Injector,
      public _footerServiceProxy: FooterServiceProxy,
    public bsModalRef: BsModalRef,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
      this._footerServiceProxy.getFooter(this.id).subscribe((result) => {
          this.footer = result;
      this.cd.detectChanges();
    });
  }

    save(): void {
         
    this.saving = true;

        this._footerServiceProxy.updateFooter(this.footer).subscribe(
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
