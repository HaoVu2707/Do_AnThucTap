import { NgModule } from '@angular/core';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { ThemeModule } from '../../@theme/theme.module';
import {TranslateModule} from '@ngx-translate/core';
import {CommonsModule} from '../commons/commons.module';
import { CurrencyMaskModule } from "ng2-currency-mask";
import { PipeModule } from '../../pipes/pipe.module';
import { ServiceUpdateModalComponent } from 'app/pages/service-management/service/service-update.component';
import { ServiceCategoryUpdateModalComponent } from 'app/pages/service-management/serviceCategory/serviceCategory-update.component';
import {TablesRoutingModule,routedComponents} from './service-management-routing.module';

const notRoutedComponents = [
  ServiceUpdateModalComponent,
  ServiceCategoryUpdateModalComponent

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
export class ServiceManagementModule { }
