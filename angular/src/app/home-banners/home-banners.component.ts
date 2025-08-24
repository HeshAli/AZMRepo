import { ChangeDetectorRef, Component, Injector } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
  PagedListingComponentBase,
  PagedRequestDto
} from 'shared/paged-listing-component-base';
import {
  HomeBannerServiceProxy,
  HomeBannerDto,
  HomeBannerDtoPagedResultDto
} from '@shared/service-proxies/service-proxies';
import { CreateHomeBannerDialogComponent } from './create-home-banner/create-home-banner-dialog.component';
import { EditHomeBannerDialogComponent } from './edit-home-banner/edit-home-banner-dialog.component';

class PagedHomeBannersRequestDto extends PagedRequestDto {
  keyword: string;
  isActive: boolean | null;
}

@Component({
  templateUrl: './home-banners.component.html',
  animations: [appModuleAnimation()]
})
export class HomeBannersComponent extends PagedListingComponentBase<HomeBannerDto> {
  banners: HomeBannerDto[] = [];
  keyword = '';
  isActive: boolean | null;
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
    this.keyword = '';
    this.isActive = undefined;
    this.getDataPage(1);
  }

  protected list(
    request: PagedHomeBannersRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;
    request.isActive = this.isActive;

    this._bannerService
      .getAll(
        request.keyword,
        request.isActive,
        request.skipCount,
        request.maxResultCount
      )
      .pipe(finalize(() => finishedCallback()))
      .subscribe((result: HomeBannerDtoPagedResultDto) => {
        this.banners = result.items;
        this.showPaging(result, pageNumber);
      });
  }

  protected delete(banner: HomeBannerDto): void {
    abp.message.confirm(
      this.l('BannerDeleteWarningMessage', banner.title),
      undefined,
      (result: boolean) => {
        if (result) {
          this._bannerService.delete(banner.id).subscribe(() => {
            abp.notify.success(this.l('SuccessfullyDeleted'));
            this.refresh();
          });
        }
      }
    );
  }

  private showCreateOrEditBannerDialog(id?: number): void {
    let dialog: BsModalRef;
    if (!id) {
      dialog = this._modalService.show(CreateHomeBannerDialogComponent, {
        class: 'modal-lg',
      });
    } else {
      dialog = this._modalService.show(EditHomeBannerDialogComponent, {
        class: 'modal-lg',
        initialState: { id: id },
      });
    }

    dialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }
}