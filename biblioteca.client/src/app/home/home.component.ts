import { Component, OnInit } from '@angular/core';
import { UsersServicesService } from '../servicios/users/users-services.service';
import { EntityUserBooks } from '../models/entities/entity-user-books';
import { UsersResponseSingle } from '../models/Responses/users-response-single';
import { Router } from '@angular/router';
import { EntityUser } from '../models/entities/entity-user';
import Swal from 'sweetalert2';
import { ErrorResponse } from '../models/Responses/error-response';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {  
  public errorResponse = {} as ErrorResponse;
  public myBooks: EntityUserBooks[] = [];
  public user = {} as EntityUser;
  public usuario: string = "";
  isAdmin: boolean = false;
  constructor(private service: UsersServicesService, private router: Router) { }
  ngOnInit(): void {
    this.user = JSON.parse(sessionStorage.getItem('user') || '{}');
    let books = [];
    this.service.GetUsersPermissions(this.user.user).subscribe((response) => {
      if (response.data.attributes.isAdmin) {
        this.isAdmin = response.data.attributes.isAdmin;
      }
    });
    this.usuario = this.user.fullName;
    this.GetBooksByUser();
  }

  GetBooksByUser() {
    this.service.GetBooksByUser(this.user.user).subscribe(
      (response) => {
        if (response.data != null && response.data.attributes.length > 0) {
          this.myBooks = response.data.attributes;
        }
      },
      (error) => {
        this.errorResponse = error;
        Swal.fire({
          icon: "error",
          title: this.errorResponse.errors[0].title,
          text: this.errorResponse.errors[0].title
        });
      }
    );
  }

  DeliveryBook(apa:string) {
    this.service.GetDeliverBooks(this.user.user, apa).subscribe(
      (response) => {
        Swal.fire({
          icon: "success",
          title: response.data.attributes.message
        });
        this.myBooks = [];
        this.GetBooksByUser();
      },
      (errorsResponse) => {
        Swal.fire({
          icon: "error",
          title: this.errorResponse.errors[0].title,
          text: this.errorResponse.errors[0].title
        });
        this.myBooks = [];
        this.GetBooksByUser();
      }
    );
  }

  Borrows() {
    this.router.navigateByUrl('/library');
  }
  CerrarSesion() {
    sessionStorage.clear();
    this.router.navigateByUrl('/');
  }
}
