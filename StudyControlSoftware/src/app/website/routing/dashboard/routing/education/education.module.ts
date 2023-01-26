import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/website/shared/shared.module';

import { EducationRoutingModule } from './education-routing.module';
import { TestComponent } from './test/test.component';

@NgModule({
  imports: [
    EducationRoutingModule,
    SharedModule
  ],
  declarations: [ TestComponent ]
})
export class EducationModule { }
