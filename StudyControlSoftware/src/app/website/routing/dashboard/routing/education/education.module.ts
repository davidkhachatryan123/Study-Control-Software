import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/website/shared/shared.module';

import { EducationRoutingModule } from './education-routing.module';

@NgModule({
  imports: [
    EducationRoutingModule,
    SharedModule
  ],
})
export class EducationModule { }
