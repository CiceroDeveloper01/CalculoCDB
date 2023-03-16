import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CalculoCDBComponent } from './calculo-cdb.component';

describe('CalculoCDBComponent', () => {
  let component: CalculoCDBComponent;
  let fixture: ComponentFixture<CalculoCDBComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CalculoCDBComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CalculoCDBComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
