import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; 
import { SharedModule } from '@shared/shared.module';
import { AboutAzmComponent } from './about-azm.component';
import { CreateAboutAzmDialogComponent } from './create-about-azm/create-about-azm-dialog/create-about-azm-dialog.component';
import { EditAboutAzmDialogComponent } from './edit-about-azm/edit-about-azm-dialog/edit-about-azm-dialog.component';
import { AboutAzmRoutingModule } from './about-azm-routing.module';
 
 
 

@NgModule({
  declarations: [
    AboutAzmComponent,
    CreateAboutAzmDialogComponent,
    EditAboutAzmDialogComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
      AboutAzmRoutingModule,
    SharedModule
  ]
})
export class AboutAzmModule { }
