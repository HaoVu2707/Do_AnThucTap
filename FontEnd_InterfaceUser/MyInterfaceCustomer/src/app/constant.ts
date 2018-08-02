import { environment } from '../environments/environment';

export const CONSTANT = {
    API_DOMAIN: environment.apiDomain,
    ACCESS_TOKEN: 'access_token',
    VALID_TIMESTAMP : 'valid_timestamp',
    PAGE_SIZE: 10,
    SAVE_DELAY_TIME : 300,
    USER_PROFILE: "user_profile",
    CURRENT_ROLE : 'current_role',
    ROLES: {
        OWNER: "OWNER",
        MANAGER: "MANAGER",
        SALESMAN: "SALESMAN",
        WAREHOUSE: "CUSTOMER"
    },
    EMAIL_PARTERN: "^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$",
    VALUES_TRUE :"true",
    VALUES_FALSE :"false"

}
