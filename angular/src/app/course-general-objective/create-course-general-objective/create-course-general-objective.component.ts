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
} from "../../../shared/service-proxies/service-proxies";

@Component({
  templateUrl: "./create-course-general-objective.component.html",
})
export class CreateCourseGeneralObjectiveComponent
  extends AppComponentBase
  implements OnInit
{
  saving = false;
  content = new CreateContentDto();

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _contentServiceProxy: ContentServiceProxy,
    public bsModalRef: BsModalRef,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {}

  save(): void {
    this.saving = true;
    this.content.categoryCode = "Course_GeneralObjectives";
    this._contentServiceProxy.createContent(this.content).subscribe(
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
