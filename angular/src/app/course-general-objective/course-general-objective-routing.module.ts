import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { CourseGeneralObjectiveComponent } from "./course-general-objective.component";

const routes: Routes = [
  {
    path: "",
    component: CourseGeneralObjectiveComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CourseGeneralObjectiveRoutingModule {}
