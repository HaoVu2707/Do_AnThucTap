import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { LocalDataSource, ViewCell } from 'ng2-smart-table';
import { HelperService } from '../../../@core/utils/helper.service';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CustomerUpdateModalComponent } from './customer-update.component';
import { DeleteDialogComponent } from '../../commons/delete-dialog/delete-dialog.component';
import { TranslateService } from '@ngx-translate/core';
import { UserService } from 'app/@core/data/user.service';
import { CONSTANT } from 'app/constant';
import { CustomerPauselModalComponent } from './customer-pause.component';
import { CustomerActivelModalComponent } from './customer-active.component';

@Component({
  selector: 'my-customer',
  templateUrl: './customer.component.html',
  styles: [`
  `],
})
export class CustomerComponent implements OnInit {

  dataList = [];
  page: number = 1;
  sort: string = 'code asc';
  totalSize: number = 0;
  keyword: string = '';

  showedColumnList = [
    { name: 'userName', translateKey: 'user_name', isShowed: true, sortable: true },
    { name: 'fullName', translateKey: 'fullName', isShowed: true, sortable: true },
    { name: 'email', translateKey: 'email', isShowed: true, sortable: true },
    { name: 'phoneNumber', translateKey: 'phoneNumber', isShowed: true, sortable: false },
    { name: 'address', translateKey: 'address', isShowed: true, sortable: false },
    { name: 'roleNames', translateKey: 'role', isShowed: true, sortable: false },
    { name: 'isActive', translateKey: 'isActive', isShowed: true, sortable: false },
  ];

  constructor(
    private userService: UserService,
    public helperService: HelperService,
    private modalService: NgbModal,
    private translateService: TranslateService,
  ) {

  }
  async ngOnInit() {
    await this.getList();
  }
  async getList() {
    try {
      let response = await this.userService.getList(this.page - 1, CONSTANT.PAGE_SIZE, this.keyword, this.sort);
      this.dataList = [];
      this.dataList = response.data;
      this.totalSize = response.totalSize;
    } catch (error) {

    }
  }
  detectSortClassName(fieldName: string): string {
    return this.helperService.detectSortClassName(this.sort, fieldName);
  }

  // onPageChange(event): void {
  //   this.page = event;
  //   this.getList();
  // }
  // onChangeSortedField(fieldName: string): void {
  //   this.sort = this.helperService.handleSortedFieldNameChanged(this.sort, fieldName);
  //   this.getList();
  // }
  onClickSearchBtn(): void {
    this.getList();
  }
  onClickAddBtn(): void {
    const modalRef = this.modalService.open(CustomerUpdateModalComponent, { backdrop: 'static' });
    modalRef.result.then(closeData => {
      this.getList();
    }).catch(dismissData => {
    })
  }
  onClickEditBtn(model: any): void {
    const modalRef = this.modalService.open(CustomerUpdateModalComponent, { backdrop: 'static' });
    modalRef.componentInstance.editedModel = model;
    modalRef.result.then(closeData => {
      this.getList();
    }).catch(dismissData => {
    })
  }
  onClickDeleteBtn(model: any): void {
    const modalRef = this.modalService.open(DeleteDialogComponent, { backdrop: 'static' });
    modalRef.componentInstance.reload = () => {
      this.getList();
    };
    this.translateService.get("delete_post").subscribe((res: string) => {
      modalRef.componentInstance.title = res;
    });
    modalRef.componentInstance.deleteFunction = () => {
      return this.userService.delete(model.id);
    }
  }

  onClickPauseBtn(item: any) {
    const modalRef = this.modalService.open(CustomerPauselModalComponent, { backdrop: 'static' });
    modalRef.componentInstance.editedModel = item;
    modalRef.result.then( closeData=>{ this.getList() } ).catch( dissmissData=>{})
  }
  onClickActiveBtn(item: any){
    const modalRef = this.modalService.open( CustomerActivelModalComponent, { backdrop: 'static' });
    modalRef.componentInstance.editedModel = item;
    modalRef.result.then( closeData=>{ this.getList() } ).catch( dissmissData=>{})
  }

  formatDate(jsonDate: string): string {
    return this.helperService.convertJSONDatetoDayMonthYear(jsonDate);
  }
}
