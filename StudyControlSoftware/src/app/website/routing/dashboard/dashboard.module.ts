import { NgModule } from '@angular/core';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { SharedModule } from '../../shared/shared.module';

import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';

import { LanguageService } from './services';
import { UsersManagmentService } from './pages/users/services';

import { DeleteDialogComponent } from 'src/app/website/routing/dashboard/dialogs';

import { AdminGuard } from './guards';

@NgModule({
  imports: [
    DashboardRoutingModule,
    SharedModule,
    MatDialogModule,
    MatButtonModule
  ],
  declarations: [
    DeleteDialogComponent,
  ],
  providers: [
    LanguageService,
    UsersManagmentService,
    AdminGuard
  ]
})
export class DashboardModule { }
