import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';  
import { ProgressStepsComponent } from './home-progressSteps.component';

const routes: Routes = [
  {
    path: '',
        component: ProgressStepsComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeprogressStepsRoutingModule { }
