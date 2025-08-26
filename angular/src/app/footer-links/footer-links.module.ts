import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; 
import { SharedModule } from '@shared/shared.module';
import { ProgressContentComponent } from './home-progressContent.component';
import { CreateProgressContentDialogComponent } from './create-home-progressContent/create-home-progressContent-dialog/create-progressContent-dialog.component';
import { EditProgressContentDialogComponent } from './edit-home-progressContent/edit-home-progressContent-dialog/edit-progressContent-dialog.component';
import { HomeProgressContentRoutingModule } from '../home-progressContent/home-progressContent-routing.module';
 
 
 

@NgModule({
  declarations: [
    ProgressContentComponent,
    CreateProgressContentDialogComponent,
    EditProgressContentDialogComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
      HomeProgressContentRoutingModule,
    SharedModule
  ]
})
export class ProgressContentModule { }
