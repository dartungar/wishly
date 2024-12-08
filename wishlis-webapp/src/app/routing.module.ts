import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {routes} from "./app.routes";
import {LocationStrategy, PathLocationStrategy} from "@angular/common";

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    bindToComponentInputs: true,
    onSameUrlNavigation: 'reload',
    paramsInheritanceStrategy: 'always',
    urlUpdateStrategy: 'eager'
  })],
  exports: [RouterModule],
  providers: [
    { provide: LocationStrategy, useClass: PathLocationStrategy }
  ]
})
export class RoutingModule { }
