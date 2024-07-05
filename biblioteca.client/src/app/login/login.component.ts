import { Component, OnInit } from '@angular/core';
import { UsersServicesService } from '../servicios/users/users-services.service';
import { Router } from '@angular/router';
import { ErrorResponse } from '../models/Responses/error-response';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  providers: [UsersServicesService]
})
export class LoginComponent implements OnInit {
  constructor(private service: UsersServicesService, private router: Router) { }
  public errorResponse = {} as ErrorResponse;

  ngOnInit() { }

  Login(user: HTMLInputElement, password: HTMLInputElement) {
    this.service.Login(user.value, password.value).subscribe(
      (response: any) => {
        sessionStorage.setItem('user', JSON.stringify(response.data.attributes));
        this.router.navigateByUrl('/home');
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
}
