import { ApiUtils } from 'app/core/api/api.utils';
import { ApiService } from '../../../core/api/api.service';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of, Subject, throwError } from 'rxjs';
import { filter, map, switchMap, take, tap } from 'rxjs/operators';
import { Customer, Tag } from './customers.types';
import ENDPOINTS from '../../../core/api/endpoints';

@Injectable({
    providedIn: 'root'
})
export class CustomersService {
    // Private
    public onError: Subject<string>;
    private _customer: BehaviorSubject<Customer | null> = new BehaviorSubject(null);
    private _customers: BehaviorSubject<Customer[] | null> = new BehaviorSubject(null);
    private _tags: BehaviorSubject<Tag[] | null> = new BehaviorSubject(null);


    constructor(private _httpClient: HttpClient, private _apiService: ApiService) {
        this.onError = _apiService.onError;
    }


    get customer$(): Observable<Customer> {
        return this._customer.asObservable();
    }


    get customers$(): Observable<Customer[]> {
        return this._customers.asObservable();
    }

    get tags$(): Observable<Tag[]> {
        return this._tags.asObservable();
    }

    getCustomers(): Observable<Customer[]> {
        return this._apiService.get<Customer[]>(ENDPOINTS.getCustomers).pipe(
            tap((customers) => {
                this._customers.next(customers);
            })
        );
    }


    searchCustomers(query: string): Observable<Customer[]> {
        return this._httpClient.get<Customer[]>('api/apps/customers/search', {
            params: { query }
        }).pipe(
            tap((customers) => {
                this._customers.next(customers);
            })
        );
    }

    getCustomerById(id: string): Observable<Customer> {
        return this._customers.pipe(
            take(1),
            map((customers) => {

                // Find the customer
                const customer = customers.find(item => item.id === id) || null;
                // Update the customer
                this._customer.next(customer);
                // Return the customer
                return customer;
            }),
            switchMap((customer) => {

                if (!customer) {
                    return throwError('Could not found customer with id of ' + id + '!');
                }

                return of(customer);
            })
        );
    }

    addCustomer(): Observable<Customer> {
        const customer = {
            id: ApiUtils.guid(),
            name: '',
            email: '',
            cpf: '',
            phoneNumbers: [],
            birthday: null,
            address: null,
            notes: null,
            tags: [],
            new:true
        };
        return this.customers$.pipe(
            take(1),
            switchMap(customers => of(customer).pipe(
                map((newCustomer) => {
                    this._customers.next([newCustomer, ...customers]);
                    return newCustomer;
                })
            ))
        );
    }

    createCustomer(customer: Customer): Observable<Customer> {
        return this.customers$.pipe(
            take(1),
            switchMap(customers => this._apiService.post<Customer>(ENDPOINTS.newCustomer, null, customer).pipe(
                map((newCustomer) => {
                    const index = customers.findIndex(item => item.id === customer.id);
                    customers[index] = newCustomer;
                    this._customer.next(newCustomer);
                    this._customers.next(customers);
                    return newCustomer;
                })
            ))
        );
    }

    updateCustomer(id: string, customer: Customer): Observable<Customer> {
        return this.customers$.pipe(
            take(1),
            switchMap(customers => this._apiService.patch<Customer>(ENDPOINTS.updateCustomer, null, customer).pipe(
                map((updatedCustomer) => {

                    // Find the index of the updated customer
                    const index = customers.findIndex(item => item.id === id);

                    // Update the customer
                    customers[index] = updatedCustomer;

                    // Update the customers
                    this._customers.next(customers);

                    // Return the updated customer
                    return updatedCustomer;
                }),
                switchMap(updatedCustomer => this.customer$.pipe(
                    take(1),
                    filter(item => item && item.id === id),
                    tap(() => {

                        // Update the customer if it's selected
                        this._customer.next(updatedCustomer);

                        // Return the updated customer
                        return updatedCustomer;
                    })
                ))
            ))
        );
    }

    deleteCustomer(id: string): Observable<boolean> {
        const customer = { id: id } as Customer;
        return this.customers$.pipe(
            take(1),
            switchMap(customers => this._apiService.post(ENDPOINTS.deleteCustomer, null, customer).pipe(
                map((isDeleted: boolean) => {

                    const index = customers.findIndex(item => item.id === id);
                    customers.splice(index, 1);
                    this._customers.next(customers);
                    return isDeleted;
                })
            ))
        );
    };


    cancelCustomer(id: string): void {
        this.customers$.subscribe((customers) => {
            const index = customers.findIndex(item => item.id === id);
            if (index > -1) {
                customers.splice(index, 1);
            }
        });
    };


    uploadAvatar(id: string, avatar: File): Observable<Customer> {
        return this.customers$.pipe(
            take(1),
            switchMap(customers => this._httpClient.post<Customer>('api/apps/customers/avatar', {
                id,
                avatar
            }, {
                headers: {
                    // eslint-disable-next-line @typescript-eslint/naming-convention
                    'Content-Type': avatar.type
                }
            }).pipe(
                map((updatedCustomer) => {

                    // Find the index of the updated customer
                    const index = customers.findIndex(item => item.id === id);

                    // Update the customer
                    customers[index] = updatedCustomer;

                    // Update the customers
                    this._customers.next(customers);

                    // Return the updated customer
                    return updatedCustomer;
                }),
                switchMap(updatedCustomer => this.customer$.pipe(
                    take(1),
                    filter(item => item && item.id === id),
                    tap(() => {

                        // Update the customer if it's selected
                        this._customer.next(updatedCustomer);

                        // Return the updated customer
                        return updatedCustomer;
                    })
                ))
            ))
        );
    }
}
