import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { Component, Input, OnInit } from '@angular/core';
import { Apartment } from '../types/apartment';

@Component({
  selector: 'apartment',
  standalone: true,
  imports: [CardModule, ButtonModule],
  templateUrl: './apartment.component.html',
  styleUrl: './apartment.component.css'
})
export class ApartmentComponent implements OnInit {
  @Input() apartment!: Apartment;

  ngOnInit(): void {

  }
}
