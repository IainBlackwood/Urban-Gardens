using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcleUrbanGardens.Web.Models
{
    public static class Constants
    {
        public static readonly string CATEGORY_UNASSIGNED_PRODUCTS = "Unassigned-Products";

        public static readonly Dictionary<string, string> ROW_OPTIONS = new Dictionary<string, string>()
            {
                { "All", "0" },
                { "100", "100" },
                { "50", "50" },
                { "30", "30" },
                { "20", "20" },
                { "10", "10" },
                { "5", "5" }
            };

        public static readonly Dictionary<string, string> SMALL_GRID_ROW_OPTIONS = new Dictionary<string, string>()
            {
                { "10", "10" },
                { "5", "5" },
                { "1", "1" }
            };

        // set to false when live 
        public static readonly bool SHOW_HISTORICAL_DATA = false;
    }
}