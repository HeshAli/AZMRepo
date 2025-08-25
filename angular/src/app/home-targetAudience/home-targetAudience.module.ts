import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; 
import { SharedModule } from '@shared/shared.module';
 
 
import { CreateTargetAudienceDialogComponent } from './create-home-targetAudience/create-home-targetAudience-dialog/create-targetAudience-dialog.component';
import { EditTargetAudienceDialogComponent } from './edit-home-targetAudience/edit-home-targetAudience-dialog/edit-targetAudience-dialog.component';
import { TargetAudienceComponent } from './home-targetAudience.component';
import { HomeTargetAudienceRoutingModule } from './home-targetAudience-routing.module';

@NgModule({
  declarations: [
    TargetAudienceComponent,
    CreateTargetAudienceDialogComponent,
    EditTargetAudienceDialogComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
      HomeTargetAudienceRoutingModule,
    SharedModule
  ]
})
export class TargetAudienceModule { }
