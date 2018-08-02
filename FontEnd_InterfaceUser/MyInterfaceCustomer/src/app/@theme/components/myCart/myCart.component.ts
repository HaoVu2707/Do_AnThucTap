import { Component, Input, Output, OnInit } from '@angular/core';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { HelperService } from '../../../@core/utils/helper.service';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { NgbDatepickerConfig, NgbDateStruct, NgbDateParserFormatter, NgbDatepickerI18n } from '@ng-bootstrap/ng-bootstrap';
import { NgbDateFRParserFormatter } from '../../../pages/commons/ng-bootstrap-datepicker-util/ngb-date-fr-parser-formatter';
import { CustomDatepickerI18n, I18n } from '../../../pages/commons/ng-bootstrap-datepicker-util/ngbd-datepicker-i18n';
import { OrderComponent } from 'app/@theme/components/order/order.component';
import { UserService } from '../../../@core/data/user.service';
import { LoginComponent } from 'app/@theme/components/login/login.component';
import { OrderService } from '../../../@core/data/order.service';
import { DeleteDialogComponent } from '../../../pages/commons/delete-dialog/delete-dialog.component';



@Component({
  selector: 'myCart-modal-component',
  styleUrls: ['./myCart.component.scss'],
  templateUrl: './myCart.component.html',
  providers: [{ provide: NgbDateParserFormatter, useClass: NgbDateFRParserFormatter },
    I18n, { provide: NgbDatepickerI18n, useClass: CustomDatepickerI18n }
  ]
})

export class MyCartComponent implements OnInit {
  @Input() editedModel: any;
  @Input() reload: any;

  private today: any = this.helperService.getTodayForDatePicker();
  amount: 0;
  Status = "BOOKED";
  TotalMoney: any;
  model: any = {
    id :''
  };
  isEditMode = false;
  isKeepOpen: boolean = false;
  listAllServices_cart = [];

  constructor(public activeModal: NgbActiveModal,
    public helperService: HelperService,
    private toastrService: ToastrService,
    private translateService: TranslateService,
    private userService: UserService,
    private orderService: OrderService,
    private i18n: I18n,
    private modalService: NgbModal,
    config: NgbDatepickerConfig,
  ) {
    // config maxDate and languge for date picker

  }
  allOrders: any = [];


  ngOnInit() {
      this.getAllOrder();
  }

  async getAllOrder() {
      let response = await this.orderService.getAll_CodeCustomers();
      this.allOrders = response.data;
      console.log(this.allOrders);

  }

  onClickBtnDestroy(order): void {
    this.model.id = order.id;
    console.log(order);
    console.log(this.model.id);
    const modalRef = this.modalService.open(DeleteDialogComponent, { backdrop: 'static' });
    modalRef.componentInstance.reload = () => {
      this.getAllOrder();
    };
    this.translateService.get("delete_postOrder").subscribe((res: string) => {
      modalRef.componentInstance.title = res +" "+ order.code;
    });
    modalRef.componentInstance.deleteFunction = () => {
      return this.orderService.editForOrderCustomer(this.model.id,this.model);
    }
  }

}
