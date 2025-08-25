import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HomeTrainingAzmRoutingModule } from './home-trainingAzm-routing.module';
import { SharedModule } from '@shared/shared.module';
import { CreateTrainingAzmDialogComponent } from './create-home-trainingAzm/create-home-trainingAzm-dialog/create-TrainingAzm-dialog.component';
import { EditTrainingAzmDialogComponent } from './edit-home-banner/edit-home-trainingAzm-dialog/edit-TrainingAzm-dialog.component';
import { TrainingAzmComponent } from './home-trainingAzm.component';

@NgModule({
  declarations: [
    TrainingAzmComponent,
    CreateTrainingAzmDialogComponent,
    EditTrainingAzmDialogComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
      HomeTrainingAzmRoutingModule,
    SharedModule
  ]
})
export class TrainingAzmModule { }
