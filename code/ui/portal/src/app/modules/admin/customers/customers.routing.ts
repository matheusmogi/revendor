import { Route } from '@angular/router';
import { CustomersCustomerResolver, CustomersResolver, NewCustomerResolver } from './customers.resolvers';
import { CustomersListComponent } from './list/list.component';
import { CustomersComponent } from './customers.component';
import { CustomersDetailsComponent } from './details/details.component';
import { CanDeactivateCustomersDetails } from './customers.guards';

export const customersRoutes: Route[] = [
    {
        path: '',
        component: CustomersComponent,
        resolve: {
        },
        children: [
            {
                path: '',
                component: CustomersListComponent,
                resolve: {
                    tasks: CustomersResolver,
                },
                children: [
                    {
                        path: ':id',
                        component: CustomersDetailsComponent,
                        resolve: {
                            task: CustomersCustomerResolver,
                        },
                        canDeactivate: [CanDeactivateCustomersDetails]
                    },
                    {
                        path: 'new/:id',
                        component: CustomersDetailsComponent,
                        resolve: {
                            task: NewCustomerResolver,
                        },
                        canDeactivate: [CanDeactivateCustomersDetails]
                    }
                ]
            }
        ]
    }
];
