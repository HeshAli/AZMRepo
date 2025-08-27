import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
  ChangeDetectorRef,
} from "@angular/core";
import { BsModalRef } from "ngx-bootstrap/modal";
import { AppComponentBase } from "../../../../shared/app-component-base";
import {
  CreateCourseDto,
  CourseDetailsServiceProxy,
} from "../../../../shared/service-proxies/service-proxies";

@Component({
  templateUrl: "./course-training-course-details-create.component.html",
  selector: "app-create-course-training",
})
export class CourseTrainingCourseDetailsCreateComponent
  extends AppComponentBase
  implements OnInit
{
  saving = false;
  course = new CreateCourseDto();

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _courseDetailsServiceProxy: CourseDetailsServiceProxy,
    public bsModalRef: BsModalRef,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {}

  save(): void {
    this.saving = true;
    this._courseDetailsServiceProxy.createCourseDetails(this.course).subscribe(
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
