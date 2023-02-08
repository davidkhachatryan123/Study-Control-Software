import { NgModule } from '@angular/core';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { SharedModule } from '../../shared/shared.module';

import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';

import { AdminGuard } from './guards';

@NgModule({
  imports: [
    DashboardRoutingModule,
    SharedModule,
    MatDialogModule,
    MatButtonModule
  ],
  providers: [
    AdminGuard
  ]
})
export class DashboardModule { }
