import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { LocalDataSource, ViewCell } from 'ng2-smart-table';
import { HelperService } from '../../../@core/utils/helper.service';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CompanyUpdateModalComponent } from './company-update.component';
import { DeleteDialogComponent } from '../../commons/delete-dialog/delete-dialog.component';
import { TranslateService } from '@ngx-translate/core';
import { CompanyService } from 'app/@core/data/company.service';
import { CONSTANT } from 'app/constant';
import { AccessiblePageService } from 'app/@core/data/accessible-page.service';

@Component({
  selector: 'my-demo',
  templateUrl: './company.component.html',
  styles: [`
  `],
})
export class CompanyComponent implements OnInit {

  dataList = [];
  page: number = 1;
  sort: string = 'name asc';
  totalSize: number = 0;
  keyword: string = '';

  showedColumnList = [
    { name: 'code', translateKey: 'code', isShowed: true, sortable: true },
    { name: 'name', translateKey: 'name_company', isShowed: true, sortable: true },
    { name: 'address', translateKey: 'address', isShowed: true, sortable: true },
    { name: 'phoneNumber',translateKey: 'phoneNumber', isShowed: true, sortable: true }

  ];

  constructor(
    private productService: CompanyService,
    public helperService: HelperService,
    private modalService: NgbModal,
    private translateService: TranslateService,
    private pagesService: AccessiblePageService
  ) {

  }
  async ngOnInit() {
    await this.getList();
  }
  async getList() {
    try {
      let response = await this.productService.getList(this.page - 1, CONSTANT.PAGE_SIZE, this.keyword, this.sort);
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
  onClickAddBtn(): void {
    const modalRef = this.modalService.open(CompanyUpdateModalComponent, { backdrop: 'static' });
    modalRef.componentInstance.reload = () => {
      this.getList();
    };
  }
  onClickEditBtn(model: any): void {
    const modalRef = this.modalService.open(CompanyUpdateModalComponent, { backdrop: 'static' });
    modalRef.componentInstance.editedModel = model;
    modalRef.result.then(( closeData=>{
      this.getList();
    })).catch(dismissData =>{

    });

  }
  onClickDeleteBtn(model: any): void {
    const modalRef = this.modalService.open(DeleteDialogComponent, { backdrop: 'static' });
    // console.log()
    modalRef.componentInstance.reload = () => {
      this.getList();
    };
    this.translateService.get("delete_demo").subscribe((res: string) => {
      modalRef.componentInstance.title = res;
    });
    modalRef.componentInstance.deleteFunction = () => {
      return this.productService.delete(model.id);
    }
 }

  formatDate(jsonDate: string): string {
    return this.helperService.convertJSONDatetoDayMonthYear(jsonDate);
  }
}
