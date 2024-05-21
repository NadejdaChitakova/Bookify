import { Apartments } from './../../types/apartments';
import { ApartmentService } from './../../services/apartment.service';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ApartmentComponent } from '../apartment/apartment.component';

@Component({
  selector: 'app-list-apartments',
  standalone: true,
  imports: [HttpClientModule, ApartmentComponent],
  templateUrl: './list-apartments.component.html',
  styleUrl: './list-apartments.component.css',
})

export class ListApartmentsComponent implements OnInit{
  apartments!: Apartments;
  error: any;
  constructor(private apartmentService: ApartmentService){}

  ngOnInit() {
    this.apartmentService.getApartments()
    .subscribe({
      next: (data) => {
        this.apartments = data;
        console.log(this.apartments);
      },
      error: (error) => {
        console.log(error, "test");
      }
    });
  }
}
