import { Component, ViewChild } from '@angular/core';
import { MatStepper } from '@angular/material/stepper'

@Component({
  selector: 'app-login',
  templateUrl: 'login.component.html',
  styleUrls: [ 'login.component.css' ]
})
export class LoginComponent {
  @ViewChild('stepper') private stepper: MatStepper;

  next() {
    this.stepper.next();
  }
}