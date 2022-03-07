using System;
using System.Collections.Generic;
using System.Text;

namespace NEFBDAACommons.Shared.Models.Chart
{
    public class ChartDataViewModel<XType, YType>
    {
        public XType X { get; set; }
        public YType Y { get; set; }
    }
}
