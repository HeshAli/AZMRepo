import { ChangeDetectorRef, Component, Injector } from "@angular/core";
import { finalize } from "rxjs/operators";
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
} from "@shared/service-proxies/service-proxies";
import { CreateHomeBannerDialogComponent } from "./create-home-banner/create-home-banner-dialog/create-home-banner-dialog.component";
import { EditHomeBannerDialogComponent } from "./edit-home-banner/edit-home-banner-dialog/edit-home-banner-dialog.component";

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

  constructor(
    injector: Injector,
    private _bannerService: HomeBannerServiceProxy,
    private _modalService: BsModalService,
    cd: ChangeDetectorRef
  ) {
    super(injector, cd);
  }

  createBanner(): void {
    this.showCreateOrEditBannerDialog();
  }

  editBanner(banner: HomeBannerDto): void {
    this.showCreateOrEditBannerDialog(banner.id);
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

  private showCreateOrEditBannerDialog(id?: number): void {
    let modalRef: BsModalRef;

    if (!id) {
      modalRef = this._modalService.show(CreateHomeBannerDialogComponent, {
        class: "modal-lg",
      });
    } else {
      modalRef = this._modalService.show(EditHomeBannerDialogComponent, {
        class: "modal-lg",
        initialState: { id },
      });
    }

    modalRef.content?.onSave.subscribe(() => this.refresh());
  }

  trackById(index: number, item: HomeBannerDto): number {
    return item.id;
  }
}
