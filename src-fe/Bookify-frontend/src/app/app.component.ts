import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ApartmentComponent } from '../apartment/apartment.component';
import { HeaderComponent } from '../layot/header/header.component';
import { FooterComponent } from '../layot/footer/footer.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, FooterComponent, ApartmentComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Bookify';
}
