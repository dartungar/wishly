import {Component, OnInit} from '@angular/core';
import {RoutingModule} from "../../app/routing.module";
import {AuthService} from "../../auth/auth.service";
import {Router} from "@angular/router";
import {NgIf} from "@angular/common";
import {MenuItem} from "primeng/api";
import {MenubarModule} from "primeng/menubar";


@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RoutingModule, NgIf, MenubarModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {
  items: MenuItem[] | undefined;
  public isAuthenticated = false;

  constructor(public authService: AuthService, public router:
    Router) {
  }

  ngOnInit(): void {
    this.authService.isAuthenticated$.subscribe(isAuthenticated => {
      this.isAuthenticated = isAuthenticated;
      console.log(this.isAuthenticated);
      this.updateMenuItems();
    });
  }

  private updateMenuItems() {
    this.items = [
      {
        icon: "pi pi-home",
        label: "Home",
        routerLink: "/me",
        visible: this.isAuthenticated
      },
      {
        icon: "pi pi-home",
        label: "Search Users",
        routerLink: "/search"
      },
      {
        icon: "pi pi-home",
        label: "Sign In",
        routerLink: "/auth/sign-in",
        visible: !this.isAuthenticated
      },
      {
        icon: "pi pi-home",
        label: "Create Account",
        routerLink: "/auth/sign-up",
        visible: !this.isAuthenticated
      },
      {
        icon: "pi pi-home",
        label: "Settings",
        routerLink: "/settings",
        visible: this.isAuthenticated
      },
      {
        icon: "pi pi-home",
        label: "Sign Out",
        visible: this.isAuthenticated,
        command: async () => {
          await this.authService.signOut();
        }
      },
    ];
    console.log(this.items);
  }
}
