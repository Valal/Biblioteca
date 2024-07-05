import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { AdminComponent } from './admin/admin.component';
import { LibraryComponent } from './library/library.component';
import { HomeComponent } from './home/home.component';
import { UsersServicesService } from './servicios/users/users-services.service';
import { BooksServicesService } from './servicios/books/books-services.service';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    AdminComponent,
    LibraryComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule, FormsModule,
    NgbModule
  ],
  providers: [UsersServicesService, BooksServicesService],
  bootstrap: [AppComponent]
})
export class AppModule { }
