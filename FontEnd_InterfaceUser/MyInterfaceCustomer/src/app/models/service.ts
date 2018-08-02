export class ServiceCart {
    id: string;
    serviceId: string;
    serviceCategoryId: string;
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
        this.serviceId = '';
        this.branchId = '';
      this.amount = 1;
    }
    setProductStorage(id: string, serviceId: string,serviceCategoryId: string, code: string,imageUrl:string,
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
        this.serviceId = serviceId;
        this.imageUrl = imageUrl;
        this.serviceCategoryId = serviceCategoryId;
    }
    setBeginProductStorage(id: string, serviceId: string,serviceCategoryId: string,  code: string,imageUrl:string,
      branchId:string ,name: string,
      description: string,price: number,discount: number) {
        this.id = id;
        this.name = name;
        this.branchId= branchId;
        this.code= code;
        this.description = description;
        this.discount = discount;
        this.price = price;
        this.serviceId = serviceId;
        this.imageUrl = imageUrl;
        this.serviceCategoryId = serviceCategoryId;

    }
    setAmout() {
      this.amount = 1;
    }

}
