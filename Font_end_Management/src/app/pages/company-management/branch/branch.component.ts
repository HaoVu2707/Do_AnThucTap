import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { LocalDataSource, ViewCell } from 'ng2-smart-table';
import { HelperService } from '../../../@core/utils/helper.service';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { BranchUpdateModalComponent } from './branch-update.component';
import { DeleteDialogComponent } from '../../commons/delete-dialog/delete-dialog.component';
import { TranslateService } from '@ngx-translate/core';
import { BranchService } from 'app/@core/data/branch.service';
import { CONSTANT } from 'app/constant';
import { AccessiblePageService } from 'app/@core/data/accessible-page.service';

@Component({
  selector: 'my-demo',
  templateUrl: './branch.component.html',
  styles: [`
  `],
})
export class BranchComponent implements OnInit {

  dataList = [];
  page: number = 1;
  sort: string = 'name asc';
  totalSize: number = 0;
  keyword: string = '';

  showedColumnList = [
    { name: 'name', translateKey: 'name', isShowed: true, sortable: true },
    { name: 'address', translateKey: 'address', isShowed: true, sortable: true },
    { name: 'phoneNumber',translateKey: 'phoneNumber', isShowed: true, sortable: true },
    { name: 'company',translateKey: 'companyName', isShowed: true, sortable: true }

  ];

  constructor(
    private branchService: BranchService,
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
      let response = await this.branchService.getList(this.page - 1, CONSTANT.PAGE_SIZE, this.keyword, this.sort);
      console.log(response);
      this.dataList = response.data;
      console.log(this.dataList);
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
    const modalRef = this.modalService.open(BranchUpdateModalComponent, { backdrop: 'static' });
    modalRef.result.then(( closeData=>{
      this.getList();
    })).catch(dismissData =>{
    });
  }
  onClickEditBtn(model: any): void {
    const modalRef = this.modalService.open(BranchUpdateModalComponent, { backdrop: 'static' });
    modalRef.componentInstance.editedModel = model;
    modalRef.result.then(( closeData=>{
      this.getList();
    })).catch(dismissData =>{

    });

  }
  onClickDeleteBtn(model: any): void {
    const modalRef = this.modalService.open(DeleteDialogComponent, { backdrop: 'static' });
    modalRef.result.then( closeData =>{
      this.getList();
    }).catch( dismssData =>{

    })

    this.translateService.get("delete_demo").subscribe((res: string) => {
      modalRef.componentInstance.title = res;
    });
    modalRef.componentInstance.deleteFunction = () => {
      return this.branchService.delete(model.id);
    }
  }

  formatDate(jsonDate: string): string {
    return this.helperService.convertJSONDatetoDayMonthYear(jsonDate);
  }
}
