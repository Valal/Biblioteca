import { Component, OnInit } from '@angular/core';
import { UsersServicesService } from '../servicios/users/users-services.service';
import { EntityUser } from '../models/entities/entity-user';
import { BooksServicesService } from '../servicios/books/books-services.service';
import { EntityBooks } from '../models/entities/entity-books';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { ErrorResponse } from '../models/Responses/error-response';

@Component({
  selector: 'app-library',
  templateUrl: './library.component.html',
  styleUrl: './library.component.css'
})
export class LibraryComponent implements OnInit {
  public user = {} as EntityUser;
  public books: EntityBooks[] = [];
  public errorResponse = {} as ErrorResponse;
  public usuario: string = "";
  constructor(private service: UsersServicesService, private booksService: BooksServicesService, private router: Router) { }
  ngOnInit(): void {
    this.user = JSON.parse(sessionStorage.getItem('user') || '{}');
    this.GetBooks('');
    this.usuario = this.user.fullName;
  }

  BorrowBook(apa: string) {
    this.service.GetRequestBooks(this.user.user, apa).subscribe(
      (response) => {
        Swal.fire({
          icon: "success",
          title: response.data.attributes.message
        });
      },
      (errorsResponse) => {
        this.errorResponse = errorsResponse.error;
        Swal.fire({
          icon: "error",
          title: this.errorResponse.errors[0].title,
          text: this.errorResponse.errors[0].detail
        });
      });
    this.books = [];
    this.GetBooks('');
  }

  GetBooks(apa: string) {
    if (apa != '') {
      this.booksService.GetByApa(apa).subscribe(
        (response) => {
        this.books = response.data.attributes;
        },
        (errorsResponse) => {
          this.errorResponse = errorsResponse.error;
          Swal.fire({
            icon: "error",
            title: this.errorResponse.errors[0].title,
            text: this.errorResponse.errors[0].detail
          });
          let txtBuscarLibro: HTMLInputElement;
          txtBuscarLibro = document.getElementById("txtBuscarLibro") as HTMLInputElement;
          txtBuscarLibro.value = "";
        });
    }
    else {
      this.booksService.Get('',0).subscribe((response) => {
        this.books = response.data.attributes;
      }); }
  }

  Return() {
    this.router.navigateByUrl('/home');
  }

  CerrarSesion() {
    sessionStorage.clear();
    this.router.navigateByUrl('/');
  }
}
