import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { FacultyComponent } from './faculty.component';

import { SharedModule } from 'src/app/website/shared/shared.module';

const routes: Routes = [
  {
    path: '',
    component: FacultyComponent
  }
];

@NgModule({
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],
  declarations: [
    FacultyComponent
  ]
})
export class FacultyModule { }
