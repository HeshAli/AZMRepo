import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { HomeBannersRoutingModule } from "./home-banners-routing.module";
import { HomeBannersComponent } from "./home-banners.component";
import { CreateHomeBannerDialogComponent } from "./create-home-banner/create-home-banner-dialog/create-home-banner-dialog.component";
import { EditHomeBannerDialogComponent } from "./edit-home-banner/edit-home-banner-dialog/edit-home-banner-dialog.component";
import { SharedModule } from "@shared/shared.module";

@NgModule({
  declarations: [
    HomeBannersComponent,
    CreateHomeBannerDialogComponent,
    EditHomeBannerDialogComponent,
  ],
  imports: [CommonModule, FormsModule, HomeBannersRoutingModule, SharedModule],
})
export class HomeBannersModule {}
