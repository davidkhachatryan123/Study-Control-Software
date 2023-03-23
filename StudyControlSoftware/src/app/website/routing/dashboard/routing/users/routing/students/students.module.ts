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
import { MatRadioModule } from '@angular/material/radio';

import { SharedModule } from 'src/app/website/shared/shared.module';

import { StudentsComponent } from './students.component';
import { NewDialogComponent } from './dialogs';
import { StudentsCardComponent } from './components/students-card/students-card.component';
import { SetFacultyComponent } from './components/set-faculty/set-faculty.component';
import { SetFacultyDialogComponent } from './dialogs/set-faculty/set-faulty-dialog.component';
import { StudentsService } from './services/students.service';

const routes: Routes = [
  {
    path: '',
    component: StudentsComponent
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
    MatRadioModule,
  ],
  declarations: [
    StudentsComponent,
    StudentsCardComponent,
    NewDialogComponent,
    SetFacultyComponent,
    SetFacultyDialogComponent
  ],
  providers: [
    StudentsService
  ]
})
export class StudentsModule { }
