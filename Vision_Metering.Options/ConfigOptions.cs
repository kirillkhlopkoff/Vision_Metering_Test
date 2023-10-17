using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vision_Metering.Options
{
    public class ConfigOptions
    {
        public string ConnectionString { get; set; }
        public string? CounterName { get; set; }
        public int? CounterValue { get; set; }
        public bool IsChecked { get; set; }
    }
}
