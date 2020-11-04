using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AkciqApp.Constants
{
    public class WebConstantsVariables
    {
        public static string User { get; private set; } = "User";
        public static string Admin { get; private set; } = "Admin";

        public static bool PromotionStarted { get; set; }
    }
}
