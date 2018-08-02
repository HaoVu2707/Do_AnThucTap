import { Component, Input, Output, OnInit } from '@angular/core';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { HelperService } from '../../../@core/utils/helper.service';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { NgbDatepickerConfig, NgbDateStruct, NgbDateParserFormatter, NgbDatepickerI18n } from '@ng-bootstrap/ng-bootstrap';
import { CustomerService } from 'app/@core/data/customer.service';
import { NgbDateFRParserFormatter } from '../../../pages/commons/ng-bootstrap-datepicker-util/ngb-date-fr-parser-formatter';
import { CustomDatepickerI18n, I18n } from '../../../pages/commons/ng-bootstrap-datepicker-util/ngbd-datepicker-i18n';
import { AuthService } from '../../../@core/data/auth.service';
import { CONSTANT } from '../../../constant';
import { UserService } from '../../../@core/data/user.service';


@Component({
  selector: 'login-modal-component',
  templateUrl: './login.component.html',
  providers: [{ provide: NgbDateParserFormatter, useClass: NgbDateFRParserFormatter },
    I18n, { provide: NgbDatepickerI18n, useClass: CustomDatepickerI18n }
  ]
})

export class LoginComponent implements OnInit {
  user: any = {
    email: '',
    password: ''
  };
  @Input() editedModel: any;
  @Input() reload: any;

  private today: any = this.helperService.getTodayForDatePicker();

  isClickedLoginBtn: boolean = false;

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



  async onClickLoginBtn() {
    this.isClickedLoginBtn = true;
    try {
      let data = await this.authService.login(this.user.userName, this.user.password);
      this.helperService.setLocalStorage(CONSTANT.ACCESS_TOKEN, data['access_token']);
      // we need to calcalate token valid timestamp (on milisecons)
      let expiresIn = data['expires_in'] * 1000;
      let validTimeStamp = + new Date() + expiresIn;
      this.helperService.setLocalStorage(CONSTANT.VALID_TIMESTAMP, validTimeStamp);
      // get user profiles & role
      let userProfile = await this.usersService.getCurrentUser();
      this.helperService.setLocalStorage(CONSTANT.USER_PROFILE, userProfile);
      this.helperService.setLocalStorage(CONSTANT.CURRENT_ROLE, userProfile.roleNames[0]);
      // get accessiable storages
      // var storages = await this.userStorageService.getStoragesByUserId(userProfile.id);

      window.location.reload();
      console.log("Đăng nhập thành công");


    } catch (error) {
      let title = '';
      let message = '';
      this.translateService.get('error').subscribe((res: string) => {
        title = res;
      })
      this.translateService.get('wrong_username_or_password').subscribe((res: string) => {
        message = res;
      })
      this.toastrService.error(message, title);
      this.isClickedLoginBtn = false;
    }
  }
}
