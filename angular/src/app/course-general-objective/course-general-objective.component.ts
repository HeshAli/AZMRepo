import { ChangeDetectorRef, Component, Injector } from "@angular/core";
import { finalize } from "rxjs/operators";
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { appModuleAnimation } from "../../shared/animations/routerTransition";
import {
  PagedListingComponentBase,
  PagedRequestDto,
} from "../../shared/paged-listing-component-base";
import {
  HomeBannerServiceProxy,
  HomeBannerDto,
  HomeBannerDtoPagedResultDto,
  ContentDto,
  ContentServiceProxy,
  ContentDtoPagedResultDto,
} from "../../shared/service-proxies/service-proxies";
import { CreateCourseGeneralObjectiveComponent } from "./create-course-general-objective/create-course-general-objective.component";
import { EditCourseGeneralObjectiveComponent } from "./edit-course-general-objective/edit-course-general-objective.component";

class PagedUserContentRequestDto extends PagedRequestDto {
  keyword: string;
  isActive: boolean | null;
}

@Component({
  templateUrl: "./course-general-objective.component.html",
  animations: [appModuleAnimation()],
})
export class CourseGeneralObjectiveComponent extends PagedListingComponentBase<ContentDto> {
  objectives: ContentDto[] = [];
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
        this.isActive ?? undefined,
        undefined,
        "Course_GeneralObjectives",
        request.skipCount,
        request.maxResultCount
      )
      .pipe(finalize(() => finishedCallback()))
      .subscribe((result: ContentDtoPagedResultDto) => {
        this.objectives = result.items || [];
        this.showPaging(result, pageNumber);
        this.cd.detectChanges();
      });
  }

  protected delete(banner: ContentDto): void {
    const displayName = banner.nameAr || "";
    abp.message.confirm(
      this.l("GeneralObjectiveDeleteWarningMessage", displayName),
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
      modalRef = this._modalService.show(
        CreateCourseGeneralObjectiveComponent,
        {
          class: "modal-lg",
        }
      );
    } else {
      modalRef = this._modalService.show(EditCourseGeneralObjectiveComponent, {
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
