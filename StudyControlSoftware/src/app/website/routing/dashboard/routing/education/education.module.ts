import { NgModule } from '@angular/core';

import { EducationRoutingModule } from './education-routing.module';

import { SharedModule } from 'src/app/website/shared/shared.module';

@NgModule({
  imports: [
    EducationRoutingModule,
    SharedModule
  ],
})
export class EducationModule { }
