import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResultResponse } from '../../models/Responses/result-response';
import { BooksResponse } from '../../models/Responses/books-response';
import { BooksResponseSingle } from '../../models/Responses/books-response-single';
import { BooksYearsResponse } from '../../models/Responses/books-years-response';

@Injectable({
  providedIn: 'root'
})
export class BooksServicesService {

  constructor(private http: HttpClient) { }
  private headers = new HttpHeaders({ 'Content-type': 'application/json', 'Accept': 'application/json' });

  Get(apa: string, year: number): Observable<BooksResponse> {
    if (apa != '') return this.GetByApa(apa); 
    else if (year > 0) return this.GetByYear(year); 
    else
      return this.http.get<BooksResponse>('/books', { headers: this.headers });
  }

  GetByApa(apa: string): Observable<BooksResponse> {
    return this.http.get<BooksResponse>(`/books/${apa}`, { headers: this.headers });
  }

  GetByYear(year: number): Observable<BooksResponse> {
    return this.http.get<BooksResponse>(`/books/${year}/years`, { headers: this.headers });
  }

  GetYears(): Observable<BooksYearsResponse> {
    return this.http.get<BooksYearsResponse>('/books/years', { headers: this.headers });
  }

  CreateBook(request: string): Observable<ResultResponse> {
    return this.http.post<ResultResponse>('/books', request, { headers: this.headers });
  }

  DeleteBook(apa: string): Observable<ResultResponse> {
    return this.http.delete<ResultResponse>(`/books/${apa}`, { headers: this.headers });
  }
}
