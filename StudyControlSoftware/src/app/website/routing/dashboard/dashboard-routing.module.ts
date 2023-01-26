import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { appRoutes } from 'src/app/website/consts';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'prefix',
    redirectTo: appRoutes.DASHBOARD_EDUCATION
  },
  {
    path: appRoutes.DASHBOARD_EDUCATION,
    loadChildren: () => import('./routing/education/education.module').then(module => module.EducationModule)
  },
  {
    path: appRoutes.DASHBOARD_USERS,
    loadChildren: () => import('./routing/users/users.module').then(module => module.UsersModule)
  },
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ],
})
export class DashboardRoutingModule { }
