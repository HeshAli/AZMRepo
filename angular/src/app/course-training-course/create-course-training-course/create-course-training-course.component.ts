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
  ContentServiceProxy,
  ContentDto,
  CreateContentDto,
  CreateCourseDto,
  CourseServiceProxy,
} from "../../../shared/service-proxies/service-proxies";

@Component({
  templateUrl: "./create-course-training-course.component.html",
})
export class CreateCourseTrainingComponent
  extends AppComponentBase
  implements OnInit
{
  saving = false;
  course = new CreateCourseDto();

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _courseServiceProxy: CourseServiceProxy,
    public bsModalRef: BsModalRef,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {}

  save(): void {
    this.saving = true;
    this._courseServiceProxy.createCourse(this.course).subscribe(
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
