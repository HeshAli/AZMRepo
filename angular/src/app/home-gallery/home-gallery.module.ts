import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { SharedModule } from "../../shared/shared.module";
import { HomeGalleryComponent } from "./home-gallery.component";
import { CreateHomeGalleryComponent } from "./create-home-gallery/create-home-gallery.component";
import { EditHomeGalleryComponent } from "./edit-home-gallery/edit-home-gallery.component";
import { HomeGalleryRoutingModule } from "./home-gallery-routing.module";

@NgModule({
  declarations: [
    HomeGalleryComponent,
    CreateHomeGalleryComponent,
    EditHomeGalleryComponent,
  ],
  imports: [CommonModule, FormsModule, HomeGalleryRoutingModule, SharedModule],
})
export class HomeGalleryModule {}
