import {Component} from '@angular/core';
import {UserService} from "../user/user.service";

@Component({
  selector: 'app-settings',
  standalone: true,
  imports: [],
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.css'
})
export class SettingsComponent {
  constructor(public userService: UserService) {
  }
}
