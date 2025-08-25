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
  ContentDto,
  CreateContentDto
} from '../../../../shared/service-proxies/service-proxies';

@Component({
    templateUrl: './create-TrainingAzm-dialog.component.html'
})
export class CreateTrainingAzmDialogComponent extends AppComponentBase
  implements OnInit {
    saving = false;
    banner = new CreateContentDto();

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
      public _contentServiceProxy: ContentServiceProxy,
    public bsModalRef: BsModalRef,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {}

  save(): void {
    this.saving = true;
      this.banner.categoryCode = "Home_TrainingWithAzm";
      this._contentServiceProxy.createContent(this.banner).subscribe(
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
