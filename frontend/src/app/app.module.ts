import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { CreateUserComponent } from './create-user/create-user.component';
import { WorkoutsComponent } from './workouts/workouts.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    CreateUserComponent,
    WorkoutsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
