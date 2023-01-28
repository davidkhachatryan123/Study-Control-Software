import { NgModule } from '@angular/core';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { SharedModule } from '../../shared/shared.module';

import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';

import { AdminGuard } from './guards';


import { TestComponent } from './test/test.component';

@NgModule({
  imports: [
    DashboardRoutingModule,
    SharedModule,
    MatDialogModule,
    MatButtonModule
  ],
  declarations: [
    TestComponent
  ],
  providers: [
    AdminGuard
  ]
})
export class DashboardModule { }
