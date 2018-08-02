using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public  static class CONSTANT

    {
        public static int TIME_ZONE = 7; // vietnam time zone

        // Status for Product Storage filter 
        public static string SERVICE_STORAGE_ALL = "ALL";
        public static string SERVICE_STORAGE_HAS_INVENTORY = "HAS_INVENTORY";
        public static string SERVICE_STORAGE_SOLD_OUT = "SOLD_OUT";

        // Status for Order 
        public static string BOOKED = "BOOKED";
        public static string PAID = "PAID";
        public static string CANCELED = "CANCELED";

        // Product Category Types

        public static string SERVICE_CATEGORY_TIRE = "TIRE";
        public static string SERVICE_CATEGORY_TRAVEL_TIRE = "TRAVEL_TIRE";
        public static string SERVICE_CATEGORY_TRUCK_TIRE = "TRUCK_TIRE";

        public static string BILL_PREFIX = "DH";
        public static string PROMOTION_PREFIX = "KM";

        public static int GENERATED_NUMBER_LENGTH = 6; // ex: 0000001 on HD2018000001

        public static string ROLE_OWNER = "OWNER";
        public static string ROLE_MANAGER = "MANAGER";
        public static string ROLE_SALEMAN = "SALEMAN";
        public static string ROLE_WAREHOUSE = "WAREHOUSE";
    }
}
