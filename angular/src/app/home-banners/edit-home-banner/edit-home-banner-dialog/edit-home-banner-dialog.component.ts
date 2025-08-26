import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
  ChangeDetectorRef,
  OnChanges,
  SimpleChanges,
} from "@angular/core";
import { BsModalRef } from "ngx-bootstrap/modal";
import { AppComponentBase } from "@shared/app-component-base";
import {
  HomeBannerServiceProxy,
  HomeBannerDto,
  FileParameter,
  AttachmentServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { finalize } from "rxjs/operators";
import { SafeUrl } from "@angular/platform-browser";

@Component({
  templateUrl: "./edit-home-banner-dialog.component.html",
})
export class EditHomeBannerDialogComponent
  extends AppComponentBase
  implements OnInit, OnChanges
{
  saving = false;
  homeBanner = new HomeBannerDto();
  id: number;
  imagePreview: string | null = null;
  imageId: number | null = null;

  logoPreview: string | null = null;
  logoId: number | null = null;

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _bannerService: HomeBannerServiceProxy,
    public bsModalRef: BsModalRef,
    private cd: ChangeDetectorRef,
    private _attachmentServiceProxy: AttachmentServiceProxy
  ) {
    super(injector);
  }
  ngOnChanges(changes: SimpleChanges): void {
    if (changes["imagePreview"] && changes["imagePreview"].currentValue) {
      this.imagePreview = changes["imagePreview"].currentValue;
      this.cd.markForCheck();
    }
    if (changes["logoPreview"] && changes["logoPreview"].currentValue) {
      this.logoPreview = changes["logoPreview"].currentValue;
      this.cd.markForCheck();
    }
  }

  ngOnInit(): void {}

  save(): void {
    this.saving = true;

    this._bannerService.updateHomeBanner(this.homeBanner).subscribe(
      () => {
        this.notify.info(this.l("SavedSuccessfully"));
        this.bsModalRef.hide();
        this.onSave.emit();
      },
      () => {
        this.saving = false;
      }
    );
  }

  onFileUploaded(data: { files: Array<File & { objectURL?: SafeUrl }> }): void {
    this.saving = true;

    const file = data.files[0];
    const fileParam: FileParameter = {
      data: file,
      fileName: file.name,
    };
    this._attachmentServiceProxy
      .uploadAttachment(fileParam)
      .pipe(
        finalize(() => {
          this.saving = false;
          this.cd.markForCheck();
        })
      )
      .subscribe({
        next: (response) => {
          this.notify.success(this.l("UploadedSuccessfully"));
          this.homeBanner.imageId = response.id;
          this.imagePreview = response.path;
          //  this.banner.logoId= response.logoId
        },
        error: (error) => {
          console.error("Upload error:", error);
          this.notify.error(this.l("UploadFailed"));
        },
      });
  }

  onLogoUploaded(data: { files: Array<File & { objectURL?: SafeUrl }> }): void {
    this.saving = true;

    const file = data.files[0];
    const fileParam: FileParameter = {
      data: file,
      fileName: file.name,
    };
    this._attachmentServiceProxy
      .uploadAttachment(fileParam)
      .pipe(
        finalize(() => {
          this.saving = false;
          this.cd.markForCheck();
        })
      )
      .subscribe({
        next: (response) => {
          this.notify.success(this.l("UploadedSuccessfully"));
          this.homeBanner.logoId = response.id;
          this.logoPreview = response.path;
        },
        error: (error) => {
          console.error("Upload error:", error);
          this.notify.error(this.l("UploadFailed"));
        },
      });
  }
}
