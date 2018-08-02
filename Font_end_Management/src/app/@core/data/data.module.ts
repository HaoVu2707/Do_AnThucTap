import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from './auth.service';
import { UserService } from 'app/@core/data/user.service';
import { RoleService } from 'app/@core/data/role.service';
import { AccessiblePageService } from 'app/@core/data/accessible-page.service';
import { StateService } from 'app/@core/data/state.service';
import { CustomerService } from './customer.service';
import { ServiceCategoryService } from './serviceCategory.service';
import { ServiceService } from './service.service';
import { OrderService } from './order.service';
import { SellingServiceService } from './sellingService.service';
import { BranchService } from './branch.service';
import { CompanyService } from './company.service';
import { EmployeeService } from './employee.service';

const SERVICES = [
  AuthService,
  UserService,
  RoleService,
  AccessiblePageService,
  StateService,
  ServiceService,
  ServiceCategoryService,
  CustomerService,
  OrderService,
  SellingServiceService,
  BranchService,
  CompanyService,
  EmployeeService
];

@NgModule({
  imports: [
    CommonModule,
  ],
  providers: [
    ...SERVICES,
  ],
})
export class DataModule {
  static forRoot(): ModuleWithProviders {
    return <ModuleWithProviders>{
      ngModule: DataModule,
      providers: [
        ...SERVICES,
      ],
    };
  }
}
