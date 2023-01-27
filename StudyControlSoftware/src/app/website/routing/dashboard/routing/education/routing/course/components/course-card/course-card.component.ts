import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard-course-card',
  templateUrl: 'course-card.component.html'
})

export class CourseCardComponent implements OnInit {
  displayedColumns: string[] = [ 'name', 'description', 'actions' ];

  constructor() { }

  ngOnInit() { }
}