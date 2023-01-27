import { Component } from '@angular/core';

@Component({
  selector: 'app-dashboard-faculty',
  templateUrl: 'faculty.component.html'
})
export class FacultyComponent {
  onNew() {
    console.log("New faculity create dialog!");
  }
}