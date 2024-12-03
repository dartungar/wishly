import {Component, Input} from '@angular/core';
import {User} from "../../user/user";
import {DatePipe} from "@angular/common";
import {RouterLink} from "@angular/router";
import {CardModule} from "primeng/card";

@Component({
  selector: 'app-search-result',
  standalone: true,
  imports: [
    DatePipe,
    RouterLink,
    CardModule
  ],
  templateUrl: './search-result.component.html',
  styleUrl: './search-result.component.css'
})
export class SearchResultComponent {
  @Input() user: User;
}
