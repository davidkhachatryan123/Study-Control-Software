import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppUser } from 'src/app/website/routing/auth/models';
import { Observable } from 'rxjs';

import { AuthService } from 'src/app/website/routing/auth/services';
import { appRoutes } from 'src/app/website/consts';
import { StorageService } from 'src/app/website/services';

@Component({
  selector: 'app-dashboard-toolbar-user',
  templateUrl: 'user.component.html',
  styleUrls: [ 'user.component.css' ]
})

export class UserComponent implements OnInit {
  public routers: typeof appRoutes = appRoutes;
  public user: AppUser | undefined = new AppUser('', '', '');

  constructor(
    private storageService: StorageService,
    private router: Router
  ) { }

  ngOnInit() {
    this.user = this.storageService.getUser();
  }

  signOut() {
    this.storageService.clean();
    this.router.navigate([this.routers.AUTH]);
  }
}