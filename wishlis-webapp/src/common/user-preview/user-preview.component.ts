import {Component, Input} from '@angular/core';
import {User} from "../../user/user";
import {AsyncPipe, DatePipe, NgIf} from "@angular/common";
import {RouterLink} from "@angular/router";
import {CardModule} from "primeng/card";
import {FavoriteButtonComponent} from "../../favorite-users/favorite-button/favorite-button.component";
import {AuthService} from "../../auth/auth.service";

@Component({
  selector: 'app-user-preview',
  standalone: true,
  imports: [
    DatePipe,
    RouterLink,
    CardModule,
    FavoriteButtonComponent,
    AsyncPipe,
    NgIf
  ],
  templateUrl: './user-preview.component.html',
  styleUrl: './user-preview.component.css'
})
export class UserPreviewComponent {
  @Input() user: User;
  @Input() isFavorite: boolean;

  constructor(public authService: AuthService) {
  }
}
