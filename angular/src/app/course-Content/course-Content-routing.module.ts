import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';   
import { CourseContentComponent } from './course-Content.component';

const routes: Routes = [
  {
    path: '',
        component: CourseContentComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeCourseContentRoutingModule { }
