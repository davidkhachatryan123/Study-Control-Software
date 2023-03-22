import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { StorageService } from 'src/app/website/services';
import { AppUser } from 'src/app/website/routing/auth/models';

import { appRoutes } from 'src/app/website/consts';
import { roles } from 'src/app/website/routing/auth/models';

@Component({
  selector: 'app-dashboard-sidenav',
  templateUrl: 'sidenav.component.html',
  styleUrls: [ 'sidenav.component.css' ]
})

export class SidenavComponent {
  routers: typeof appRoutes = appRoutes;
  route: string;

  constructor(
    private router: Router,
  ) {
    this.route = router.url;
  }
}