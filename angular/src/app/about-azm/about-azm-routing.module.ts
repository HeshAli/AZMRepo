import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';   
import { AboutAzmComponent } from './about-azm.component';

const routes: Routes = [
  {
    path: '',
        component: AboutAzmComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AboutAzmRoutingModule { }
