import { ChangeDetectorRef, Component, Injector } from "@angular/core";
import { finalize, switchMap, map, catchError } from "rxjs/operators";
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { of } from "rxjs";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import {
  PagedListingComponentBase,
  PagedRequestDto,
} from "@shared/paged-listing-component-base";
import {
  ContentDto,
  ContentServiceProxy,
  ContentDtoPagedResultDto,
  AttachmentServiceProxy,
} from "@shared/service-proxies/service-proxies";

import { CreateHomeGalleryComponent } from "./create-home-gallery/create-home-gallery.component";
import { EditHomeGalleryComponent } from "./edit-home-gallery/edit-home-gallery.component";

class PagedUserContentRequestDto extends PagedRequestDto {
  keyword: string;
  isActive: boolean | null;
}

@Component({
  templateUrl: "./home-gallery.component.html",
  animations: [appModuleAnimation()],
})
export class HomeGalleryComponent extends PagedListingComponentBase<ContentDto> {
  galleryImages: ContentDto[] = [];
  homeGallery: ContentDto = new ContentDto();
  imagePreview: string | null = null;
  imageId: number | null = null;
  keyword = "";
  isActive: boolean | null = undefined;
  advancedFiltersVisible = false;

  constructor(
    injector: Injector,
    private _contentServiceProxy: ContentServiceProxy,
    private _attachmentServiceProxy: AttachmentServiceProxy,
    private _modalService: BsModalService,
    cd: ChangeDetectorRef
  ) {
    super(injector, cd);
  }

  create(): void {
    this.showCreateOrEditGalleryImageDialog();
  }

  edit(content: ContentDto): void {
    this.showCreateOrEditGalleryImageDialog(content.id);
  }

  clearFilters(): void {
    this.keyword = "";
    this.isActive = undefined;
    this.getDataPage(1);
  }

  list(
    request: PagedRequestDto,
    pageNumber: number,
    finishedCallback: () => void
  ): void {
    this._contentServiceProxy
      .getContentList(
        this.keyword,
        this.isActive ?? undefined,
        undefined,
        "Home_Gallery",
        request.skipCount,
        request.maxResultCount
      )
      .pipe(finalize(() => finishedCallback()))
      .subscribe((result: ContentDtoPagedResultDto) => {
        this.galleryImages = result.items || [];
        this.showPaging(result, pageNumber);
        this.cd.detectChanges();
      });
  }

  protected delete(gallery: ContentDto): void {
    const displayName = gallery.nameAr || "";
    abp.message.confirm(
      this.l("GalleryImageDeleteWarningMessage", displayName),
      undefined,
      (confirmed: boolean) => {
        if (confirmed) {
          this._contentServiceProxy.deleteContent(gallery.id).subscribe(() => {
            abp.notify.success(this.l("SuccessfullyDeleted"));
            this.refresh();
          });
        }
      }
    );
  }

  private showCreateOrEditGalleryImageDialog(id?: number): void {
    let modalRef: BsModalRef;

    if (!id) {
      modalRef = this._modalService.show(CreateHomeGalleryComponent, {
        class: "modal-lg",
      });
      modalRef.content?.onSave.subscribe(() => this.refresh());
      return;
    }

    this._contentServiceProxy
      .getContent(id)
      .pipe(
        switchMap((content) => {
          this.homeGallery = content;
          if (content.attachmentId) {
            return this._attachmentServiceProxy
              .getAttachment(content.attachmentId)
              .pipe(
                map((att) => ({
                  content,
                  imagePreview: att?.path ?? null,
                })),
                catchError(() => of({ content, imagePreview: null }))
              );
          }
          return of({ content, imagePreview: null });
        })
      )
      .subscribe(({ content, imagePreview }) => {
        modalRef = this._modalService.show(EditHomeGalleryComponent, {
          class: "modal-lg",
          initialState: {
            id: content.id,
            gallery: content,
            imageId: content.attachmentId ?? null,
            imagePreview,
          },
        });
        modalRef.content?.onSave.subscribe(() => this.refresh());
        this.cd.detectChanges();
      });
  }

  trackById(index: number, item: ContentDto): number {
    return item.id;
  }
}
