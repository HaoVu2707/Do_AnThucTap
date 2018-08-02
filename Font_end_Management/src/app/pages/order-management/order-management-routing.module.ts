import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { OrderManagmentComponent } from './order-management.component';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import {DeleteDialogComponent} from '../commons/delete-dialog/delete-dialog.component';
import {OrderComponent} from './order/order.component'; 
import { OrderDetailModalComponent } from 'app/pages/order-management/order/order-detail.component';

const routes: Routes = [{
  path: '',
  component: OrderManagmentComponent,
  children: [
 
    {
      path: 'orders',
      component: OrderComponent,
    },
],
}];

@NgModule({
  imports: [RouterModule.forChild(routes),
  ],
  exports: [RouterModule],
  entryComponents: [
     DeleteDialogComponent , 
     OrderDetailModalComponent
  ]
})
export class TablesRoutingModule { }

export const routedComponents = [
  OrderManagmentComponent,
  OrderComponent,

];
