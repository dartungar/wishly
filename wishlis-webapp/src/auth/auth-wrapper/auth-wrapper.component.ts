import {Component } from '@angular/core';
import {AuthService} from "../auth.service";

@Component({
  selector: 'app-auth-wrapper',
  standalone: false,
  templateUrl: './auth-wrapper.component.html',
  styleUrl: './auth-wrapper.component.css',
})
export class AuthWrapperComponent {
  constructor(public authService: AuthService) {

  }
}
