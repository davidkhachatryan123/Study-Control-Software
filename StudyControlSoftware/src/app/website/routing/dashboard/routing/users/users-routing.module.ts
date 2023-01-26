import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { appRoutes } from 'src/app/website/consts';


import { TestComponent } from '../../test/test.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'prefix',
    redirectTo: appRoutes.DASHBOARD_USERS_LECTURERS
  },
  {
    path: appRoutes.DASHBOARD_USERS_LECTURERS,
    component: TestComponent
  },
  {
    path: appRoutes.DASHBOARD_USERS_STUDENTS,
    component: TestComponent
  },
  {
    path: appRoutes.DASHBOARD_USERS_ADMINS,
    component: TestComponent
  }
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ],
})
export class UsersRoutingModule { }
