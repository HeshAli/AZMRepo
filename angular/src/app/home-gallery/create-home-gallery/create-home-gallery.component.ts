import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
  ChangeDetectorRef,
} from "@angular/core";
import { BsModalRef } from "ngx-bootstrap/modal";
import { AppComponentBase } from "../../../shared/app-component-base";
import { SafeUrl } from "@angular/platform-browser";
import { finalize } from "rxjs/operators";
import {
  HomeBannerServiceProxy,
  HomeBannerDto,
  ContentServiceProxy,
  ContentDto,
  CreateContentDto,
  FileParameter,
  AttachmentServiceProxy,
} from "../../../shared/service-proxies/service-proxies";

@Component({
  templateUrl: "./create-home-gallery.component.html",
})
export class CreateHomeGalleryComponent
  extends AppComponentBase
  implements OnInit
{
  gallery = new ContentDto();
  saving = false;
  content = new CreateContentDto();
  selectedFile: File | null = null;
  previewUrl: string | null = null;
  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _contentServiceProxy: ContentServiceProxy,
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

  imagePreview: string | null = null;
  onFileUploaded(data: { files: Array<File & { objectURL?: SafeUrl }> }): void {
    this.saving = true;

    const file = data.files[0];
    const fileParam: FileParameter = {
      data: file,
      fileName: file.name,
    };
    this._attachmentServiceProxy
      .uploadAttachment("gallery", fileParam)
      .pipe(
        finalize(() => {
          this.saving = false;
          this.cd.markForCheck();
        })
      )
      .subscribe({
        next: (response) => {
          this.notify.success(this.l("UploadedSuccessfully"));
          this.gallery.attachmentId = response.id;
          this.imagePreview = response.path;
        },
        error: (error) => {
          console.error("Upload error:", error);
          this.notify.error(this.l("UploadFailed"));
        },
      });
  }

  save(): void {
    this.saving = true;
    this.content.categoryCode = "Home_Gallery";
    this.content.attachmentId = this.gallery.attachmentId;
    this._contentServiceProxy.createContent(this.content).subscribe(
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
}
