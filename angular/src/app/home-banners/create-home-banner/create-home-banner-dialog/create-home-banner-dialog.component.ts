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
  HomeBannerDto
} from '../../../../shared/service-proxies/service-proxies';

@Component({
  templateUrl: './create-home-banner-dialog.component.html'
})
export class CreateHomeBannerDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  banner = new HomeBannerDto();

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _bannerService: HomeBannerServiceProxy,
    public bsModalRef: BsModalRef,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {}

  save(): void {
    this.saving = true;

    this._bannerService.createHomeBanner(this.banner).subscribe(
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