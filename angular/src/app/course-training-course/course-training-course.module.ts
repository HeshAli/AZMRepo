import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { SharedModule } from "../../shared/shared.module";
import { TrainingCoursesRoutingModule } from "./course-training-course-routing.module";
import { CreateCourseTrainingComponent } from "./create-course-training-course/create-course-training-course.component";
import { EditCourseTrainingComponent } from "./edit-course-training-course/edit-course-training-course.component";
import { TrainingCoursesComponent } from "./course-training-course.component";

@NgModule({
  declarations: [
    TrainingCoursesComponent,
    CreateCourseTrainingComponent,
    EditCourseTrainingComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    TrainingCoursesRoutingModule,
    SharedModule,
  ],
})
export class TrainingCoursesModule {}
