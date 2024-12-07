import {Component, OnInit} from '@angular/core';
import {UserService} from "../user/user.service";
import {User} from "../user/user";
import {FormsModule} from "@angular/forms";
import {CheckboxModule} from "primeng/checkbox";
import {CalendarModule} from "primeng/calendar";
import {InputTextModule} from "primeng/inputtext";
import {AuthService} from "../auth/auth.service";
import {DropdownModule} from "primeng/dropdown";
import {currencies} from "../common/currencies";
import {NotificationService} from "../common/notification.service";

@Component({
  selector: 'app-settings',
  standalone: true,
  imports: [
    FormsModule,
    CheckboxModule,
    CalendarModule,
    InputTextModule,
    DropdownModule
  ],
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.css'
})
export class SettingsComponent implements OnInit {
  user: User;

  constructor(public userService: UserService, private notificationService: NotificationService) {  }

  ngOnInit() {
    this.userService.authenticatedUser$.subscribe(user => {
      if (user) {
        this.user = user;
      }
    }
  );
    if (!this.user) {
      this.notificationService.showError("Error loading settings", "Error loading user settings.");
      return;
    }
    // @ts-ignore
    this.userService.getUser(this.user.id).subscribe((user: User) => {
      this.user = user;
    });
  }

  updateSettings() {
    this.userService.updateUser(this.user).subscribe(
      _ => {
        // TODO: update settings
        this.notificationService.showSuccess("Settings updated", "Your settings has been updated.");
      });
  }

  protected readonly currencies = currencies;
}
