import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import {DashboardComponent} from "./components/dashboard/dashboard.component";
import {ListComponent} from "./components/list/list.component";
import {EditorComponent} from "./components/editor/editor.component";

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    ListComponent,
    EditorComponent,

  ],
    bootstrap: [AppComponent], imports: [BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    FormsModule,
    RouterModule.forRoot([
      {path: '', component: DashboardComponent, pathMatch: 'full'}
    ]), ReactiveFormsModule], providers: [provideHttpClient(withInterceptorsFromDi())] })
export class AppModule { }
