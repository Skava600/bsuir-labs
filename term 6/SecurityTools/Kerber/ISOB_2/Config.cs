using System;
using System.Collections.Generic;
using System.Text;

namespace ISOB_2
{
    public static class Config
    {
        public const int C_port = 8000;
        public const int AS_port = 8001;
        public const int SS_port = 8002;
        public const int TGS_port = 8003;

        public const string tgs = "tgs";
        public const string SS_ID = "SS_ID";

        public static readonly byte[] K_C = Helper.ExtendKey("K_C");
        public static readonly byte[] K_C_TGS = Helper.ExtendKey("K_C_TGS");
        public static readonly byte[] K_C_SS = Helper.ExtendKey("K_C_SS");
        public static readonly byte[] K_AS_TGS = Helper.ExtendKey("K_AS_TGS");
        public static readonly byte[] K_TGS_SS = Helper.ExtendKey("K_TGS_SS");
       
        public static readonly TimeSpan ASTicketDuration = new TimeSpan(24, 0, 0);
        public static readonly TimeSpan TGSTicketDuration = new TimeSpan(12, 0, 0);



    }
}
