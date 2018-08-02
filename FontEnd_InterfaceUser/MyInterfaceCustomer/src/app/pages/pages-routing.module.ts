import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { PagesComponent } from './pages.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { PermissionDenyComponent } from './permission-deny/permission-deny.component';

import { CustomerComponent } from 'app/pages/customer-management/customer/customer.component';
import { ServiceComponent } from 'app/pages/service-management/service/service.component';
import { ServiceCategoryComponent } from 'app/pages/service-management/serviceCategory/serviceCategory.component';

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
