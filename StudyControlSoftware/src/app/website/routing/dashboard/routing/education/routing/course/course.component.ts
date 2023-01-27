import { Component } from '@angular/core';

@Component({
  selector: 'app-dashboard-course',
  templateUrl: 'course.component.html'
})
export class CourseComponent {
  onNew() {
    console.log("New course create dialog!");
  }
}