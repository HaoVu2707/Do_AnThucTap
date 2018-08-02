import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { DeleteDialogComponent } from '../commons/delete-dialog/delete-dialog.component';
import { CustomerManagementComponent } from './customer-management.component';
import { CustomerComponent } from './customer/customer.component';
import { CustomerUpdateModalComponent } from './customer/customer-update.component';
import { ShowedColumnsButtonComponent } from 'app/pages/commons/showed-columns-button/showed-columns-button.component';
import { CustomerActivelModalComponent } from './customer/customer-active.component';
import { CustomerPauselModalComponent } from './customer/customer-pause.component';

const routes: Routes = [{
  path: '',
  component: CustomerManagementComponent,
  children: [{
    path: 'customers',
    component: CustomerComponent,
  }]
}];

@NgModule({
  imports: [RouterModule.forChild(routes),
  ],
  exports: [RouterModule],
  entryComponents: [
    CustomerUpdateModalComponent,
    DeleteDialogComponent,
    ShowedColumnsButtonComponent,
    CustomerActivelModalComponent,
    CustomerPauselModalComponent
  ]
})
export class TablesRoutingModule { }

export const routedComponents = [
  CustomerManagementComponent,
  CustomerComponent
];
