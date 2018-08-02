import { Component, Input, Output, OnInit } from '@angular/core';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { HelperService } from '../../../@core/utils/helper.service';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { NgbDatepickerConfig, NgbDateStruct, NgbDateParserFormatter, NgbDatepickerI18n } from '@ng-bootstrap/ng-bootstrap';
import { NgbDateFRParserFormatter } from "../../commons/ng-bootstrap-datepicker-util/ngb-date-fr-parser-formatter";
import { CustomDatepickerI18n, I18n } from "../../commons/ng-bootstrap-datepicker-util/ngbd-datepicker-i18n";
import { RoleService } from 'app/@core/data/role.service';
import { UserService } from 'app/@core/data/user.service';


@Component({
    selector: 'customer-update-modal-component',
    templateUrl: './customer-update.component.html',
    providers: [{ provide: NgbDateParserFormatter, useClass: NgbDateFRParserFormatter },
        I18n, { provide: NgbDatepickerI18n, useClass: CustomDatepickerI18n }
    ]
})

export class CustomerUpdateModalComponent implements OnInit {
    @Input() editedModel: any;
    @Input() reload: any;

    private today: any = this.helperService.getTodayForDatePicker();

    model: any = {
    };
    isEditMode = false;
    isDuplicatedCode = false;
    allRoles: any = [];
    isKeepOpen: boolean = false;
    allCustomers: any = [];


    constructor(public activeModal: NgbActiveModal,
        public helperService: HelperService,
        private toastrService: ToastrService,
        private roleService: RoleService,
        private userService: UserService,
        private i18n: I18n,
        config: NgbDatepickerConfig,
    ) {
    }

    async ngOnInit() {
        if (this.editedModel) {
            this.isEditMode = true;
            this.model = this.helperService.deepCopy(this.editedModel);
        }
        await this.getAllRoles();
        await this.getAllCustomers();
    }

    isDuplicatedForm() {
        return this.isDuplicatedCode;
    }
    private async getAllRoles() {
        let response = await this.roleService.getAll();
        this.allRoles = response.data;
    }
    private async getAllCustomers() {
        let response = await this.userService.getAll();
        this.allCustomers = response.data;
    }

    async onClickSaveBtn() {
        try {
            if (this.isEditMode) {
                let response = await this.userService.edit(this.model.id, this.model);
                this.helperService.showEditSuccessToast();
            } else {
                let response = await this.userService.add(this.model);
                this.helperService.showAddSuccessToast();
                if (this.isKeepOpen) {
                    this.getAllCustomers();
                    this.model.name = null;
                }
            }
            if (!this.isKeepOpen) {
                this.activeModal.close();
                this.reload();
            }
        } catch (error) {
            this.helperService.showErrorToast(error);
        }
    }
}
