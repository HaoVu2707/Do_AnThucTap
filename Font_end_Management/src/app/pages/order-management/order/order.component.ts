import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { LocalDataSource, ViewCell } from 'ng2-smart-table';
import { CONSTANT } from '../../../constant';
import { HelperService } from '../../../@core/utils/helper.service';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { DeleteDialogComponent } from '../../commons/delete-dialog/delete-dialog.component';
import { TranslateService } from '@ngx-translate/core';
import { OrderService } from '../../../@core/data/order.service';
import { OrderDetailModalComponent } from 'app/pages/order-management/order/order-detail.component';


@Component({
  selector: 'my-order',
  templateUrl: './order.component.html',
  styles: [`
  `],
})
export class OrderComponent implements OnInit {


  dataList = [];
  page: number = 1;
  sort: string = 'carNumber asc';
  totalSize: number = 0;
  keyword: string = '';

  showedColumnList = [
    { name: 'code', translateKey: 'code', isShowed: true, sortable: true },
    { name: 'createdDate', translateKey: 'CreatedDate', isShowed: true, sortable: true },
    { name: 'totalMoney', translateKey: 'TotalMoney', isShowed: true, sortable: false },
    { name: 'address', translateKey: 'address', isShowed: true, sortable: false },
    { name: 'userName', translateKey: 'user_name', isShowed: true, sortable: false },
  ];

  constructor(

    public helperService: HelperService,
    private modalService: NgbModal,
    private translateService: TranslateService,
    private orderService: OrderService


  ) {
  }
  async ngOnInit() {
    await this.getList();
  }

  async getList() {
    try {
      let response = await this.orderService.getList(this.page - 1, CONSTANT.PAGE_SIZE, this.keyword, this.sort);
      this.dataList = response.data;
      this.totalSize = response.totalSize;
    } catch (error) {

    }
  }

  detectSortClassName(fieldName: string): string {
    return this.helperService.detectSortClassName(this.sort, fieldName);
  }

  onPageChange(event): void {
    this.page = event;
    this.getList();
  }
  onChangeSortedField(fieldName: string): void {
    this.sort = this.helperService.handleSortedFieldNameChanged(this.sort, fieldName);
    this.getList();
  }
  onClickSearchBtn(): void {
    this.getList();
  }
  onClickDetailBtn(item: any): void {
    const modalRef = this.modalService.open(OrderDetailModalComponent, { backdrop: 'static', size: 'lg' });
    modalRef.componentInstance.editedModel = item;
  }
  onClickDeleteBtn(model: any): void {
    const modalRef = this.modalService.open(DeleteDialogComponent, { backdrop: 'static' });
    modalRef.componentInstance.reload = () => {
      this.getList();
    };

    this.translateService.get('delete_car').subscribe((res: string) => {
      modalRef.componentInstance.title = res;
    })

    modalRef.componentInstance.deleteFunction = () => {

    }
  }
}
