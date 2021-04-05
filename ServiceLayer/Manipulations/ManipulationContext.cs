using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Manipulations
{
    public class ManipulationContext
    {
        public OrderingContext OrderingContext { get; set; }
        public FiltrationContext FiltrationContext { get; set; }

        public ManipulationContext(FiltrationContext filtrationContext, OrderingContext orderingContext)
        {
            OrderingContext = orderingContext;
            FiltrationContext = filtrationContext;
        }
    }
}
