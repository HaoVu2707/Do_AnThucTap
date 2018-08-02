import { Component, Input, OnInit } from '@angular/core';
import {  NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { UserService } from 'app/@core/data/user.service';
import { HelperService } from 'app/@core/utils/helper.service';

@Component({
    selector: 'customer-active-modal-component',
    templateUrl: './customer-avtive.component.html',
    providers: [
    ]
})
export class CustomerActivelModalComponent implements OnInit {
    @Input() editedModel: any;

    model: any = {
    };

    constructor(public activeModal: NgbActiveModal,
        public helperService: HelperService,
        private userService: UserService
    ){}

    async ngOnInit() {
        this.model = this.helperService.deepCopy(this.editedModel);
    }
    async onClickActiveBtn(){
        // try{
        //     await this.userService.changeActiveStatus(this.model.id, true);
        //     this.helperService.showSuccessToast('success', 'active_product_successfully');
        //     this.activeModal.close();
        // }catch(error) {
        //     this.helperService.showErrorToast(error);
        // }

    }

}
