import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Amplify } from 'aws-amplify';
import outputs from '../../amplify_outputs.json';

import { AmplifyAuthenticatorModule, AuthenticatorService } from '@aws-amplify/ui-angular';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  imports: [RouterOutlet, AmplifyAuthenticatorModule],
})
export class AppComponent {
  title = 'app';


  constructor(public authenticator: AuthenticatorService) {
    Amplify.configure(outputs);
  }
}
