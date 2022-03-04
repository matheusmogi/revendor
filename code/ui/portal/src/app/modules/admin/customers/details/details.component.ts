import { Cep, CepService } from './../../../../core/services/cep.service';
import { Address } from './../customers.types';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, ElementRef, OnDestroy, OnInit, Renderer2, TemplateRef, ViewChild, ViewContainerRef, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TemplatePortal } from '@angular/cdk/portal';
import { Overlay, OverlayRef } from '@angular/cdk/overlay';
import { MatDrawerToggleResult } from '@angular/material/sidenav';
import { Subject, Observable } from 'rxjs';
import { debounceTime, takeUntil, map, delay } from 'rxjs/operators';
import { FuseConfirmationService } from '@fuse/services/confirmation';
import { Customer, Country, Tag } from '../customers.types';
import { CustomersListComponent } from '../list/list.component';
import { CustomersService } from '../customers.service';
import { add } from 'lodash';
import { FuseSplashScreenService } from '@fuse/services/splash-screen/splash-screen.service';

@Component({
    selector: 'customers-details',
    templateUrl: './details.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class CustomersDetailsComponent implements OnInit, OnDestroy {
    @ViewChild('avatarFileInput') private _avatarFileInput: ElementRef;
    @ViewChild('tagsPanel') private _tagsPanel: TemplateRef<any>;
    @ViewChild('tagsPanelOrigin') private _tagsPanelOrigin: ElementRef;

    editMode: boolean = false;
    tags: Tag[];
    tagsEditMode: boolean = false;
    filteredTags: Tag[];
    customer: Customer;
    customerForm: FormGroup;
    customers: Customer[];
    countries: Country[];
    private _tagsPanelOverlayRef: OverlayRef;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    /**
     * Constructor
     */
    constructor(
        private _activatedRoute: ActivatedRoute,
        private _changeDetectorRef: ChangeDetectorRef,
        private _customersListComponent: CustomersListComponent,
        private _customersService: CustomersService,
        private _formBuilder: FormBuilder,
        private _fuseConfirmationService: FuseConfirmationService,
        private _router: Router,
        private _cepService: CepService,
        private _splash: FuseSplashScreenService
    ) {
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        // Open the drawer
        this._customersListComponent.matDrawer.open();

        // Create the customer form
        this.customerForm = this._formBuilder.group({
            id: [''],
            name: ['', [Validators.required]],
            email: [''],
            cpf: [''],
            phoneNumbers: this._formBuilder.array([]),
            title: [''],
            birthday: [null],
            address: this._formBuilder.group({
                zipCode: [''],
                addressLine: [''],
                streetNumber: [''],
                complement: [''],
                neighbourhood: [''],
                city: [''],
                state: [''],

            }),
            notes: [null],
            tags: [[]]
        });


        // Get the customers
        this._customersService.customers$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((customers: Customer[]) => {
                this.customers = customers;
                this._changeDetectorRef.markForCheck();
            });

        this._customersService.customer$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((customer: Customer) => {

                this._customersListComponent.matDrawer.open();
                this.customer = customer;
                (this.customerForm.get('phoneNumbers') as FormArray).clear();
                this.customerForm.patchValue(customer);
                const phoneNumbersFormGroups = [];

                if (customer.phoneNumbers.length > 0) {
                    customer.phoneNumbers.forEach((phoneNumber) => {
                        phoneNumbersFormGroups.push(
                            this._formBuilder.group({
                                phoneNumber: [phoneNumber.phoneNumber],
                                label: [phoneNumber.label]
                            })
                        );
                    });
                }
                else {
                    phoneNumbersFormGroups.push(
                        this._formBuilder.group({
                            country: ['us'],
                            phoneNumber: [''],
                            label: ['']
                        })
                    );
                }

                phoneNumbersFormGroups.forEach((phoneNumbersFormGroup) => {
                    (this.customerForm.get('phoneNumbers') as FormArray).push(phoneNumbersFormGroup);
                });

                this.toggleEditMode(false);

                if (this.customer.new) {
                    this.toggleEditMode(true);
                }

                this._changeDetectorRef.markForCheck();
            });


    }


    onChangeCep(): void {
        const cep = this.customerForm.get('address').get('zipCode').value;
        this._cepService.search(cep).subscribe((data: Cep) => {
            this._splash.show();
            this.customerForm.get('address').get('addressLine').setValue(data.street);
            this.customerForm.get('address').get('neighbourhood').setValue(data.neighbourhood);
            this.customerForm.get('address').get('city').setValue(data.city);
            this.customerForm.get('address').get('state').setValue(data.state);
            this.customerForm.get('address').get('complement').setValue(data.complement);
            this._splash.hide();
        });
    }


    getInitials(customerName: string): string {
        const parts = customerName.split(' ');
        if (parts.length > 1) {
            return `${customerName.charAt(0)}${parts[parts.length - 1].charAt(0) ? parts[parts.length - 1].charAt(0) : customerName.charAt(1)}`;
        }
        return customerName.charAt(0);
    }

    buildAddress(address: Address): string {
        return `${address.addressLine}, ${address.streetNumber} - ${address.neighbourhood} - ${address.city}/${address.state} - ${address.zipCode}`;
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();

        if (this._tagsPanelOverlayRef) {
            this._tagsPanelOverlayRef.dispose();
        }
    }
    closeDrawer(): Promise<MatDrawerToggleResult> {
        return this._customersListComponent.matDrawer.close();
    }
    toggleEditMode(editMode: boolean | null = null): void {
        if (editMode === null) {
            this.editMode = !this.editMode;
        }
        else {
            this.editMode = editMode;
        }

        this._changeDetectorRef.markForCheck();
    }
    updateCustomer(): void {
        const customer = this.customerForm.getRawValue();
        customer.phoneNumbers = customer.phoneNumbers.filter(phoneNumber => phoneNumber.phoneNumber);

        if (this._activatedRoute.snapshot.url.find(a => a.path === 'new')) {
            this._customersService.createCustomer(customer).subscribe(() => {
                this.toggleEditMode(false);
                this._router.navigate(['../../'], { relativeTo: this._activatedRoute });
            });
        } else {
            this._customersService.updateCustomer(customer.id, customer).subscribe(() => {
                this.toggleEditMode(false);
            });
        }
    }


    canDelete(): boolean {
        return !this._activatedRoute.snapshot.url.find(a => a.path === 'new');
    }

    cancelCustomer(): void {
        if (this._activatedRoute.snapshot.url.find(a => a.path === 'new')) {
            const id = this.customer.id;
            this._customersService.cancelCustomer(id);
        }
        this._router.navigate(['../../'], { relativeTo: this._activatedRoute });
    }

    deleteCustomer(): void {
        const confirmation = this._fuseConfirmationService.open({
            title: 'Excluir cliente',
            message: 'Você tem certeza que deseja excluir este cliente? Esta ação não pode ser desfeita!',
            actions: {
                confirm: {
                    label: 'Deletar'
                }
            }
        });

        confirmation.afterClosed().subscribe((result) => {

            if (result === 'confirmed') {
                const id = this.customer.id;
                const currentCustomerIndex = this.customers.findIndex(item => item.id === id);
                const nextCustomerIndex = currentCustomerIndex + ((currentCustomerIndex === (this.customers.length - 1)) ? -1 : 1);
                const nextCustomerId = (this.customers.length === 1 && this.customers[0].id === id) ? null : this.customers[nextCustomerIndex].id;

                this._customersService.deleteCustomer(id)
                    .subscribe((isDeleted) => {
                        if (!isDeleted) {
                            return;
                        }

                        if (nextCustomerId) {
                            this._router.navigate(['../', nextCustomerId], { relativeTo: this._activatedRoute });
                        }
                        else {
                            this._router.navigate(['../'], { relativeTo: this._activatedRoute });
                        }

                        this.toggleEditMode(false);
                    });

                this._changeDetectorRef.markForCheck();
            }
        });

    }

    removeEmailField(index: number): void {
        const emailsFormArray = this.customerForm.get('emails') as FormArray;
        emailsFormArray.removeAt(index);
        this._changeDetectorRef.markForCheck();
    }

    addPhoneNumberField(): void {
        const phoneNumberFormGroup = this._formBuilder.group({
            country: ['us'],
            phoneNumber: [''],
            label: ['']
        });

        (this.customerForm.get('phoneNumbers') as FormArray).push(phoneNumberFormGroup);
        this._changeDetectorRef.markForCheck();
    }

    removePhoneNumberField(index: number): void {
        const phoneNumbersFormArray = this.customerForm.get('phoneNumbers') as FormArray;
        phoneNumbersFormArray.removeAt(index);
        this._changeDetectorRef.markForCheck();
    }

    trackByFn(index: number, item: any): any {
        return item.id || index;
    }
}
