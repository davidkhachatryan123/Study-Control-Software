import { Component, EventEmitter, Output } from '@angular/core';

import { Course } from 'src/app/website/routing/dashboard/routing/education/models';
import { tableDetailExpand } from 'src/app/website/shared/dashboard/animations';

@Component({
  selector: 'app-dashboard-course-card',
  templateUrl: 'course-card.component.html',
  styleUrls: [ 'course-card.component.css' ],
  animations: [
    tableDetailExpand
  ],
})
export class CourseCardComponent {
  @Output() onEdit = new EventEmitter<Course>;
  @Output() onDelete = new EventEmitter<Course>;

  columnsToDisplay: string[] = [ 'name', 'description', 'actions' ];
  columnsToDisplayWithExpand = [...this.columnsToDisplay, 'expand'];

  expandedElement: Course | null;

  data: Course[] = [
    new Course(1, 'C# ծրագրավորում', 'Սովորում ենք գրել ծրագրեր օգտագործելով C# ծրագրավորման լեզուն'),
    new Course(2, 'C++ ծրագրավորում', 'Սովորում ենք գրել ծրագրեր օգտագործելով C++ ծրագրավորման լեզուն'),
    new Course(3, 'ASP.NET Core', 'Սովորում ենք գրել ծրագրեր օգտագործելով ASP.NET Core framework-ը')
  ];

  onEditEvent(element: Course) {
    this.onEdit.emit(element);
  }

  onDeleteEvent(element: Course) {
    this.onDelete.emit(element);
  }
}