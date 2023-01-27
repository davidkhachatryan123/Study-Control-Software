import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';

import { SharedModule } from 'src/app/website/shared/shared.module';

import { CourseComponent } from './course.component';
import { CourseCardComponent } from './components/course-card/course-card.component';

const routes: Routes = [
  {
    path: '',
    component: CourseComponent
  }
];

@NgModule({
  imports: [
    SharedModule,
    RouterModule.forChild(routes),
    MatCardModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatButtonModule,
  ],
  declarations: [
    CourseComponent,
    CourseCardComponent
  ]
})
export class CourseModule { }
