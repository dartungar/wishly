import {Component, OnInit} from '@angular/core';
import {AuthService} from "../auth/auth.service";


@Component({
  selector: 'app-root',
  standalone: false,
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  constructor(public authService: AuthService) {
  }

  async ngOnInit(): Promise<void> {
    await this.authService.tryGetUserFromCognitoAuthenticatorCookies();
  }

}
