import { Component, EventEmitter, Output } from '@angular/core';

import { tableDetailExpand } from 'src/app/website/shared/dashboard/animations';

import { Faculty } from '../../../../models';

@Component({
  selector: 'app-dashboard-faculity-card',
  templateUrl: 'faculty-card.component.html',
  styleUrls: [ 'faculty-card.component.css' ],
  animations: [
    tableDetailExpand
  ],
})
export class FaculityCardComponent {
  @Output() onEdit = new EventEmitter<Faculty>;
  @Output() onDelete = new EventEmitter<Faculty>;

  columnsToDisplay: string[] = [ 'name', 'actions' ];
  columnsToDisplayWithExpand = [...this.columnsToDisplay, 'expand'];

  expandedElement: Faculty | null;

  data: Faculty[] = [
    new Faculty(1, 'Ինֆորմատիկայի ֆակուլտետ'),
    new Faculty(2, 'Պատմության ֆակուլտետ'),
    new Faculty(3, 'Լեզվաբանական ֆակուլտետ'),
  ];

  onEditEvent(element: Faculty) {
    this.onEdit.emit(element);
  }

  onDeleteEvent(element: Faculty) {
    this.onDelete.emit(element);
  }
}