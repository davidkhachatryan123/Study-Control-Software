import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard-faculity-card',
  templateUrl: 'faculty-card.component.html'
})

export class FaculityCardComponent implements OnInit {
  displayedColumns: string[] = [ 'name', 'actions' ];

  constructor() { }

  ngOnInit() { }
}