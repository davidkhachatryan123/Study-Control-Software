import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent, RegisterComponent } from './components';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'prefix',
    redirectTo: 'login'
  },
  {
    path: 'register',
    component: RegisterComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  }
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ],
})
export class AuthRoutingModule { }
