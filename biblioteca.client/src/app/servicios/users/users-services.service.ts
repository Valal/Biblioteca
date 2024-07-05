import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UsersResponseSingle } from '../../models/Responses/users-response-single';
import { Observable } from 'rxjs';
import { UsersResponse } from '../../models/Responses/users-response';
import { BooksByUserResponse } from '../../models/Responses/books-by-user-response';
import { ResultResponse } from '../../models/Responses/result-response';
import { UsersAdminResponse } from '../../models/Responses/users-admin-response';

@Injectable({
  providedIn: 'root'
})
export class UsersServicesService {

  constructor(private http: HttpClient) { }
  private headers = new HttpHeaders({ 'Content-type': 'application/json', 'Accept': 'application/json' });

  Login(user: string, password: string): Observable<UsersResponseSingle> {
    return this.http.post<UsersResponseSingle>('/users/login', JSON.stringify(this.LoginObject(user, password)), { headers: this.headers });
  }

  private LoginObject(user: string, password: string) {
    return {
      type: "usuario",
      attributes: { password: password, usuario: user } 
    };
  }

  Get(): Observable<UsersResponse> {
    return this.http.get<UsersResponse>('/users', { headers: this.headers });
  }

  GetBooksByUser(user: string): Observable<BooksByUserResponse> {
    return this.http.get<BooksByUserResponse>(`/users/${user}/books`, { headers: this.headers });
  }

  GetUsersPermissions(user: string): Observable<UsersAdminResponse> {
    return this.http.get<UsersAdminResponse>(`/users/${user}/permissions`, { headers: this.headers });
  }

  GetRequestBooks(user: string, apa: string): Observable<ResultResponse> {
    return this.http.get<ResultResponse>(`/users/${user}/books/${apa}/request`, { headers: this.headers });
  }

  GetDeliverBooks(user: string, apa: string): Observable<ResultResponse> {
    return this.http.get<ResultResponse>(`/users/${user}/books/${apa}/deliver`, { headers: this.headers });
  }  
}
