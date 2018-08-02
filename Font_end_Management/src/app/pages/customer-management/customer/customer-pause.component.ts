import { Component, Input, OnInit } from '@angular/core';
import {  NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { UserService } from 'app/@core/data/user.service';
import { HelperService } from 'app/@core/utils/helper.service';

@Component({
    selector: 'customer-pause-modal-component',
    templateUrl: './customer-pause.component.html',
    providers: [
    ]
})
export class CustomerPauselModalComponent implements OnInit {
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
    async onClickPauseBtn(){
        // try{
        //     await this.userService.changeActiveStatus(this.model.id, false);
        //     this.helperService.showSuccessToast('success', 'pause_product_successfully');
        //     this.activeModal.close();
        // }catch(error) {
        //     this.helperService.showErrorToast(error);
        // }

    }


}
