import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AuthService } from '../../../core/auth/auth.service';
import { AuthSignUpComponent } from './sign-up.component';

describe('AuthSignUpComponent', () => {
    let component: AuthSignUpComponent;
    let fixture: ComponentFixture<AuthSignUpComponent>;
    let fakeService: AuthService;
    beforeEach(() => {
        fakeService = jasmine.createSpyObj<AuthService>(
            'AuthService',
            {
            }
        );
        TestBed.configureTestingModule({
            declarations: [AuthSignUpComponent],
            providers: [
                { provide: AuthService, useValue: fakeService }
            ]
        }).compileComponents();
        fixture = TestBed.createComponent(AuthSignUpComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('can load instance', () => {
        expect(component).toBeTruthy();
    });

    // it(`showAlert has default value`, () => {
    //     expect(component.showAlert).toEqual(false);
    // });

    // describe('ngOnInit', () => {
    //     it('makes expected calls', () => {
    //         const formBuilderStub: FormBuilder = fixture.debugElement.injector.get(
    //             FormBuilder
    //         );
    //         spyOn(formBuilderStub, 'group').and.callThrough();
    //         component.ngOnInit();
    //         expect(formBuilderStub.group).toHaveBeenCalled();
    //     });
    // });

    // describe('signUp', () => {
    //     it('makes expected calls', () => {
    //         const routerStub: Router = fixture.debugElement.injector.get(Router);
    //         const authServiceStub: AuthService = fixture.debugElement.injector.get(
    //             AuthService
    //         );
    //         spyOn(routerStub, 'navigateByUrl').and.callThrough();
    //         spyOn(authServiceStub, 'signUp').and.callThrough();
    //         component.signUp();
    //         expect(routerStub.navigateByUrl).toHaveBeenCalled();
    //         expect(authServiceStub.signUp).toHaveBeenCalled();
    //     });
    // });
});
