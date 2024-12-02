import {Component, OnInit} from '@angular/core';
import {RoutingModule} from "../../app/routing.module";
import {AuthService} from "../../auth/auth.service";
import {Router} from "@angular/router";
import {NgIf} from "@angular/common";
import {MenuItem} from "primeng/api";
import {MenubarModule} from "primeng/menubar";
import {DarkModeSwitchComponent} from "../dark-mode-switch/dark-mode-switch.component";


@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RoutingModule, NgIf, MenubarModule, DarkModeSwitchComponent],
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
      this.updateMenuItems();
    });
  }

  private updateMenuItems() {
    this.items = [
      {
        icon: "pi pi-home",
        label: "Home",
        route: "/me",
        visible: this.isAuthenticated
      },
      {
        icon: "pi pi-search",
        label: "Search Users",
        route: "/search"
      },
      {
        icon: "pi-sign-in",
        label: "Sign In",
        route: "/auth/sign-in",
        visible: !this.isAuthenticated
      },
      {
        icon: "pi-user-plus",
        label: "Create Account",
        route: "/auth/sign-up",
        visible: !this.isAuthenticated
      },
      {
        icon: "pi pi-cog",
        label: "Settings",
        route: "/settings",
        visible: this.isAuthenticated
      },
      {
        icon: "pi pi-sign-out",
        label: "Sign Out",
        visible: this.isAuthenticated,
        command: async () => {
          await this.authService.signOut();
        }
      },
    ];
  }
}
