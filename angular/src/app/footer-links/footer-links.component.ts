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
import { CreateProgressContentDialogComponent } from "./create-home-progressContent/create-home-progressContent-dialog/create-progressContent-dialog.component";
import { EditProgressContentDialogComponent } from "./edit-home-progressContent/edit-home-progressContent-dialog/edit-progressContent-dialog.component";
import { FooterDto, FooterDtoPagedResultDto, FooterServiceProxy } from "../../shared/service-proxies/service-proxies";
 
 

class PagedUserContentRequestDto extends PagedRequestDto {
    keyword: string;
    isActive: boolean | null;
}

@Component({
    templateUrl: "./footer-links.component.html",
  animations: [appModuleAnimation()],
})
export class FooterLinksComponent extends PagedListingComponentBase<ContentDto> {
    banners: FooterDto[] = [];
  keyword = "";
  isActive: boolean | null = undefined;
  advancedFiltersVisible = false;

  constructor(
      injector: Injector,
      private _footerServiceProxy: FooterServiceProxy,
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
      this._footerServiceProxy
          .getFooterList(
        1,
              this.isActive ?? undefined, undefined,
        request.skipCount,
        request.maxResultCount
      )
          .pipe(finalize(() => finishedCallback()))
          .subscribe((result: FooterDtoPagedResultDto) => {
        this.banners = result.items || [];
        this.showPaging(result, pageNumber);
        this.cd.detectChanges();
      });
  }

    protected delete(banner: FooterDto): void {
    const displayName = banner.nameAr || "";
    abp.message.confirm(
      this.l("BannerDeleteWarningMessage", displayName),
      undefined,
      (confirmed: boolean) => {
          if (confirmed) {
              this._footerServiceProxy.deleteFooter(banner.id).subscribe(() => {
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
        modalRef = this._modalService.show(CreateProgressContentDialogComponent, {
        class: "modal-lg",
      });
    } else {
        modalRef = this._modalService.show(EditProgressContentDialogComponent, {
        class: "modal-lg",
        initialState: { id },
      });
    }

    modalRef.content?.onSave.subscribe(() => this.refresh());
  }

    trackById(index: number, item: FooterDto): number {
    return item.id;
  }
}
