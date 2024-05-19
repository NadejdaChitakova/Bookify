import { Injectable } from '@angular/core';
import { AppService } from './app.service';
import { Observable } from 'rxjs';
import { Apartments } from '../types/apartments';
import { PaginationParams } from '../types/pagination-params';

@Injectable({
  providedIn: 'root'
})
export class ApartmentService {

  constructor(private appService: AppService) { }

  getApartments = (url: string, params: PaginationParams) : Observable<Apartments> => {
    return this.appService.get(url, {
      params,
      responseType: 'json'
    })
  }
}
