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

@Component({
    templateUrl: './edit-about-azm-dialog.component.html'
})
export class EditAboutAzmDialogComponent extends AppComponentBase
  implements OnInit {
    saving = false;
    content = new UpdateContentDto();
  id: number;

  @Output() onSave = new EventEmitter<any>();

  constructor(
      injector: Injector,
      public _contentServiceProxy: ContentServiceProxy,
    public bsModalRef: BsModalRef,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
      this._contentServiceProxy.getContent(this.id).subscribe((result) => {
          this.content = result;
      this.cd.detectChanges();
    });
  }

    save(): void {
         
    this.saving = true;

        this._contentServiceProxy.updateContent(this.content).subscribe(
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
