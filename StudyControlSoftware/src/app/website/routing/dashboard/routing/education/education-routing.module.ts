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
    loadChildren: () => import('./routing/faculty/faculty.module').then(module => module.FacultyModule)
  },
  {
    path: appRoutes.DASHBOARD_EDUCATION_COURSE,
    loadChildren: () => import('./routing/course/course.module').then(module => module.CourseModule)
  }
];
@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ],
})
export class EducationRoutingModule { }
