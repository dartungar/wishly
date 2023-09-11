import { Component, OnInit } from '@angular/core';
import {AuthService} from "../../auth/auth.service";
import {tap} from "rxjs";

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  authorized = this.authService.isAuthorized;
  constructor(public authService: AuthService) {
    this.authorized.subscribe(val => console.log(val));
  }

  ngOnInit(): void {
  }

}
