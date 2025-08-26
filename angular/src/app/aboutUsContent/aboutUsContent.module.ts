import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; 
import { SharedModule } from '@shared/shared.module';
 
import { AboutUsContentRoutingModule } from './aboutUsContent-routing.module';
import { AboutUsContentComponent } from './aboutUsContent.component';
import { CreateaboutUsContentDialogComponent } from './create-aboutUsContentContent/create-aboutUsContent-dialog/create-aboutUsContent-dialog.component';
import { EditAboutUsContenDialogComponent } from './edit-aboutUsContent/edit-aboutUsContent-dialog/edit-aboutUsContent-dialog.component';
 
 
 

@NgModule({
  declarations: [
        AboutUsContentComponent,
        CreateaboutUsContentDialogComponent,
    EditAboutUsContenDialogComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
      AboutUsContentRoutingModule,
    SharedModule
  ]
})
export class AboutContentModule { }
