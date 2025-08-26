import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; 
import { SharedModule } from '@shared/shared.module'; 
import { CourseContentComponent } from './course-Content.component';
import { CreateCourseContentDialogComponent } from './create-course-Content/create-course-Content-dialog/create-course-Content-dialog.component';
import { EditCourseContentDialogComponent } from './edit-course-Content/edit-course-Content-dialog/edit-course-Content-dialog.component';
import { HomeCourseContentRoutingModule } from './course-Content-routing.module';
 
 
 

@NgModule({
  declarations: [
    CourseContentComponent,
    CreateCourseContentDialogComponent,
    EditCourseContentDialogComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
      HomeCourseContentRoutingModule,
    SharedModule
  ]
})
export class CourseContentModule { }
