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
  UpdateContentDto,
  FileParameter,
  ContentDto,
  ContentServiceProxy,
  AttachmentServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { finalize } from "rxjs/operators";
import { SafeUrl } from "@angular/platform-browser";
import { NgForm } from "@angular/forms";

@Component({
  templateUrl: "./edit-home-gallery.component.html",
})
export class EditHomeGalleryComponent
  extends AppComponentBase
  implements OnInit, OnChanges
{
  saving = false;
  gallery = new ContentDto();
  id: number;
  imagePreview: string | null = null;
  imageId: number | null = null;

  content = new UpdateContentDto();

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

  ngOnChanges(changes: SimpleChanges): void {
    if (changes["imagePreview"] && changes["imagePreview"].currentValue) {
      this.imagePreview = changes["imagePreview"].currentValue;
      this.cd.markForCheck();
    }
  }
  ngOnInit(): void {
    this._contentServiceProxy.getContent(this.id).subscribe((result) => {
      this.content = result;
      this.cd.detectChanges();
    });
  }

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

    this._contentServiceProxy.updateContent(this.content).subscribe(
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
