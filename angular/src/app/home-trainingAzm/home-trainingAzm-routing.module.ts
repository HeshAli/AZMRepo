import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router'; 
import { TrainingAzmComponent } from './home-trainingAzm.component';

const routes: Routes = [
  {
    path: '',
        component: TrainingAzmComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeTrainingAzmRoutingModule { }
