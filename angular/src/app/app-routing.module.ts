import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { AppRouteGuard } from "@shared/auth/auth-route-guard";
import { AppComponent } from "./app.component";

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: "",
        component: AppComponent,
        children: [
          {
            path: "home",
            loadChildren: () =>
              import("./home/home.module").then((m) => m.HomeModule),
            canActivate: [AppRouteGuard],
          },

          {
            path: "users",
            loadChildren: () =>
              import("./users/users.module").then((m) => m.UsersModule),
            data: { permission: "Pages.Users" },
            canActivate: [AppRouteGuard],
          },
          {
            path: "home-banners",
            loadChildren: () =>
              import("./home-banners/home-banners.module").then(
                (m) => m.HomeBannersModule
              ),
            /*  data: { permission: 'Pages.Roles' },*/
            canActivate: [AppRouteGuard],
          },
          {
            path: "home-gallery",
            loadChildren: () =>
              import("./home-gallery/home-gallery.module").then(
                (m) => m.HomeGalleryModule
              ),
            /*  data: { permission: 'Pages.Roles' },*/
            canActivate: [AppRouteGuard],
          },
          {
            path: "trainingAzm",
            loadChildren: () =>
              import("./home-trainingAzm/home-trainingAzm.module").then(
                (m) => m.TrainingAzmModule
              ),
            /*  data: { permission: 'Pages.Roles' },*/
            canActivate: [AppRouteGuard],
            },
            {
                path: "targetAudience",
                loadChildren: () =>
                    import("./home-targetAudience/home-targetAudience.module").then(
                        (m) => m.TargetAudienceModule
                    ),
                /*  data: { permission: 'Pages.Roles' },*/
                canActivate: [AppRouteGuard],
            },
            {
                path: "progressContent",
                loadChildren: () =>
                    import("./home-progressContent/home-progressContent.module").then(
                        (m) => m.ProgressContentModule
                    ),
                /*  data: { permission: 'Pages.Roles' },*/
                canActivate: [AppRouteGuard],
            },
            {
                path: "progressSteps",
                loadChildren: () =>
                    import("./home-progressSteps/home-progressSteps.module").then(
                        (m) => m.ProgressStepsModule
                    ),
                /*  data: { permission: 'Pages.Roles' },*/
                canActivate: [AppRouteGuard],
            },
            {
                path: "acceptanceCriteria",
                loadChildren: () =>
                    import("./home-acceptanceCriteria/home-acceptanceCriteria.module").then(
                        (m) => m.AcceptanceCriteriaModule
                    ),
                /*  data: { permission: 'Pages.Roles' },*/
                canActivate: [AppRouteGuard],
            },
            {
                path: "courseContent",
                loadChildren: () =>
                    import("./course-Content/course-Content.module").then(
                        (m) => m.CourseContentModule
                    ),
                /*  data: { permission: 'Pages.Roles' },*/
                canActivate: [AppRouteGuard],
            },
          {
            path: "roles",
            loadChildren: () =>
              import("./roles/roles.module").then((m) => m.RolesModule),
            data: { permission: "Pages.Roles" },
            canActivate: [AppRouteGuard],
          },
          {
            path: "tenants",
            loadChildren: () =>
              import("./tenants/tenants.module").then((m) => m.TenantsModule),
            data: { permission: "Pages.Tenants" },
            canActivate: [AppRouteGuard],
          },
          {
            path: "update-password",
            loadChildren: () =>
              import("./users/users.module").then((m) => m.UsersModule),
            canActivate: [AppRouteGuard],
          },
        ],
      },
    ]),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
