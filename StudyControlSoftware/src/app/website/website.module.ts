import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';

import { SharedModule } from './shared/shared.module';
import { AuthModule } from './routing/auth/auth.module';
import { WebsiteRoutingModule } from './website-routing.module';

import { StorageService } from './services';
import { JwtModule } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';

export function tokenGetter() {
  return window.localStorage.getItem(environment.sessionStorageConfig.TOKEN_KEY);
}

@NgModule({
  imports: [
    WebsiteRoutingModule,
    SharedModule,
    AuthModule,
    BrowserAnimationsModule,
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: [environment.config.api],
        disallowedRoutes: []
      }
    })
  ],
  providers: [
    StorageService
  ],
})
export class WebSiteModule { }
