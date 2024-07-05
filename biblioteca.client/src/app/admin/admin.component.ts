import { Component, OnInit } from '@angular/core';
import { BooksServicesService } from '../servicios/books/books-services.service';
import { EntityBooks } from '../models/entities/entity-books';
import { EntityBooksYears } from '../models/entities/entity-books-years';
import { EntityUser } from '../models/entities/entity-user';
import { Router } from '@angular/router';
import { ErrorResponse } from '../models/Responses/error-response';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css'
})
export class AdminComponent implements OnInit {
  constructor(private service: BooksServicesService, private router:Router) { }
  public errorResponse = {} as ErrorResponse;
  public years: EntityBooksYears[] = [];
  selectYear = {} as HTMLInputElement;
  public books: EntityBooks[] = [];
  public user = {} as EntityUser;
  public usuario: string = "";
  slYears: any;

  ngOnInit(): void {
    this.user = JSON.parse(sessionStorage.getItem('user') || '{}');
    this.usuario = this.user.fullName;
    this.GetYears();
    this.GetBooks();
  }

  GetBooksByParams(apa: HTMLInputElement) {
    this.service.Get(apa.value, this.slYears).subscribe(
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
        this.books = [];
        this.Clean();
        this.GetBooks();
      })
  }

  GetBooks() {
    this.service.Get('', 0).subscribe((response) => { this.books = response.data.attributes; })
  }

  GetYears() {
    this.service.GetYears().subscribe((response) => {
      this.years = response.data.attributes
    });
  }

  DeleteBook(apa: string) {
    Swal.fire({
      icon: "warning",
      title: "¿Seguro que deseas eliminar el libro?",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Eliminar!"
    }).then(
      (result) => {
        if (result.isConfirmed) {
          this.service.DeleteBook(apa).subscribe(
            (response) => {
              Swal.fire({
                icon: "success",
                title: response.data.attributes.message
              });
              this.books = [];
              this.years = [];
              this.GetBooks();
              this.GetYears();
            },
            (errorsResponse) => {
              this.errorResponse = errorsResponse.error;
              Swal.fire({
                icon: "error",
                title: this.errorResponse.errors[0].title,
                text: this.errorResponse.errors[0].detail
              });
            }
          );
        }
      });
  }

  Clean() {
    let txtApa: HTMLInputElement;
    this.books = [];
    this.slYears = 0;
    txtApa = document.getElementById("txtApa") as HTMLInputElement;
    txtApa.value = "";
    
  }
  Back() {
    this.router.navigateByUrl('/home');
  }

  AddBook(name: HTMLInputElement, lastNames: HTMLInputElement, title: HTMLInputElement, year: HTMLInputElement, place: HTMLInputElement, editorial: HTMLInputElement, availables: HTMLInputElement) {
    this.service.CreateBook(this.CreateBookRequest(name.value, lastNames.value, title.value, Number(year.value), place.value, editorial.value, Number(availables.value))).subscribe(
      (response) => {
        Swal.fire({
          icon: "success",
          title: response.data.attributes.message
        });
        this.CleanAddBook();
        this.GetBooks();
        let btnDismiss: HTMLButtonElement;
        btnDismiss = document.getElementById("btnCloseModal") as HTMLButtonElement;
        btnDismiss.click();
      },
      (errorsResponse) => {
        this.CleanAddBook();
        this.errorResponse = errorsResponse.error;
        Swal.fire({
          icon: "error",
          title: this.errorResponse.errors[0].title,
          text: this.errorResponse.errors[0].detail
        });
        this.GetBooks();
      });
  }

  CleanAddBook() {
    let txtTitle: HTMLInputElement;
    let txtName: HTMLInputElement;
    let txtLastNames: HTMLInputElement;
    let txtYear: HTMLInputElement;
    let txtAvailables: HTMLInputElement;
    let txtPlace: HTMLInputElement;
    let txtEditorial: HTMLInputElement;

    txtTitle = document.getElementById("txtTitle") as HTMLInputElement;
    txtName = document.getElementById("txtName") as HTMLInputElement;
    txtLastNames = document.getElementById("txtLastNames") as HTMLInputElement;
    txtYear = document.getElementById("txtYear") as HTMLInputElement;
    txtAvailables = document.getElementById("txtAvailables") as HTMLInputElement;
    txtPlace = document.getElementById("txtPlace") as HTMLInputElement;
    txtEditorial = document.getElementById("txtEditorial") as HTMLInputElement;

    txtTitle.value = "";
    txtName.value = "";
    txtLastNames.value = "";
    txtYear.value = "";
    txtAvailables.value = "";
    txtPlace.value = "";
    txtEditorial.value = "";
  }

  CerrarSesion() {
    sessionStorage.clear();
    this.router.navigateByUrl('/');
  }

  private CreateBookRequest(_name: string, _lastNames: string, _title: string, _year: number, _place: string, _editorial: string, _availables: number): string {
    let request = {
      type: "libros",
      data: {
        name: _name,
        lastNames: _lastNames,
        title: _title,
        year: _year,
        place: _place,
        editorial: _editorial,
        availables: _availables
      }
    };
    return JSON.stringify(request);
  }
}
