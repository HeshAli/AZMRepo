import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';  
import { AboutUsContentComponent } from './aboutUsContent.component';

const routes: Routes = [
  {
    path: '',
        component: AboutUsContentComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AboutUsContentRoutingModule { }
