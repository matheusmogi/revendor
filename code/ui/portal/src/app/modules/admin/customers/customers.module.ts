import { MatSelectModule } from '@angular/material/select';
import { NgModule } from '@angular/core';
import { Route, RouterModule } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { SharedModule } from 'app/shared/shared.module';
import { MatButtonModule } from '@angular/material/button';
import { customersRoutes } from './customers.routing';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import moment from 'moment';
import { FuseFindByKeyPipeModule } from '@fuse/pipes/find-by-key';
import { CustomersComponent } from './customers.component';
import { CustomersListComponent } from './list/list.component';
import { CustomersDetailsComponent } from './details/details.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import {MatSnackBarModule, MAT_SNACK_BAR_DEFAULT_OPTIONS} from '@angular/material/snack-bar';
import { NgxMaskModule, IConfig } from 'ngx-mask';

export const options: Partial<IConfig> | (() => Partial<IConfig>) = null;

@NgModule({
    declarations: [
        CustomersComponent,
        CustomersListComponent,
        CustomersDetailsComponent
    ],
    imports: [
        RouterModule.forChild(customersRoutes),
        MatFormFieldModule,
        MatIconModule,
        MatSidenavModule,
        MatInputModule,
        SharedModule,
        MatMomentDateModule,
        MatButtonModule,
        MatDatepickerModule,
        FuseFindByKeyPipeModule,
        MatSelectModule,
        MatTooltipModule,
        MatSnackBarModule,
        NgxMaskModule.forRoot(),
    ],
    providers: [
            {provide: MAT_SNACK_BAR_DEFAULT_OPTIONS, useValue: {duration: 2500}},
            { provide: MAT_DATE_LOCALE, useValue: 'pt-BR' },
            {provide: MAT_DATE_FORMATS,
            useValue: {
                parse: {
                    dateInput: 'DD/MM/YYYY'
                },
                display: {
                    dateInput: 'DD/MM/YYYY',
                    monthYearLabel: 'YYYY',
                    dateA11yLabel: 'LL',
                    monthYearA11yLabel: 'YYYY'
                }
            }
            }
    ]
})
export class CustomersModule { }
