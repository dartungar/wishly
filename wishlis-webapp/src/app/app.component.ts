import { Component } from '@angular/core';
import {AmplifyAuthenticatorModule} from "@aws-amplify/ui-angular";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    AmplifyAuthenticatorModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {

}
