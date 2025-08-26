import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';  
import { TargetAudienceComponent } from './home-targetAudience.component';

const routes: Routes = [
  {
    path: '',
        component: TargetAudienceComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeTargetAudienceRoutingModule { }
