import { DOCUMENT } from '@angular/common';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Inject, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDrawer } from '@angular/material/sidenav';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { FuseMediaWatcherService } from '@fuse/services/media-watcher';
import { FuseSplashScreenService } from '@fuse/services/splash-screen';
import { fromEvent, Observable, Subject } from 'rxjs';
import { filter, map, switchMap, takeUntil } from 'rxjs/operators';
import { CustomersService } from '../customers.service';
import { Customer, Country } from '../customers.types';

@Component({
    selector: 'app-customers-list',
    templateUrl: './list.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class CustomersListComponent implements OnInit, OnDestroy {

    @ViewChild('matDrawer', { static: true }) matDrawer: MatDrawer;
    customers$: Observable<Customer[]>;

    customersCount: number = 0;
    customersTableColumns: string[] = ['name', 'email', 'phoneNumber'];
    countries: Country[];
    drawerMode: 'side' | 'over';
    searchInputControl: FormControl = new FormControl();
    selectedCustomer: Customer;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(private _activatedRoute: ActivatedRoute,
        private _changeDetectorRef: ChangeDetectorRef,
        private _customersService: CustomersService,
        @Inject(DOCUMENT) private _document: any,
        private _router: Router,
        private _fuseMediaWatcherService: FuseMediaWatcherService,
        private _snackBar: MatSnackBar,
        private _splash: FuseSplashScreenService) { }
    ngOnDestroy(): void {
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    ngOnInit(): void {
        this._splash.show();
        this._customersService.onError.subscribe((error) => {
            this._snackBar.open('Aconteceu um erro inesperado. Já estamos trabalhando para resolver.', 'Fechar', { duration: 5000 });
        });

        // Get the customers
        this.customers$ = this._customersService.customers$.pipe(map((customers) => {
            const newCustomers = customers.filter(x => !x.new);
            return newCustomers;
        }));
        this._customersService.customers$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((customers: Customer[]) => {

                // Update the counts
                this.customersCount = customers.length;

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });

        // Get the Customer
        this._customersService.customer$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((customer: Customer) => {

                // Update the selected Customer
                this.selectedCustomer = customer;

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });

        // Subscribe to search input field value changes
        this.searchInputControl.valueChanges
            .pipe(
                takeUntil(this._unsubscribeAll),
                switchMap(query =>
                    // Search
                    this._customersService.searchCustomers(query)
                )
            )
            .subscribe();

        // Subscribe to MatDrawer opened change
        this.matDrawer.openedChange.subscribe((opened) => {
            if (!opened) {
                // Remove the selected Customer when drawer closed
                this.selectedCustomer = null;

                // Mark for check
                this._changeDetectorRef.markForCheck();
            }
        });

        // Subscribe to media changes
        this._fuseMediaWatcherService.onMediaChange$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(({ matchingAliases }) => {

                // Set the drawerMode if the given breakpoint is active
                if (matchingAliases.includes('lg')) {
                    this.drawerMode = 'side';
                }
                else {
                    this.drawerMode = 'over';
                }

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });

        // Listen for shortcuts
        fromEvent(this._document, 'keydown')
            .pipe(
                takeUntil(this._unsubscribeAll),
                filter<KeyboardEvent>(event =>
                    (event.ctrlKey === true || event.metaKey) // Ctrl or Cmd
                    && (event.key === '/') // '/'
                )
            )
            .subscribe(() => {
                this.createCustomer();
            });

        this._splash.hide();

    }

    getInitials(customerName: string): string {
        if (customerName.split(' ').length > 1) {
            return `${customerName.charAt(0)}${customerName.split(' ')[1].charAt(0) ? customerName.split(' ')[1].charAt(0) : customerName.charAt(1)}`;
        }
        return customerName.charAt(0);
    }
    createCustomer(): void {
        this._customersService.addCustomer().subscribe((newCustomer) => {

            // Go to the new Customer
            this._router.navigate(['./new', newCustomer.id], { relativeTo: this._activatedRoute });

            // Mark for check
            this._changeDetectorRef.markForCheck();
        });
    }

}
