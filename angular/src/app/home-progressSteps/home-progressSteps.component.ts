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
  ContentDto,
  ContentServiceProxy,
  ContentDtoPagedResultDto,
} from "@shared/service-proxies/service-proxies"; 
import { CreateProgressStepsDialogComponent } from "./create-home-progressSteps/create-home-progressSteps-dialog/create-progressSteps-dialog.component";
import { EditProgressStepsAzmDialogComponent } from "./edit-home-progressSteps/edit-home-progressSteps-dialog/edit-progressSteps-dialog.component";
 

class PagedUserContentRequestDto extends PagedRequestDto {
    keyword: string;
    isActive: boolean | null;
}

@Component({
    templateUrl: "./home-progressSteps.component.html",
  animations: [appModuleAnimation()],
})
export class ProgressStepsComponent extends PagedListingComponentBase<ContentDto> {
    banners: ContentDto[] = [];
  keyword = "";
  isActive: boolean | null = undefined;
  advancedFiltersVisible = false;

  constructor(
      injector: Injector,
      private _contentServiceProxy: ContentServiceProxy,
    private _modalService: BsModalService,
    cd: ChangeDetectorRef
  ) {
    super(injector, cd);
  }

  create(): void {
    this.showCreateOrEditBannerDialog();
  }

  edit(banner: HomeBannerDto): void {
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
      this._contentServiceProxy
          .getContentList(
        this.keyword,
              this.isActive ?? undefined, undefined,"Home_ProgressSteps",
        request.skipCount,
        request.maxResultCount
      )
      .pipe(finalize(() => finishedCallback()))
      .subscribe((result: ContentDtoPagedResultDto) => {
        this.banners = result.items || [];
        this.showPaging(result, pageNumber);
        this.cd.detectChanges();
      });
  }

    protected delete(banner: ContentDto): void {
    const displayName = banner.nameAr || "";
    abp.message.confirm(
      this.l("BannerDeleteWarningMessage", displayName),
      undefined,
      (confirmed: boolean) => {
          if (confirmed) {
              this._contentServiceProxy.deleteContent(banner.id).subscribe(() => {
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
        modalRef = this._modalService.show(CreateProgressStepsDialogComponent, {
        class: "modal-lg",
      });
    } else {
        modalRef = this._modalService.show(EditProgressStepsAzmDialogComponent, {
        class: "modal-lg",
        initialState: { id },
      });
    }

    modalRef.content?.onSave.subscribe(() => this.refresh());
  }

    trackById(index: number, item: ContentDto): number {
    return item.id;
  }
}
