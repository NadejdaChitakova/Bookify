import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListApartmentsComponent } from './list-apartments.component';

describe('ListApartmentsComponent', () => {
  let component: ListApartmentsComponent;
  let fixture: ComponentFixture<ListApartmentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListApartmentsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ListApartmentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
