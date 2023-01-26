import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { appRoutes } from 'src/app/website/consts';


import { TestComponent } from '../../test/test.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'prefix',
    redirectTo: appRoutes.DASHBOARD_EDUCATION_FACULTY
  },
  {
    path: appRoutes.DASHBOARD_EDUCATION_FACULTY,
    component: TestComponent
  },
  {
    path: appRoutes.DASHBOARD_EDUCATION_COURSE,
    component: TestComponent
  }
];
@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ],
})
export class EducationRoutingModule { }
