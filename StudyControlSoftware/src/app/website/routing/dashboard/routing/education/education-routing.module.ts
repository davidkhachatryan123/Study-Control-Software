import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { appRoutes } from 'src/app/website/consts';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'prefix',
    redirectTo: appRoutes.DASHBOARD_EDUCATION_FACULTY
  },
  {
    path: appRoutes.DASHBOARD_EDUCATION_FACULTY,
  },
  {
    path: appRoutes.DASHBOARD_EDUCATION_COURSE,
  }
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ],
})
export class EducationRoutingModule { }
