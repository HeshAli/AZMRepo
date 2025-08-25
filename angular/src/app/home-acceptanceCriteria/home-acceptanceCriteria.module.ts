import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; 
import { SharedModule } from '@shared/shared.module';
import { AcceptanceCriteriaComponent } from './home-acceptanceCriteria.component';
import { CreateAcceptanceCriteriaDialogComponent } from './create-home-acceptanceCriteria/create-home-acceptanceCriteria-dialog/create-acceptanceCriteria-dialog.component';
import { EditAcceptanceCriteriaDialogComponent } from './edit-home-acceptanceCriteria/edit-home-acceptanceCriteria-dialog/edit-acceptanceCriteria-dialog.component';
import { HomeAcceptanceCriteriaRoutingModule } from './home-acceptanceCriteria-routing.module';
 
 
 

@NgModule({
  declarations: [
    AcceptanceCriteriaComponent,
    CreateAcceptanceCriteriaDialogComponent,
    EditAcceptanceCriteriaDialogComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
      HomeAcceptanceCriteriaRoutingModule,
    SharedModule
  ]
})
export class AcceptanceCriteriaModule { }
