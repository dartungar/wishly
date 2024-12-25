import {Component, OnInit} from '@angular/core';
import {UserService} from "../user/user.service";
import {User} from "../user/user";
import {FormsModule} from "@angular/forms";
import {CheckboxModule} from "primeng/checkbox";
import {CalendarModule} from "primeng/calendar";
import {InputTextModule} from "primeng/inputtext";
import {DropdownModule} from "primeng/dropdown";
import {currencies} from "../common/currencies";
import {NotificationService} from "../common/notification.service";
import {catchError, EMPTY, take} from "rxjs";

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

  constructor(public userService: UserService, private notificationService: NotificationService) {
  }

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
    this.userService.updateUser(this.user)
      .pipe(take(1),
        catchError(e => {
        this.notificationService.showError("Error", "Errow while trying save the settings.");
        return EMPTY;}))
      .subscribe(_ => this.notificationService.showSuccess("Success", "Settings saved successfully."));
  }

  protected readonly currencies = currencies;
}
