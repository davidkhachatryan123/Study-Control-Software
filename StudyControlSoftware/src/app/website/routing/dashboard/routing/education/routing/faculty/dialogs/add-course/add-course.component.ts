import { Component, ElementRef, EventEmitter, Inject, Output, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { Course } from '../../../../models';

@Component({
  selector: 'app-dashboard-faculty-new',
  templateUrl: 'add-course.component.html'
})
export class AddCourseDialogComponent {
  @Output() onSubmit = new EventEmitter<Course[]>();

  courseCtrl = new FormControl('');
  filteredCourses: Observable<string[]>;
  courses: Course[] = [];

  allCourses: Course[] = [];

  @ViewChild('courseInput') courseInput: ElementRef<HTMLInputElement>;

  constructor(
    @Inject(MAT_DIALOG_DATA) public dialogData: any
  ) {
    this.allCourses = dialogData.allCourses;

    this.filteredCourses = this.courseCtrl.valueChanges.pipe(
      startWith(null),
      map((course: string | null) =>
      (course ? this._filter(course) : this.allCourses.map(data => data.title).slice())),
    );
  }

  remove(course: Course): void {
    const index = this.courses.indexOf(course);

    if (index >= 0) {
      this.courses.splice(index, 1);
    }
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    this.courses.push(new Course(
      this.allCourses.find(course => course.title == event.option.value).id,
      event.option.value,
      ''));

    this.courseInput.nativeElement.value = '';
    this.courseCtrl.setValue(null);
  }

  onSubmitEvent() {
    this.onSubmit.emit(this.courses);
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.allCourses.map(course => course.title)
    .filter(course => course.toLowerCase().includes(filterValue));
  }
}