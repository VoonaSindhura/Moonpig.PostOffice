using System;
using System.Collections.Generic;

namespace Moonpig.PostOffice.Api.Service
{
    public interface IDispatchDateCalculatorService
    {
        DateTime CalculateDispatchDate(List<int> productIds, DateTime orderDate);
        DateTime AdjustedDispatchDate(DateTime orderDate, int leadTime);
    }
}
