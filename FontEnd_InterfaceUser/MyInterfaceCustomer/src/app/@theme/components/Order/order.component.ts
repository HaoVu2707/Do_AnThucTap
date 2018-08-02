import { Component, Input, Output, OnInit } from '@angular/core';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { HelperService } from '../../../@core/utils/helper.service';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { NgbDatepickerConfig, NgbDateStruct, NgbDateParserFormatter, NgbDatepickerI18n } from '@ng-bootstrap/ng-bootstrap';
import { NgbDateFRParserFormatter } from '../../../pages/commons/ng-bootstrap-datepicker-util/ngb-date-fr-parser-formatter';
import { CustomDatepickerI18n, I18n } from '../../../pages/commons/ng-bootstrap-datepicker-util/ngbd-datepicker-i18n';
import { UserService } from '../../../@core/data/user.service';
import { OrderService } from '../../../@core/data/order.service';
import {ServiceForOrder}  from '../../../models/serviceForOrder';


@Component({
    selector: 'order-modal-component',
    styleUrls: ['./order.component.scss'],
    templateUrl: './order.component.html',
    providers: [{ provide: NgbDateParserFormatter, useClass: NgbDateFRParserFormatter },
        I18n, { provide: NgbDatepickerI18n, useClass: CustomDatepickerI18n }
    ]
})

export class OrderComponent implements OnInit {
    @Input() editedModel: any;
    @Input() reload: any;

    private today: any = this.helperService.getTodayForDatePicker();
    amount : 0;
    TotalMoney: any;
    model: any = {
      serviceList : []
    };
    isEditMode = false;
    isKeepOpen: boolean = false;
    listAllServices_cart = [];
    user: any = {
      name: '',
      address: '',
      phoneNumber:''
    };

    constructor(public activeModal: NgbActiveModal,
        public helperService: HelperService,
        private toastrService: ToastrService,
        private translateService: TranslateService,
        private i18n: I18n,
        private userService: UserService,
        private orderService: OrderService,
        private modalService: NgbModal,
        config: NgbDatepickerConfig,
    ) {
        // config maxDate and languge for date picker
        config.maxDate = this.today;
        this.i18n.language = this.translateService.currentLang;
    }

    async ngOnInit() {
      var data = await this.userService.getCurrentUser();
      console.log(data);
      this.user.name = data.firstName + " " + data.lastName;
      this.user.phoneNumber = data.phoneNumber;
      console.log(data.phoneNumber);
      this.user.address = data.address;

      this.helperService.setLocalStorage("ServiceListForOrder",null);

    }


    async onClickConFirmBtn() {
      let inputStorageIdList = [];
      let products = this.helperService.getLocalStorage("ProductList")||[];
      let servicesForOrder = this.helperService.getLocalStorage("ServiceListForOrder")||[];
      console.log (products);
      for(let i = 0; i < products.length; i ++ ){
        let newServiceCart :ServiceForOrder ;
        newServiceCart = new ServiceForOrder();
        newServiceCart.setBeginProductStorage(products[i].serviceId, products[i].code ,products[i].imageUrl,
          products[i].branchId,products[i].name,products[i].description,products[i].price,products[i].discount,products[i].amount);

          servicesForOrder.push(newServiceCart);
      }
      localStorage.setItem("ServiceListForOrder", JSON.stringify(servicesForOrder));

      this.model.serviceList = this.helperService.getLocalStorage("ServiceListForOrder")||[];

      try {
        console.log(this.model);
        let response = await this.orderService.add(this.model);
                this.helperService.showAddSuccessForOrderCustomerToast();
                if (this.isKeepOpen) {
                   // this.getAllCustomers();
                    this.model = null;
                }
                if (!this.isKeepOpen) {
                          this.activeModal.close();
                          this.reload();
                      }
      }
      catch(error){
          this.helperService.showErrorToast(error);
      }

    }
}
