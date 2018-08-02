import { Component, Input, Output, OnInit , EventEmitter} from '@angular/core';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { HelperService } from '../../../@core/utils/helper.service';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { NgbDatepickerConfig, NgbDateStruct, NgbDateParserFormatter, NgbDatepickerI18n } from '@ng-bootstrap/ng-bootstrap';
import { CustomerService } from 'app/@core/data/customer.service';
import { NgbDateFRParserFormatter } from '../../../pages/commons/ng-bootstrap-datepicker-util/ngb-date-fr-parser-formatter';
import { CustomDatepickerI18n, I18n } from '../../../pages/commons/ng-bootstrap-datepicker-util/ngbd-datepicker-i18n';
import { CONSTANT } from '../../../constant';
import { AuthService } from '../../../@core/data/auth.service';
import { UserService } from '../../../@core/data/user.service';


@Component({
    selector: 'signup-modal-component',
    templateUrl: './signup.component.html',
    providers: [{ provide: NgbDateParserFormatter, useClass: NgbDateFRParserFormatter },
        I18n, { provide: NgbDatepickerI18n, useClass: CustomDatepickerI18n }
    ]
})

export class SignUpComponent implements OnInit {
    @Input() editedModel: any;
    @Input() reload: any;
    @Output() userCreated = new EventEmitter();
    private today: any = this.helperService.getTodayForDatePicker();

    model: any = {
      roleNames : [CONSTANT.ROLES.WAREHOUSE]
  };
  isEditMode = false;
  isKeepOpen: boolean = false;
  justClickSignBtn: boolean = false;

    constructor(public activeModal: NgbActiveModal,
        public helperService: HelperService,
        private toastrService: ToastrService,
        private translateService: TranslateService,
        private authService: AuthService,
        private usersService: UserService,
        private i18n: I18n,
        config: NgbDatepickerConfig,
    ) {
        // config maxDate and languge for date picker
        config.maxDate = this.today;
        this.i18n.language = this.translateService.currentLang;
    }

    async ngOnInit() {
    }

    async onClickSignUpBtn() {
      this.justClickSignBtn = true;
      setTimeout(async () => {
          // this.model.roleNames = [this.selece]
          try {
              let response = await this.usersService.add(this.model);
              this.helperService.showSignUpSuccessToast();
              if (this.userCreated) {
                  this.userCreated.emit(response.id);
              }
              console.log(this.model);
          } catch (error) {
              console.log(this.model);
              this.helperService.showErrorToast(error);
          }
      }, CONSTANT.SAVE_DELAY_TIME);
      this.justClickSignBtn = false;

    }
}
