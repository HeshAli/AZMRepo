import { ChangeDetectorRef, Component, Injector, OnInit } from "@angular/core";
import { BsModalRef } from "ngx-bootstrap/modal";
import { AppComponentBase } from "../../../../shared/app-component-base";
import {
  CourseDetailsServiceProxy,
  CourseDetailsDto,
} from "../../../../shared/service-proxies/service-proxies";

@Component({
  templateUrl: "./course-training-course-detail-edit.component.html",
})
export class CourseTrainingCourseDetailEditComponent
  extends AppComponentBase
  implements OnInit
{
  saving = false;
  detail: CourseDetailsDto = new CourseDetailsDto();
  id: number;
  constructor(
    injector: Injector,
    public bsModalRef: BsModalRef,
    private detailService: CourseDetailsServiceProxy,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
  }
  ngOnInit(): void {
    this.detailService.getCourseDetails(this.id).subscribe((result) => {
      this.detail = result;
      this.cd.detectChanges();
    });
  }

  save(): void {
    this.saving = true;

    this.detailService.updateCourseDetails(this.detail).subscribe(() => {
      this.notify.info(this.l("SavedSuccessfully"));
      this.bsModalRef.hide();
    });
  }

  deleteDetail(): void {
    if (!this.detail.id) return;

    this.message.confirm(this.l("AreYouSureToDelete"), undefined, (result) => {
      if (result) {
        this.detailService
          .deleteCourseDetails(this.detail.id!)
          .subscribe(() => {
            this.notify.info(this.l("SuccessfullyDeleted"));
            this.bsModalRef.hide();
          });
      }
    });
  }
}
