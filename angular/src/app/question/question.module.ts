import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { QuestionRoutingModule } from './question-routing.module';
import { SharedModule } from '@shared/shared.module'; 
import { QuestionComponent } from './question.component';
import { CreateQuestionDialogComponent } from './create-question/create-question-dialog/create-question-dialog.component';
import { EditQuestionDialogComponent } from './edit-question/edit-question-dialog/edit-question-dialog.component';
@NgModule({
  declarations: [
    QuestionComponent,
    CreateQuestionDialogComponent,
    EditQuestionDialogComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
      QuestionRoutingModule,
    SharedModule
  ]
})
export class QuestionModule { }
