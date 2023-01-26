import { NgModule } from '@angular/core';

import { UsersRoutingModule } from './users-routing.module';

import { SharedModule } from 'src/app/website/shared/shared.module';

@NgModule({
  imports: [
    UsersRoutingModule,
    SharedModule
  ],
})
export class UsersModule { }
