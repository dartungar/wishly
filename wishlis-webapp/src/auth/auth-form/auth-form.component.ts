import {Component, OnInit} from '@angular/core';
import {AuthService} from "../auth.service";
import {ActivatedRoute, Router} from "@angular/router";
import {AuthModule} from "../auth.module";
import {AmplifyAuthenticatorModule, AuthenticatorService} from "@aws-amplify/ui-angular";

@Component({
  selector: 'app-auth-form',
  standalone: false,
  templateUrl: './auth-form.component.html',
  styleUrl: './auth-form.component.css'
})
export class AuthFormComponent implements OnInit {

  constructor(public authService: AuthService, private authenticator: AuthenticatorService , private route: ActivatedRoute, private router: Router) {
  }

  ngOnInit(): void {
    // Retrieve the path from the URL
    this.route.url.subscribe(urlSegments => {
      const path = urlSegments.join('/');
      if (path === 'auth/sign-up') {
        this.authenticator.toSignUp();
      }
      else if (path === "auth/sign-in" || path === "auth") {
        this.authenticator.toSignIn();
      }
    });
  }
}
