import { NgModule } from '@angular/core';
import { ThemeModule } from '../../@theme/theme.module';
import { TablesRoutingModule, routedComponents } from './order-management-routing.module';
import {TranslateModule} from '@ngx-translate/core';
import {CommonsModule} from '../commons/commons.module';
import { CurrencyMaskModule } from "ng2-currency-mask";
import { PipeModule } from '../../pipes/pipe.module';
import { NguiAutoCompleteModule } from '@ngui/auto-complete';
import { OrderDetailModalComponent } from 'app/pages/order-management/order/order-detail.component';


const notRoutedComponents = [ 
  OrderDetailModalComponent
]

@NgModule({
  imports: [
    ThemeModule,
    TablesRoutingModule,
    TranslateModule,
    CommonsModule,
    CurrencyMaskModule,
    PipeModule,
    NguiAutoCompleteModule
    
  ],
  declarations: [
    ...routedComponents,
    ...notRoutedComponents
  ],
  providers: [
  ],
})
export class OrderManagementModule { }
