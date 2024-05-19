import { MenubarModule } from 'primeng/menubar';
import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'header',
  standalone: true,
  imports: [
    MenubarModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})

export class HeaderComponent implements OnInit {
  items: MenuItem[] | undefined;

  ngOnInit(): void {
    this.items = [
      {
        label: 'Home',
        icon: 'pi pi-fw pi-plus',
      },
      {
        label: 'Apartment',
        icon: 'pi pi-fw pi-plus',
      },
    ]
  }

}

