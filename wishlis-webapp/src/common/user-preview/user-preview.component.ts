import {Component, Input} from '@angular/core';
import {User} from "../../user/user";
import {DatePipe} from "@angular/common";
import {RouterLink} from "@angular/router";
import {CardModule} from "primeng/card";
import {UserService} from "../user.service";

@Component({
  selector: 'app-user-preview',
  standalone: true,
  imports: [
    DatePipe,
    RouterLink,
    CardModule
  ],
  templateUrl: './user-preview.component.html',
  styleUrl: './user-preview.component.css'
})
export class UserPreviewComponent {
  @Input() user: User;

  constructor(private userService: UserService) {
  }
}
