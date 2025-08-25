import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';  
 
import { AcceptanceCriteriaComponent } from './home-acceptanceCriteria.component';

const routes: Routes = [
  {
    path: '',
        component: AcceptanceCriteriaComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeAcceptanceCriteriaRoutingModule { }
