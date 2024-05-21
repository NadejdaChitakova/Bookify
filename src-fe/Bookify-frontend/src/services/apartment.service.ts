import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Apartment } from '../types/apartment';
import { Apartments } from '../types/apartments';

@Injectable({
  providedIn: 'root'
})
export class ApartmentService {

  constructor(private http: HttpClient) { }

  getApartments() :Observable<Apartments> {
    const url = "http://localhost:5000/api/Apartments/GetApartments";

     return this.http.get<Apartments>(url);
  }
}
