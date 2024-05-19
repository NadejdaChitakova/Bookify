import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Options } from '../types/options';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AppService {

  constructor(
    private httpClient: HttpClient) { }

    get<T>(url: string, options: Options): Observable<T>{
      return this.httpClient.get<T>(url, options) as Observable<T>;
    }
}
