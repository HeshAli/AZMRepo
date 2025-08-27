import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { TrainingCoursesComponent } from "./course-training-course.component";

const routes: Routes = [
  {
    path: "",
    component: TrainingCoursesComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TrainingCoursesRoutingModule {}
