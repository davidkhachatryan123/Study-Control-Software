import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { MatCardModule } from '@angular/material/card'
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatMenuModule } from '@angular/material/menu';
import { MatBadgeModule } from '@angular/material/badge';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';

import { AuthLayoutComponent } from './auth/auth-layout.component';
import { DashboardLayoutComponent, ToolBarComponent, UserComponent, SidenavComponent, ActionsNewComponent } from './dashboard';
import { DeleteDialogComponent } from './dashboard/dialogs';

@NgModule({
  declarations: [
    AuthLayoutComponent,
    DashboardLayoutComponent,
    ToolBarComponent,
    UserComponent,
    SidenavComponent,
    ActionsNewComponent,
    DeleteDialogComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    MatCardModule,
    MatToolbarModule,
    MatSidenavModule,
    MatButtonModule,
    MatIconModule,
    MatListModule,
    MatExpansionModule,
    MatMenuModule,
    MatBadgeModule,
    MatFormFieldModule,
    MatInputModule,
    MatDialogModule,
  ],
  exports: [
    AuthLayoutComponent,
    DashboardLayoutComponent,
    ActionsNewComponent,
    DeleteDialogComponent,
    
  ],
})
export class SharedModule { }
