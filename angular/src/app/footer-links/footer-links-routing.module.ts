import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';   
import { ProgressContentComponent } from './home-progressContent.component';

const routes: Routes = [
  {
    path: '',
        component: ProgressContentComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeProgressContentRoutingModule { }
