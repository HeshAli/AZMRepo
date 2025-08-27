import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { SharedModule } from "../../shared/shared.module";
import { ServiceProxyModule } from "@shared/service-proxies/service-proxy.module";
import { TrainingCoursesRoutingModule } from "./course-training-course-routing.module";
import { CreateCourseTrainingComponent } from "./create-course-training-course/create-course-training-course.component";
import { EditCourseTrainingComponent } from "./edit-course-training-course/edit-course-training-course.component";
import { TrainingCoursesComponent } from "./course-training-course.component";
import { CourseTrainingCourseDetailsComponent } from "./course-training-course-details/course-training-course-details.component";
import { CourseTrainingCourseDetailEditComponent } from "./course-training-course-details/course-training-course-detail-edit/course-training-course-detail-edit.component";
import { CourseTrainingCourseDetailsCreateComponent } from "./course-training-course-details/course-training-course-details-create/course-training-course-details-create.component";

@NgModule({
  declarations: [
    TrainingCoursesComponent,
    CreateCourseTrainingComponent,
    EditCourseTrainingComponent,
    CourseTrainingCourseDetailsComponent,
    CourseTrainingCourseDetailEditComponent,
    CourseTrainingCourseDetailsCreateComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    TrainingCoursesRoutingModule,
    SharedModule,
    ServiceProxyModule,
  ],
})
export class TrainingCoursesModule {}
