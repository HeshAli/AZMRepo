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
  HomeBannerDto
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: './edit-home-banner-dialog.component.html'
})
export class EditHomeBannerDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  homeBanner = new HomeBannerDto();
  id: number;

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _bannerService: HomeBannerServiceProxy,
    public bsModalRef: BsModalRef,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._bannerService.getHomeBanner(this.id).subscribe((result) => {
      this.homeBanner = result;
      this.cd.detectChanges();
    });
  }

  save(): void {
    this.saving = true;

    this._bannerService.updateHomeBanner(this.homeBanner).subscribe(
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