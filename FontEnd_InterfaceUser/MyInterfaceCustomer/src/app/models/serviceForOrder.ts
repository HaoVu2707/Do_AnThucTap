export class ServiceForOrder {
  id: string;
  code: string;
  imageUrl : string;
  branchId: string;
  name: string;
  description: string;
  price: number;
  discount: number;
  amount: number;

  constructor() {
      this.id = '';
      this.branchId = '';
    this.amount = 1;
  }
  setProductStorage(id: string,
     code: string,imageUrl:string,
    branchId:string, amount: number,name: string,
    description: string,price: number,discount: number) {
      this.id = id;
      this.name = name;
      this.amount = amount;
      this.branchId= branchId;
      this.code= code;
      this.description = description;
      this.discount = discount;
      this.price = price;
      this.imageUrl = imageUrl;
  }
  setBeginProductStorage(id: string,  code: string,imageUrl:string,
    branchId:string ,name: string,
    description: string,price: number,discount: number, amount: number) {
      this.id = id;
      this.name = name;
      this.branchId= branchId;
      this.code= code;
      this.description = description;
      this.discount = discount;
      this.price = price;
      this.imageUrl = imageUrl;
      this.amount = amount;

  }
  setAmout() {
    this.amount = 1;
  }

}
