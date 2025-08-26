import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; 
import { SharedModule } from '@shared/shared.module';
import { ProgressStepsComponent } from './home-progressSteps.component';
import { CreateProgressStepsDialogComponent } from './create-home-progressSteps/create-home-progressSteps-dialog/create-progressSteps-dialog.component';
import { HomeprogressStepsRoutingModule } from './home-progressSteps-routing.module';
import { EditProgressStepsAzmDialogComponent } from './edit-home-progressSteps/edit-home-progressSteps-dialog/edit-progressSteps-dialog.component';
 

@NgModule({
  declarations: [
    ProgressStepsComponent,
    CreateProgressStepsDialogComponent,
        EditProgressStepsAzmDialogComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
      HomeprogressStepsRoutingModule,
    SharedModule
  ]
})
export class ProgressStepsModule { }
