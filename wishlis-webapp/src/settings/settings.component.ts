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

  constructor(public userService: UserService, public authService: AuthService) {  }

  ngOnInit() {
    this.authService.authenticatedUser$.subscribe(user => {
      if (user) {
        this.user = user;
      }
    }
  );
    if (!this.user)
      return; // todo: show error
    // @ts-ignore
    this.userService.getUser(this.user.id).subscribe((user: User) => {
      this.user = user;
    });
  }

  updateSettings() {
    this.userService.updateUser(this.user).subscribe();
  }

  protected readonly currencies = currencies;
}
