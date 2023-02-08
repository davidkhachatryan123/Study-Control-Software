import { Component, ElementRef, EventEmitter, Output, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
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

  // Get this values in ngOnInit() function
  allFaculty: Faculty[] = [
    new Faculty(1, 'Ինֆորմատիկայի ֆակուլտետ'),
    new Faculty(2, 'Պատմության ֆակուլտետ'),
    new Faculty(3, 'Լեզվաբանական ֆակուլտետ'),
  ];

  @ViewChild('facultyInput') facultyInput: ElementRef<HTMLInputElement>;

  constructor() {
    this.filteredFacultys = this.facultyCtrl.valueChanges.pipe(
      startWith(null),
      map((faculty: string | null) =>
      (faculty ? this._filter(faculty) : this.allFaculty.slice())),
    );
  }

  remove() {
    this.faculty = undefined;
  }

  selected(event: MatAutocompleteSelectedEvent) {
    this.faculty = this.allFaculty.find(faculty => faculty.id == event.option.value);

    this.facultyInput.nativeElement.value = '';
    this.facultyCtrl.setValue(null);
  }

  onSubmitEvent() {
    this.onSubmit.emit(this.faculty);
  }

  private _filter(value: string): Faculty[] {
    try {
      const filterValue = value.toLowerCase();

      return this.allFaculty.filter(faculty => faculty.name.toLowerCase().includes(filterValue));
    }
    catch {}
  }
}