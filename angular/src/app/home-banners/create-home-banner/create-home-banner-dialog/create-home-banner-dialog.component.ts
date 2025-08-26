import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
  ChangeDetectorRef,
  ChangeDetectionStrategy,
} from "@angular/core";
import { BsModalRef } from "ngx-bootstrap/modal";
import { AppComponentBase } from "../../../../shared/app-component-base";
import {
  HomeBannerServiceProxy,
  HomeBannerDto,
  FileParameter,
  AttachmentServiceProxy,
} from "../../../../shared/service-proxies/service-proxies";
import { finalize } from "rxjs/operators";
import { SafeUrl } from "@angular/platform-browser";
@Component({
  templateUrl: "./create-home-banner-dialog.component.html",
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CreateHomeBannerDialogComponent
  extends AppComponentBase
  implements OnInit
{
  saving = false;
  banner = new HomeBannerDto();
  selectedFile: File | null = null;
  previewUrl: string | null = null;
  logoUrl: string | null = null;

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

  ngOnInit(): void {}

  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0];

    if (this.selectedFile) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.previewUrl = e.target.result;
        this.cd.detectChanges();
      };
      reader.readAsDataURL(this.selectedFile);
    }
  }

  save(): void {
    this.saving = true;
    this._bannerService
      .createHomeBanner(this.banner)
      .pipe(
        finalize(() => {
          this.saving = false;
          this.cd.markForCheck();
        })
      )
      .subscribe({
        next: () => {
          this.notify.info(this.l("SavedSuccessfully"));
          this.bsModalRef.hide();
          this.onSave.emit();
        },
      });
  }
  imagePreview: string | null = null;
  onFileUploaded(data: { files: Array<File & { objectURL?: SafeUrl }> }): void {
    this.saving = true;

    const file = data.files[0];
    const fileParam: FileParameter = {
      data: file,
      fileName: file.name,
    };
    this._attachmentServiceProxy
      .uploadAttachment("homeBanner", fileParam)
      .pipe(
        finalize(() => {
          this.saving = false;
          this.cd.markForCheck();
        })
      )
      .subscribe({
        next: (response) => {
          this.notify.success(this.l("UploadedSuccessfully"));
          this.banner.imageId = response.id;
          this.imagePreview = response.path;
        },
        error: (error) => {
          console.error("Upload error:", error);
          this.notify.error(this.l("UploadFailed"));
        },
      });
  }

  logoPreview: string | null = null;
  onLogoUploaded(data: { files: Array<File & { objectURL?: SafeUrl }> }): void {
    this.saving = true;

    const file = data.files[0];
    const fileParam: FileParameter = {
      data: file,
      fileName: file.name,
    };
    this._attachmentServiceProxy
      .uploadAttachment("homeBanner", fileParam)
      .pipe(
        finalize(() => {
          this.saving = false;
          this.cd.markForCheck();
        })
      )
      .subscribe({
        next: (response) => {
          this.notify.success(this.l("UploadedSuccessfully"));
          this.logoPreview = response.path;
          this.banner.logoId = response.id;
        },
        error: (error) => {
          console.error("Upload error:", error);
          this.notify.error(this.l("UploadFailed"));
        },
      });
  }
}
