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
  CourseServiceProxy,
  CourseDtoPagedResultDto,
  CourseDto,
} from "../../shared/service-proxies/service-proxies";
import { CreateCourseTrainingComponent } from "./create-course-training-course/create-course-training-course.component";
import { EditCourseTrainingComponent } from "./edit-course-training-course/edit-course-training-course.component";
class PagedUserContentRequestDto extends PagedRequestDto {
  keyword: string;
  isActive: boolean | null;
}

@Component({
  templateUrl: "./course-training-course.component.html",
  animations: [appModuleAnimation()],
})
export class TrainingCoursesComponent extends PagedListingComponentBase<ContentDto> {
  courses: CourseDto[] = [];
  keyword = "";
  isActive: boolean | null = undefined;
  advancedFiltersVisible = false;

  constructor(
    injector: Injector,
    private _coursesServiceProxy: CourseServiceProxy,
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
    this._coursesServiceProxy
      .getCourseList(this.keyword, request.skipCount, request.maxResultCount)
      .pipe(finalize(() => finishedCallback()))
      .subscribe((result: CourseDtoPagedResultDto) => {
        this.courses = result.items || [];
        this.showPaging(result, pageNumber);
        this.cd.detectChanges();
      });
  }

  protected delete(banner: ContentDto): void {
    const displayName = banner.nameAr || "";
    abp.message.confirm(
      this.l("TrainingUnitDeleteWarningMessage", displayName),
      undefined,
      (confirmed: boolean) => {
        if (confirmed) {
          this._coursesServiceProxy.deleteCourse(banner.id).subscribe(() => {
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
      modalRef = this._modalService.show(CreateCourseTrainingComponent, {
        class: "modal-lg",
      });
    } else {
      modalRef = this._modalService.show(EditCourseTrainingComponent, {
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
