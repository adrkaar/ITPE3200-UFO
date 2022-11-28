import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NotloggedinmodalComponent } from './notloggedinmodal.component';

describe('NotloggedinmodalComponent', () => {
    let component: NotloggedinmodalComponent;
    let fixture: ComponentFixture<NotloggedinmodalComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [NotloggedinmodalComponent]
        })
            .compileComponents();

        fixture = TestBed.createComponent(NotloggedinmodalComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});