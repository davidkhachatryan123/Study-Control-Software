import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';
import { MatChipsModule } from '@angular/material/chips';
import { MatAutocompleteModule } from '@angular/material/autocomplete';

import { SharedModule } from 'src/app/website/shared/shared.module';

import { CourseComponent } from './course.component';
import { CourseCardComponent } from './components/course-card/course-card.component';
import { AddLecturerComponent } from './components/add-lecturer/add-lecturer.component';
import { SetLecturerDialogComponent } from './dialogs';
import { NewDialogComponent } from './dialogs/new/new.component';

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
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    MatCardModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatButtonModule,
    MatIconModule,
    MatListModule,
    MatFormFieldModule,
    MatInputModule,
    MatDialogModule,
    MatChipsModule,
    MatAutocompleteModule,
  ],
  declarations: [
    CourseComponent,
    CourseCardComponent,
    AddLecturerComponent,
    SetLecturerDialogComponent,
    NewDialogComponent
  ]
})
export class CourseModule { }
