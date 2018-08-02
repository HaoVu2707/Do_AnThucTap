import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { PagesComponent } from './pages.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { PermissionDenyComponent } from './permission-deny/permission-deny.component';

import { CustomerComponent } from 'app/pages/customer-management/customer/customer.component';
import { ServiceComponent } from 'app/pages/service-management/service/service.component';
import { ServiceCategoryComponent } from 'app/pages/service-management/serviceCategory/serviceCategory.component';
import {CompanyComponent} from 'app/pages/Company-management/company/company.component'
import {BranchComponent} from 'app/pages/Company-management/branch/branch.component'

const routes: Routes = [{
  path: '',
  component: PagesComponent,
  children: [{
    path: 'dashboard',
    component: DashboardComponent,
  },
  {
    path: 'permission-deny',
    component: PermissionDenyComponent,
  },

  {
    path: 'service-management',
    loadChildren: './service-management/service-management.module#ServiceManagementModule'
  },
  {
    path: 'sales',
    loadChildren: './sales-management/sales-management.module#SalesManagementModule',
  },
  {
    path: 'customer-management',
    loadChildren: './customer-management/customer-management.module#CustomerManagementModule'
  },
  {
    path: 'company-management',
    loadChildren: './company-management/company-management.module#CompanyManagementModule'
  },
  {
    path: 'order-management',
    loadChildren: './order-management/order-management.module#OrderManagementModule'
  },
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full',
  }],
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PagesRoutingModule {
}
