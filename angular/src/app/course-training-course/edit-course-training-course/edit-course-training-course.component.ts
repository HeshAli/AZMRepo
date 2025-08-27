import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
  ChangeDetectorRef,
} from "@angular/core";
import { BsModalRef } from "ngx-bootstrap/modal";
import { AppComponentBase } from "../../../shared/app-component-base";
import {
  HomeBannerServiceProxy,
  HomeBannerDto,
  UpdateContentDto,
  ContentDto,
  ContentServiceProxy,
  UpdateCourseDto,
  CourseServiceProxy,
} from "../../../shared/service-proxies/service-proxies";
import { NgForm } from "@angular/forms";

@Component({
  templateUrl: "./edit-course-training-course.component.html",
})
export class EditCourseTrainingComponent
  extends AppComponentBase
  implements OnInit
{
  saving = false;
  course = new UpdateCourseDto();
  id: number;

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _courseServiceProxy: CourseServiceProxy,
    public bsModalRef: BsModalRef,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._courseServiceProxy.getCourse(this.id).subscribe((result) => {
      this.course = result;
      this.cd.detectChanges();
    });
  }

  save(): void {
    this.saving = true;

    this._courseServiceProxy.updateCourse(this.course).subscribe(
      () => {
        this.notify.info(this.l("SavedSuccessfully"));
        this.bsModalRef.hide();
        this.onSave.emit();
      },
      () => {
        this.saving = false;
      }
    );
  }
}
