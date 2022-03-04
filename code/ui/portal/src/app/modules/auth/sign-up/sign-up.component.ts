import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseAlertType } from '@fuse/components/alert';
import { AuthService } from 'app/core/auth/auth.service';
import { CustomValidator } from 'app/core/auth/custom.validators';

@Component({
    selector: 'auth-sign-up',
    templateUrl: './sign-up.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class AuthSignUpComponent implements OnInit {
    @ViewChild('signUpNgForm') signUpNgForm: NgForm;

    alert: { type: FuseAlertType; message: string } = {
        type: 'success',
        message: ''
    };
    signUpForm: FormGroup;
    showAlert: boolean = false;

    constructor(
        private _authService: AuthService,
        private _formBuilder: FormBuilder,
        private _router: Router
    ) {
    }

    ngOnInit(): void {
        this.signUpForm = this._formBuilder.group({
            name: ['', Validators.required],
            email: ['', [Validators.required, Validators.email]],
            password: ['', Validators.required],
            cpf: ['', [Validators.required, CustomValidator.cpf()]],
            agreements: ['', Validators.requiredTrue]
        }
        );
    }

    signUp(): void {
        if (this.signUpForm.invalid) {
            return;
        }
        this.signUpForm.disable();
        this.showAlert = false;
        this._authService.signUp(this.signUpForm.value)
            .subscribe(
                (response) => {
                    this._router.navigateByUrl('/confirmation-required');
                },
                (response) => {
                    this.signUpForm.enable();
                    this.signUpNgForm.resetForm();
                    this.alert = {
                        type: 'error',
                        message: 'Something went wrong, please try again.'
                    };
                    this.showAlert = true;
                }
            );
    }
}
