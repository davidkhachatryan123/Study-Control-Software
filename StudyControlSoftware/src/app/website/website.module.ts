import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';

import { SharedModule } from './shared/shared.module';
import { AuthModule } from './routing/auth/auth.module';
import { WebsiteRoutingModule } from './website-routing.module';
import { StorageService } from './services';

@NgModule({
  imports: [
    WebsiteRoutingModule,
    SharedModule,
    AuthModule,
    BrowserAnimationsModule,
    HttpClientModule,
  ],
  declarations: [],
  providers: [
    StorageService
  ],
})
export class WebSiteModule { }
