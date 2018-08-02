import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { HelperService } from '../utils/helper.service';
import {BaseService} from './base.service';
@Injectable()
export class BranchService extends BaseService {

  constructor( public childHttp: Http, public childHelperService: HelperService) {
    super(childHttp, childHelperService, 'branchs');
  }

  // async Check_CodeCustomerss(): Promise<any> {
  //   try {
  //     let headers = this.childHelperService.getHeadersRequest();
  //     let options = new RequestOptions({ headers: headers });

  //     const response = await this.childHttp.get(this.domain + "/CheckCodeExist", options)
  //       .toPromise();
  //     return response.json();
  //   } catch (error) {
  //     this.childHelperService.handleError(error);
  //   }
  // }
  // async getAll_CodeCompanies(): Promise<any> {
  //   try {
  //     let headers = this.childHelperService.getHeadersRequest();
  //     let options = new RequestOptions({ headers: headers });

  //     const response = await this.childHttp.get(this.domain + "/getListCompanies", options)
  //       .toPromise();
  //     return response.json();
  //   } catch (error) {
  //     this.childHelperService.handleError(error);
  //   }
  // }

}
