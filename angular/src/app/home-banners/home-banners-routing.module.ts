import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeBannersComponent } from './home-banners.component';

const routes: Routes = [
  {
    path: '',
    component: HomeBannersComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeBannersRoutingModule { }
