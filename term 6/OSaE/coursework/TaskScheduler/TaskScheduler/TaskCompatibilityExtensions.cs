using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerProject
{
    internal static class TaskCompatibilityExtensions
    {
        public static string ToFriendlyString(TaskCompatibility tc)
        {
            switch (tc)
            {
                case TaskCompatibility.AT:
                    return "AT command";
                case TaskCompatibility.V1:
                    return "Windows Server™ 2003, Windows® XP, or Windows® 2000";
                case TaskCompatibility.V2:
                    return "Windows Vista™, Windows Server™ 2008";
                case TaskCompatibility.V2_1:
                    return "Windows® 7, Windows Server™ 2008 R2";
                case TaskCompatibility.V2_2:
                    return "Windows® 8.x, Windows Server™ 2012";
                case TaskCompatibility.V2_3:
                    return "Windows® 10, Windows Server™ 2016";
                default: return "";
            }
        }
    }
}
