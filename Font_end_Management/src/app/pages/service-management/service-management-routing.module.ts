import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { DeleteDialogComponent } from '../commons/delete-dialog/delete-dialog.component';
import { ServiceManagementComponent } from './service-management.component';
import { ServiceComponent } from './service/service.component';
import { ServiceUpdateModalComponent } from './service/service-update.component';
import { ShowedColumnsButtonComponent } from 'app/pages/commons/showed-columns-button/showed-columns-button.component';

import { ServiceCategoryComponent } from './serviceCategory/serviceCategory.component';
import { ServiceCategoryUpdateModalComponent } from 'app/pages/service-management/serviceCategory/serviceCategory-update.component';


const routes: Routes = [{
  path: '',
  component: ServiceManagementComponent,
  children: [{
    path: 'services',
    component: ServiceComponent,
  },
  {
    path: 'serviceCategories',
    component: ServiceCategoryComponent,
  }

]
}];

@NgModule({
  imports: [RouterModule.forChild(routes),
  ],
  exports: [RouterModule],
  entryComponents: [
    ServiceUpdateModalComponent,
    ServiceCategoryUpdateModalComponent,
    DeleteDialogComponent,
    ShowedColumnsButtonComponent
  ]
})
export class TablesRoutingModule { }

export const routedComponents = [
  ServiceManagementComponent,
  ServiceComponent,
  ServiceCategoryComponent

];
