import { ChangeDetectorRef, Component, Injector, OnInit } from "@angular/core";
import { finalize } from "rxjs/operators";
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { appModuleAnimation } from "../../../shared/animations/routerTransition";
import {
  PagedListingComponentBase,
  PagedRequestDto,
} from "../../../shared/paged-listing-component-base";
import {
  ContentDto,
  CourseDtoPagedResultDto,
  CourseDetailsDto,
  CourseDetailsServiceProxy,
  CourseDetailsDtoPagedResultDto,
} from "../../../shared/service-proxies/service-proxies";

import { CourseTrainingCourseDetailsCreateComponent } from "./course-training-course-details-create/course-training-course-details-create.component";
import { CourseTrainingCourseDetailEditComponent } from "./course-training-course-detail-edit/course-training-course-detail-edit.component";
import { ActivatedRoute } from "@node_modules/@angular/router";
class PagedUserContentRequestDto extends PagedRequestDto {
  keyword: string;
  isActive: boolean | null;
}

@Component({
  templateUrl: "./course-training-course-details.component.html",
  animations: [appModuleAnimation()],
})
export class CourseTrainingCourseDetailsComponent
  extends PagedListingComponentBase<CourseDetailsDto>
  implements OnInit
{
  courseDetails: CourseDetailsDto[] = [];
  keyword = "";
  isActive: boolean | null = undefined;
  advancedFiltersVisible = false;
  id!: number;

  constructor(
    injector: Injector,
    private _coursesDetailsServiceProxy: CourseDetailsServiceProxy,
    private _modalService: BsModalService,
    cd: ChangeDetectorRef,
    private route: ActivatedRoute
  ) {
    super(injector, cd);
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      this.id = Number(params.get("id"));

      this._coursesDetailsServiceProxy
        .getCourseDetailsByCourseId(undefined, 0, 10, this.id)
        .subscribe((result: CourseDtoPagedResultDto) => {
          this.courseDetails = result.items || [];
          this.showPaging(result, 1);
          this.cd.detectChanges();
        });
    });
  }

  create(): void {
    this.showCreateOrEditCourseDetailsDialog();
  }

  edit(courseDetails: CourseDetailsDto): void {
    this.showCreateOrEditCourseDetailsDialog(courseDetails.id);
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
    this._coursesDetailsServiceProxy
      .getCourseDetailsByCourseId(
        this.keyword,
        request.skipCount,
        request.maxResultCount,
        this.id
      )
      .pipe(finalize(() => finishedCallback()))
      .subscribe((result: CourseDetailsDtoPagedResultDto) => {
        this.courseDetails = result.items || [];
        this.showPaging(result, pageNumber);
        this.cd.detectChanges();
      });
  }

  protected delete(courseDetails: CourseDetailsDto): void {
    const displayName = courseDetails.nameAr || "";
    abp.message.confirm(
      this.l("TrainingUnitDetailDeleteWarningMessage", displayName),
      undefined,
      (confirmed: boolean) => {
        if (confirmed) {
          this._coursesDetailsServiceProxy
            .deleteCourseDetails(courseDetails.id)
            .subscribe(() => {
              abp.notify.success(this.l("SuccessfullyDeleted"));
              this.refresh();
            });
        }
      }
    );
  }

  private showCreateOrEditCourseDetailsDialog(id?: number): void {
    let modalRef: BsModalRef;

    if (!id) {
      modalRef = this._modalService.show(
        CourseTrainingCourseDetailsCreateComponent,
        {
          class: "modal-lg",
        }
      );
    } else {
      modalRef = this._modalService.show(
        CourseTrainingCourseDetailEditComponent,
        {
          class: "modal-lg",
          initialState: { id: id },
        }
      );
    }

    modalRef.content?.onSave.subscribe(() => this.refresh());
  }

  trackById(index: number, item: ContentDto): number {
    return item.id;
  }
}
