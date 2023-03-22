import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { appRoutes } from 'src/app/website/consts';

import { AuthGuard } from './routing/auth/guards';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'prefix',
    redirectTo: appRoutes.DASHBOARD
  },
  {
    path: appRoutes.AUTH,
    loadChildren: () => import('./routing/auth/auth.module').then(module => module.AuthModule)
  },
  {
    path: appRoutes.DASHBOARD,
    canActivate: [AuthGuard],
    loadChildren: () => import('./routing/dashboard/dashboard.module').then(module => module.DashboardModule)
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [],
})
export class WebsiteRoutingModule { }
