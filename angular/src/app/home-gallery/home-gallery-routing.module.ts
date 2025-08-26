import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { HomeGalleryComponent } from "./home-gallery.component";

const routes: Routes = [
  {
    path: "",
    component: HomeGalleryComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class HomeGalleryRoutingModule {}
