import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { DeleteDialogComponent } from '../commons/delete-dialog/delete-dialog.component';
import { CompanyManagementComponent } from './company-management.component';
import { BranchComponent } from './branch/branch.component';
import { BranchUpdateModalComponent } from './branch/branch-update.component';
import { ShowedColumnsButtonComponent } from 'app/pages/commons/showed-columns-button/showed-columns-button.component';

import { CompanyComponent } from './company/company.component'
import { CompanyUpdateModalComponent } from 'app/pages/company-management/company/company-update.component';

const routes: Routes = [{
  path: '',
  component: CompanyManagementComponent,
  children: [{
    path: 'companies',
    component: CompanyComponent,
  },

  {
    path: 'branchs',
    component: BranchComponent,
  }
]
}];

@NgModule({
  imports: [RouterModule.forChild(routes),
  ],
  exports: [RouterModule],
  entryComponents: [
    BranchUpdateModalComponent,
    CompanyUpdateModalComponent,
    DeleteDialogComponent,
    ShowedColumnsButtonComponent
  ]
})
export class TablesRoutingModule { }

export const routedComponents = [
  CompanyManagementComponent,
  BranchComponent,
  CompanyComponent
];

export const notRoutedComponents = [
  BranchUpdateModalComponent,
  CompanyUpdateModalComponent
 ]
