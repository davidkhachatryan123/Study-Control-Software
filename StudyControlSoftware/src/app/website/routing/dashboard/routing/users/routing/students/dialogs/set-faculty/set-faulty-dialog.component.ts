import { Component, ElementRef, EventEmitter, Inject, Output, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

import { Faculty } from '../../../../../education/models';

@Component({
  selector: 'app-dashboard-students-set-faculty',
  templateUrl: 'set-faulty-dialog.component.html'
})
export class SetFacultyDialogComponent {
  @Output() onSubmit = new EventEmitter<Faculty>();

  facultyCtrl = new FormControl('');
  filteredFacultys: Observable<Faculty[]>;
  faculty: Faculty | undefined;

  allFaculties: Faculty[] = [];

  @ViewChild('facultyInput') facultyInput: ElementRef<HTMLInputElement>;

  constructor(
    @Inject(MAT_DIALOG_DATA) public dialogData: any
  ) {
    this.allFaculties = dialogData.allFaculties;

    this.filteredFacultys = this.facultyCtrl.valueChanges.pipe(
      startWith(null),
      map((faculty: string | null) =>
      (faculty ? this._filter(faculty) : this.allFaculties.slice())),
    );
  }

  remove() {
    this.faculty = undefined;
  }

  selected(event: MatAutocompleteSelectedEvent) {
    this.faculty = this.allFaculties.find(faculty => faculty.id == event.option.value);

    this.facultyInput.nativeElement.value = '';
    this.facultyCtrl.setValue(null);
  }

  onSubmitEvent() {
    this.onSubmit.emit(this.faculty);
  }

  private _filter(value: string): Faculty[] {
    try {
      const filterValue = value.toLowerCase();

      return this.allFaculties.filter(faculty => faculty.name.toLowerCase().includes(filterValue));
    }
    catch {}
  }
}