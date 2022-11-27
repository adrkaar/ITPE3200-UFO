import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WelcomemodalComponent } from './welcomemodal.component';

describe('WelcomemodalComponent', () => {
  let component: WelcomemodalComponent;
  let fixture: ComponentFixture<WelcomemodalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WelcomemodalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WelcomemodalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
