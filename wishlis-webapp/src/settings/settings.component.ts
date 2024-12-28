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
import {NgIf} from "@angular/common";
import {ActivatedRoute} from "@angular/router";
import {MessageModule} from "primeng/message";
import {CardModule} from "primeng/card";
import {ConfirmationService} from "primeng/api";
import {WishlistItemsService} from "../wishlist/wishlist-items.service";
import {ConfirmDialogModule} from "primeng/confirmdialog";

@Component({
  selector: 'app-settings',
  standalone: true,
  imports: [
    FormsModule,
    CheckboxModule,
    CalendarModule,
    InputTextModule,
    DropdownModule,
    MessageModule,
    NgIf,
    CardModule,
    ConfirmDialogModule,
  ],
  providers: [ConfirmationService],
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.css'
})
export class SettingsComponent implements OnInit {
  currencyCodeBeforeChange: string;
  user: User;
  showOnboardingMessage: boolean = false;

  constructor(public userService: UserService,
              private itemsService: WishlistItemsService,
              private notificationService: NotificationService,
              private confirmationService: ConfirmationService,
              private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.showOnboardingMessage = this.route.snapshot.queryParams['isNewUser'] === 'true';

    this.userService.authenticatedUser$.pipe(take(1)).subscribe(user => {
        if (user) {
          this.user = user;
          this.currencyCodeBeforeChange = user.currencyCode;
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
    console.log('Before update - Current:', this.user.currencyCode, 'Original:', this.currencyCodeBeforeChange);
    this.userService.updateUser(this.user)
      .pipe(
        take(1),
        catchError(e => {
            this.notificationService.showError("Error", "Errow while trying save the settings.");
            return EMPTY;
          }
        )
      )
      .subscribe(_ => {
        this.notificationService.showSuccess("Success", "Settings saved successfully.");
        console.log('After update - Current:', this.user.currencyCode, 'Original:', this.currencyCodeBeforeChange);
        if (this.user.currencyCode !== this.currencyCodeBeforeChange) {
          console.log("currency code has been changed...")
          this.confirmationService.confirm({
            message: 'You have changed your preferred currency. Do you want to set it to items in your wishlist?',
            header: 'The currency has been changed',
            icon: 'pi pi-dollar',
            accept: () => {
              this.itemsService.updateAllItemsCurrency(this.user.id, this.user.currencyCode).subscribe(_ =>
                this.notificationService.showSuccess("Success", "Updated currency for items in the wishlist. Refresh the page if changes are not displayed."))
            }
          });
        }
      });
  }

  protected readonly currencies = currencies;
}
