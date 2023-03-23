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

import { FacultyComponent } from './faculty.component';
import { FaculityCardComponent } from './components/faculty-card/faculty-card.component';
import { AddCourseListComponent } from './components/add-course-list/add-course-list.component';
import { NewDialogComponent, AddCourseDialogComponent } from './dialogs';
import { FacultyService } from './services';

const routes: Routes = [
  {
    path: '',
    component: FacultyComponent
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
    FacultyComponent,
    FaculityCardComponent,
    AddCourseListComponent,
    NewDialogComponent,
    AddCourseDialogComponent
  ],
  providers: [
    FacultyService
  ]
})
export class FacultyModule { }
