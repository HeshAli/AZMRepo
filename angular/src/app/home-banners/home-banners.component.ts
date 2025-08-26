import { ChangeDetectorRef, Component, Injector } from "@angular/core";
import { catchError, finalize, map, switchMap } from "rxjs/operators";
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import {
  PagedListingComponentBase,
  PagedRequestDto,
} from "@shared/paged-listing-component-base";
import {
  HomeBannerServiceProxy,
  HomeBannerDto,
  HomeBannerDtoPagedResultDto,
  AttachmentServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { CreateHomeBannerDialogComponent } from "./create-home-banner/create-home-banner-dialog/create-home-banner-dialog.component";
import { EditHomeBannerDialogComponent } from "./edit-home-banner/edit-home-banner-dialog/edit-home-banner-dialog.component";
import { of } from "rxjs";

class PagedHomeBannersRequestDto extends PagedRequestDto {
  keyword: string;
  isActive: boolean | null;
}

@Component({
  templateUrl: "./home-banners.component.html",
  animations: [appModuleAnimation()],
})
export class HomeBannersComponent extends PagedListingComponentBase<HomeBannerDto> {
  banners: HomeBannerDto[] = [];
  keyword = "";
  isActive: boolean | null = undefined;
  advancedFiltersVisible = false;
  homeBanner: HomeBannerDto = new HomeBannerDto();
  imagePreview: string | null = null;
  logoPreview: string | null = null;
  imageId: number | null = null;
  constructor(
    injector: Injector,
    private _bannerService: HomeBannerServiceProxy,
    private _attachmentServiceProxy: AttachmentServiceProxy,
    private _modalService: BsModalService,
    cd: ChangeDetectorRef
  ) {
    super(injector, cd);
  }

  createBanner(): void {
    this.showCreateOrEditBannerDialog();
  }

  editBanner(banner: HomeBannerDto): void {
    this.showCreateOrEditBannerDialog(banner);
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
    this._bannerService
      .getHomeBannerList(
        this.keyword,
        this.isActive ?? undefined,
        request.skipCount,
        request.maxResultCount
      )
      .pipe(finalize(() => finishedCallback()))
      .subscribe((result: HomeBannerDtoPagedResultDto) => {
        this.banners = result.items || [];
        this.showPaging(result, pageNumber);
        this.cd.detectChanges();
      });
  }

  protected delete(banner: HomeBannerDto): void {
    const displayName = banner.nameAr || "";
    abp.message.confirm(
      this.l("BannerDeleteWarningMessage", displayName),
      undefined,
      (confirmed: boolean) => {
        if (confirmed) {
          this._bannerService.deleteHomeBanner(banner.id).subscribe(() => {
            abp.notify.success(this.l("SuccessfullyDeleted"));
            this.refresh();
          });
        }
      }
    );
  }

  private showCreateOrEditBannerDialog(row?): void {
    if (!row) {
      const modalRef = this._modalService.show(
        CreateHomeBannerDialogComponent,
        {
          class: "modal-lg",
        }
      );
      modalRef.content?.onSave.subscribe(() => this.refresh());
      return;
    }

    // Edit flow: get banner, then (optionally) attachments, then open modal once with full data.
    this._bannerService
      .getHomeBanner(row.id)
      .pipe(
        switchMap((banner) => {
          this.homeBanner = banner;

          const hasImage = banner?.imageId != null && banner.imageId > 0;
          const hasLogo = banner?.logoId != null && banner.logoId > 0;

          if (!hasImage && !hasLogo) {
            // no attachments → just proceed with null previews
            return of({
              banner,
              imagePreview: null,
              logoPreview: null,
            });
          }
          banner.imageId = banner.imageId ?? 0;
          banner.logoId = banner.logoId ?? 0;
          return this._attachmentServiceProxy
            .getHomeBannerAttachments(banner.imageId, banner.logoId)
            .pipe(
              map((res: any) => {
                // handle both shapes: res or res.result
                const data = res && res.image !== undefined ? res : res?.result;
                const imagePreview = data?.image?.path ?? null;
                const logoPreview = data?.logo?.path ?? null;
                return { banner, imagePreview, logoPreview };
              }),
              // if attachments call fails, still open modal
              catchError((_) =>
                of({ banner, imagePreview: null, logoPreview: null })
              )
            );
        })
      )
      .subscribe(({ banner, imagePreview, logoPreview }) => {
        const modalRef = this._modalService.show(
          EditHomeBannerDialogComponent,
          {
            class: "modal-lg",
            initialState: {
              id: row.id,
              imageId: banner.imageId ?? null,
              logoId: banner.logoId ?? null,
              imagePreview,
              logoPreview,
              homeBanner: banner,
            },
          }
        );
        modalRef.content?.onSave.subscribe(() => this.refresh());
        this.cd.detectChanges();
      });
  }

  private getHomeBanner(id: number): void {
    this._bannerService.getHomeBanner(id).subscribe((result) => {
      this.homeBanner = result;
      if (this.homeBanner) {
        this.getAttachmentById(this.homeBanner.imageId);
      }
      this.cd.markForCheck();
    });
  }
  private getAttachmentById(imageId): void {
    if (imageId) {
      this._attachmentServiceProxy.getAttachment(imageId).subscribe((res) => {
        this.imagePreview = res.path;
        this.cd.markForCheck();
      });
    }
  }

  trackById(index: number, item: HomeBannerDto): number {
    return item.id;
  }
}
