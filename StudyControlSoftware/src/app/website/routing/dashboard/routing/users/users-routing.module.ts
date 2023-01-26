import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { appRoutes } from 'src/app/website/consts';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'prefix',
    redirectTo: appRoutes.DASHBOARD_USERS_LECTURERS
  },
  {
    path: appRoutes.DASHBOARD_USERS_LECTURERS,
  },
  {
    path: appRoutes.DASHBOARD_USERS_STUDENTS,
  },
  {
    path: appRoutes.DASHBOARD_USERS_ADMINS,
  }
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ],
})
export class UsersRoutingModule { }
