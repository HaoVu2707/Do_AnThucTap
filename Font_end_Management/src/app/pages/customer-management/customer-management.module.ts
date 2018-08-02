import { NgModule } from '@angular/core';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { ThemeModule } from '../../@theme/theme.module';
import {TranslateModule} from '@ngx-translate/core';
import {CommonsModule} from '../commons/commons.module';
import { CurrencyMaskModule } from "ng2-currency-mask";
import { PipeModule } from '../../pipes/pipe.module';
import { CustomerUpdateModalComponent } from 'app/pages/customer-management/customer/customer-update.component';
import { TablesRoutingModule,routedComponents } from './customer-management-routing.module';
import { CustomerActivelModalComponent } from './customer/customer-active.component';
import { CustomerPauselModalComponent } from './customer/customer-pause.component';

const notRoutedComponents = [ 
  CustomerUpdateModalComponent,
  CustomerActivelModalComponent,
  CustomerPauselModalComponent
]

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
export class CustomerManagementModule { }
