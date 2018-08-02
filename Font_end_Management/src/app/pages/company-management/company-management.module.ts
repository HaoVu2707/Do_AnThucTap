import { NgModule } from '@angular/core';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { ThemeModule } from '../../@theme/theme.module';
import {TranslateModule} from '@ngx-translate/core';
import {CommonsModule} from '../commons/commons.module';
import { CurrencyMaskModule } from "ng2-currency-mask";
import { PipeModule } from '../../pipes/pipe.module';
import { BranchUpdateModalComponent } from 'app/pages/company-management/branch/branch-update.component';
import { CompanyUpdateModalComponent } from 'app/pages/company-management/company/company-update.component';
import {TablesRoutingModule,routedComponents , notRoutedComponents} from './company-management-routing.module';



@NgModule({
  imports: [
    ThemeModule,
    TablesRoutingModule,
    Ng2SmartTableModule,
    TranslateModule,
    CommonsModule,
    CurrencyMaskModule,
    PipeModule

  ],
  declarations: [
    ...routedComponents,
    ...notRoutedComponents
  ],
  providers: [
  ],
})
export class CompanyManagementModule { }
