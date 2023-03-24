import { Component, ElementRef, EventEmitter, Inject, Output, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

import { Lecturer } from 'src/app/website/routing/dashboard/routing/users/models';

@Component({
  selector: 'app-dashboard-course-set-lecturer',
  templateUrl: 'set-lecturer.component.html'
})
export class SetLecturerDialogComponent {
  @Output() onSubmit = new EventEmitter<Lecturer>();

  lecturerCtrl = new FormControl('');
  filteredLecturers: Observable<Lecturer[]>;
  lecturer: Lecturer | null;

  allԼecturers: Lecturer[] = [];

  @ViewChild('lecturerInput') lecturerInput: ElementRef<HTMLInputElement>;

  constructor(
    @Inject(MAT_DIALOG_DATA) public dialogData: any
  ) {
    this.allԼecturers = dialogData.allԼecturers;

    this.filteredLecturers = this.lecturerCtrl.valueChanges.pipe(
      startWith(null),
      map((lecturer: string | null) =>
      (lecturer ? this._filter(lecturer) : this.allԼecturers.slice())),
    );
  }

  remove() {
    this.lecturer = null;
  }

  selected(event: MatAutocompleteSelectedEvent) {
    this.lecturer = this.allԼecturers.find(lecturer => lecturer.id == event.option.value);

    this.lecturerInput.nativeElement.value = '';
    this.lecturerCtrl.setValue(null);
  }

  onSubmitEvent() {
    this.onSubmit.emit(this.lecturer);
  }

  private _filter(value: string): Lecturer[] {
    try {
      const filterValue = value.toLowerCase();

      return this.allԼecturers.filter(lecturer => lecturer.firstName + ' ' +  lecturer.lastName.toLowerCase().includes(filterValue));
    }
    catch {}
  }
}