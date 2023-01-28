import { Component, EventEmitter, Output } from '@angular/core';
import {animate, state, style, transition, trigger} from '@angular/animations';

import { Faculty } from '../../../../models';

@Component({
  selector: 'app-dashboard-faculity-card',
  templateUrl: 'faculty-card.component.html',
  styleUrls: [ 'faculty-card.component.css' ],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
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