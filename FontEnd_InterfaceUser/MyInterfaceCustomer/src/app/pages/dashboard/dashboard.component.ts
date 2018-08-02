import { Component, OnInit } from '@angular/core';
import { HelperService } from 'app/@core/utils/helper.service';
import { AccessiblePageService } from 'app/@core/data/accessible-page.service';
import { SellingServiceService } from '../../@core/data/sellingService.service';
import { ServiceService } from '../../@core/data/service.service';
import {ServiceCategoryService} from '../../@core/data/serviceCategory.service';
import { forEach } from '@angular/router/src/utils/collection';
import { count } from 'rxjs/operator/count';
import { CONSTANT } from '../../constant';
import {ServiceCart}  from '../../models/service';
import { UserService } from '../../@core/data/user.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { LoginComponent } from '../../@theme/components/login/login.component';
declare var $: any;

@Component({
  selector: 'my-dashboard',
  styleUrls: ['./dashboard.component.scss'],
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent implements OnInit {
  constructor(
    public helperService: HelperService,
    public sellingServiceService: SellingServiceService,
    public serviceCategoryService: ServiceCategoryService,
    private userService: UserService,
    private modalService: NgbModal,
    private accessiblePagesSerivce: AccessiblePageService
  ) {
  }
  saleMoneyData = {
    totalMoney: 0,
    totalCash: 0,
    totalBank: 0,
    totalCard: 0
  };
  todayString: string;
  isValidRole = false;
  productList : any = [];
  serviceList : any = []  ;


  async ngOnInit() {
    this.getAllService();
    this.helperService.setLocalStorage("ProductList",null);

  }
async AddLocalCart(data){
  let userProfile = await this.userService.getCurrentUser();
  if (userProfile == null) {
    const modalRef = this.modalService.open(LoginComponent, { backdrop: 'static' });
  }
  else {
    let list　= this.helperService.getLocalStorage("ProductList") || [];

 if(list.length != 0){
   for(let i = 0; i < list.length; i ++ ){
      if(data.id == list[i].id){
        list[i].amount ++;
        break;
      }
      else if(i == list.length - 1){
        let newServiceCart1 :ServiceCart ;
        newServiceCart1 = new ServiceCart();
        var discountMoney = 0;
        console.log(data.isDiscountMoney);
        if(data.isDiscountMoney.toString() == CONSTANT.VALUES_TRUE){
          discountMoney = data.discount;
        }
        else if(data.isDiscountMoney.toString() == CONSTANT.VALUES_FALSE){
          discountMoney = data.price * data.discount / 100;

        }
        newServiceCart1.setBeginProductStorage(data.id,data.serviceId ,data.service.serviceCatagoryId, data.service.code ,data.service.imageUrl,
          data.branchId,data.service.name,data.service.description,data.price,discountMoney);

        list.push(newServiceCart1);
        break;
      }
   }

 }
 else{
   let newServiceCart : ServiceCart  ;
   newServiceCart = new ServiceCart();
   var discountMoney = 0;
   if(data.isDiscountMoney.toString() == CONSTANT.VALUES_TRUE){
    discountMoney = data.discount;
   }
   else if(data.isDiscountMoney.toString() == CONSTANT.VALUES_FALSE){
    discountMoney = data.price * data.discount / 100;
   }
   newServiceCart.setBeginProductStorage(data.id,data.serviceId ,data.service.serviceCatagoryId, data.service.code ,data.service.imageUrl,
    data.branchId,data.service.name,data.service.description,data.price,discountMoney);
  list.push(newServiceCart);
  console.log(list);
 }
 localStorage.setItem("ProductList", JSON.stringify(list));

  let products = this.helperService.getLocalStorage("ProductList")||[];
  console.log(products);
  }


}
getLocalProductCart(){
  let products = this.helperService.getLocalStorage("ProductList");
  console.log(products);

}
getCountLocalProductCart(){
  return this.productList.length;
}
async getAllService() {
  let response = await this.sellingServiceService.getAllForCustomer();
  this.serviceList = response.data;
  console.log(this.serviceList);
}
}
