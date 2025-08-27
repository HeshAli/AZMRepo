import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { TrainingCoursesComponent } from "./course-training-course.component";
import { CreateCourseTrainingComponent } from "./create-course-training-course/create-course-training-course.component";
import { EditCourseTrainingComponent } from "./edit-course-training-course/edit-course-training-course.component";
import { CourseTrainingCourseDetailsComponent } from "./course-training-course-details/course-training-course-details.component";

const routes: Routes = [
  { path: "", component: TrainingCoursesComponent },
  { path: "create", component: CreateCourseTrainingComponent },
  { path: "edit/:id", component: EditCourseTrainingComponent },
  { path: ":id/details", component: CourseTrainingCourseDetailsComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TrainingCoursesRoutingModule {}
