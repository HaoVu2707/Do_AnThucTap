import { Component, Input, OnInit } from '@angular/core';
import { NbMenuService, NbSidebarService } from '@nebular/theme';
import { AnalyticsService } from '../../../@core/utils/analytics.service';
import { TranslateService } from '@ngx-translate/core';
import { AuthService } from '../../../@core/data/auth.service';
import { UserService } from 'app/@core/data/user.service';
import { HelperService } from '../../../@core/utils/helper.service';
import { CONSTANT } from '../../../constant';
import { ServiceService } from '../../../@core/data/service.service';
//import { LoginComponent } from '../../../auth/login/login.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SignUpComponent, LoginComponent, CartComponent, MyCartComponent } from 'app/@theme/components';

@Component({
  selector: 'ngx-header',
  styleUrls: ['./header.component.scss'],
  templateUrl: './header.component.html',
})
export class HeaderComponent implements OnInit {



  @Input() position: string = 'normal';
  user: any = {};
  isLogin: boolean = false;

  userMenu = [];
  menuNot = [];

  amount : any ;
  constructor(private sidebarService: NbSidebarService,
    private menuService: NbMenuService,
    private userService: UserService,
    private analyticsService: AnalyticsService,
    private translateService: TranslateService,
    private authService: AuthService,
    public helperService : HelperService,
    public serviceService : ServiceService,
    private modalService: NgbModal,
  ) {
  }

  async ngOnInit() {

    try {
      let accessToken = this.helperService.getLocalStorage(CONSTANT.ACCESS_TOKEN);
      let userProfile = await this.userService.getCurrentUser();
      if (userProfile == null) {
        this.isLogin = false;
        this.getNotLoginMenu();
      }
      else {
        this.isLogin = true;
       this.getIsLoginMenu();
      }

      var data = await this.userService.getCurrentUser();
      this.user.name = data.firstName + " " + data.lastName;
      this.user.picture = "./assets/images/plt.jpg";

    } catch (error) {

    }

  }

  private getIsLoginMenu() {
    let logout = '';
    this.translateService.get('logout').subscribe((res: string) => {
      logout = res;

    });

    let profile = '';
    this.translateService.get('profile').subscribe((res: string) => {
      profile = res;
    });

    let order = '';
    this.translateService.get('order').subscribe((res: string) => {
      order = res;
    });

    console.log(profile);
    this.userMenu = [{ title: logout, key: 'LOGOUT' }, { title: profile, key: 'PROFILE' },{ title: order, key: 'ORDER' }];

  }

  private getNotLoginMenu() {

    let login = '';
    this.translateService.get('login').subscribe((res: string) => {
      login = res;
    });

    let signup = '';
    this.translateService.get('signup').subscribe((res: string) => {
      signup = res;
    });

    this.menuNot = [{ title: login, key: 'LOGIN' }, { title: signup, key: 'SIGNUP' }];
  }

  menuClick(item) {
    if (item.key == 'LOGIN') {
      const modalRef = this.modalService.open(LoginComponent, { backdrop: 'static' });
    }
    if (item.key == 'LOGOUT') {
      this.authService.logout();
    }
    if (item.key == 'SIGNUP') {
      const modalRef = this.modalService.open(SignUpComponent, { backdrop: 'static' });
    }
    if (item.key == 'ORDER') {
      const modalRef = this.modalService.open(MyCartComponent, { backdrop: 'static', size: 'lg' });
    }
  }


  getAmountServiceCart(){
    let list = this.helperService.getLocalStorage("ProductList") || [];
    return list.length ;
  }

  toggleSidebar(): boolean {
    this.sidebarService.toggle(true, 'menu-sidebar');
    return false;
  }

  toggleSettings(): boolean {
    this.sidebarService.toggle(false, 'settings-sidebar');
    return false;
  }

  goToHome() {
    this.menuService.navigateHome();
  }

  startSearch() {
    this.analyticsService.trackEvent('startSearch');
  }

  ShowYourCart (){
    const modalRef = this.modalService.open(CartComponent, { backdrop: 'static' , size: 'lg'});
  }

}
