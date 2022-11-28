import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WarningmodalComponent } from './warningmodal.component';

describe('WarningmodalComponent', () => {
  let component: WarningmodalComponent;
  let fixture: ComponentFixture<WarningmodalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WarningmodalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WarningmodalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});