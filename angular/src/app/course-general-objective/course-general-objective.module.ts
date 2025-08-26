import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { SharedModule } from "../../shared/shared.module";

import { CreateCourseGeneralObjectiveComponent } from "./create-course-general-objective/create-course-general-objective.component";
import { EditCourseGeneralObjectiveComponent } from "./edit-course-general-objective/edit-course-general-objective.component";
import { CourseGeneralObjectiveComponent } from "./course-general-objective.component";
import { CourseGeneralObjectiveRoutingModule } from "./course-general-objective-routing.module";

@NgModule({
  declarations: [
    CourseGeneralObjectiveComponent,
    CreateCourseGeneralObjectiveComponent,
    EditCourseGeneralObjectiveComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    SharedModule,
    CourseGeneralObjectiveRoutingModule,
  ],
})
export class GeneralObjectivesModule {}
