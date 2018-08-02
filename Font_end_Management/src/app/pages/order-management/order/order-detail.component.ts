import { Component, Input, OnInit } from '@angular/core';
import { HelperService } from '../../../@core/utils/helper.service';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
@Component({
    selector: 'order-detail-modal-component',
    templateUrl: './order-detail.component.html',
    providers: [
    ]
})
export class OrderDetailModalComponent implements OnInit {
    @Input() editedModel: any;
    @Input() reload: any;
    model: any = {
    };

    constructor(
        public helperService: HelperService,
        public activeModal: NgbActiveModal
    ) { }

    async ngOnInit() {
        this.model = this.helperService.deepCopy(this.editedModel);
    }

}
