import { Component, Input, Output, OnInit } from '@angular/core';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { HelperService } from '../../../@core/utils/helper.service';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { NgbDatepickerConfig, NgbDateStruct, NgbDateParserFormatter, NgbDatepickerI18n } from '@ng-bootstrap/ng-bootstrap';
import { NgbDateFRParserFormatter } from "../../commons/ng-bootstrap-datepicker-util/ngb-date-fr-parser-formatter";
import { CustomDatepickerI18n, I18n } from "../../commons/ng-bootstrap-datepicker-util/ngbd-datepicker-i18n";
import { BranchService } from 'app/@core/data/branch.service';
import { CompanyService } from 'app/@core/data/company.service';

@Component({
    selector: 'branch-update-modal-component',
    templateUrl: './branch-update.component.html',
    providers: [{ provide: NgbDateParserFormatter, useClass: NgbDateFRParserFormatter },
        I18n, { provide: NgbDatepickerI18n, useClass: CustomDatepickerI18n }
    ]
})

export class BranchUpdateModalComponent implements OnInit {
    @Input() editedModel: any;
    @Input() reload: any;

    private today: any = this.helperService.getTodayForDatePicker();

    model: any = {
    };
    isEditMode = false;
    allDemos: any = [];
    isDuplicatedName = false;
    providersList: any = [];
    companyList: any = [];
    selectedDate: any = this.today;
    isKeepOpen: boolean = false;
    dataListCode:any = [];
    isDuplicatedCode = false;

    constructor(public activeModal: NgbActiveModal,
        public helperService: HelperService,
        private toastrService: ToastrService,
        private branchService : BranchService,
        private translateService: TranslateService,
        private companyService: CompanyService,
        private i18n: I18n,
        config: NgbDatepickerConfig,
    ) {
        // config maxDate and languge for date picker
        config.maxDate = this.today;
        this.i18n.language = this.translateService.currentLang;
    }

    async ngOnInit() {
        if (this.editedModel) {
            this.isEditMode = true;
            this.model = this.helperService.deepCopy(this.editedModel);
        }
        this.getAllDemos();
        this.getallCompanies();

    }
    async getallCompanies() {
        const response = await this.companyService.getAll();
        this.companyList = response.data;
    }

    isDuplicatedForm() {
        return this.isDuplicatedName;
    }

    async getAllDemos() {
        const response = await this.branchService.getAll();
        this.allDemos = response.data;
    }

    async onChangeNameValue(id, value) {
        this.isDuplicatedName = this.helperService.isDuplicatedValue(id, value, 'name', this.allDemos);
    }

    async onClickSaveBtn() {
        try {
            if (this.isEditMode) {
                let response = await this.branchService.edit(this.model.id, this.model);
                this.helperService.showEditSuccessToast();
            } else {
                let response = await this.branchService.add(this.model);
                this.helperService.showAddSuccessToast();
            }
            this.activeModal.close();
        } catch (error) {
            this.helperService.showErrorToast(error);
        }
    }

}
