﻿using System;
using System.Collections.Generic;
using System.Linq;
using Moonpig.PostOffice.Data;

namespace Moonpig.PostOffice.Api
{
    public class DispatchDateCalculator
    {
        private readonly DbContext _dbContext;

        public DispatchDateCalculator(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DateTime CalculateDispatchDate(List<int> productIds, DateTime orderDate)
        {
            DateTime dispatchDate = orderDate;

            foreach (var productId in productIds)
            {
                var product = _dbContext.Products.SingleOrDefault(x => x.ProductId == productId);

                if (product != null)
                {
                    var supplier = _dbContext.Suppliers.SingleOrDefault(x => x.SupplierId == product.SupplierId);

                    if (supplier != null)
                    {
                        var leadTime = supplier.LeadTime;

                        dispatchDate = AdjustedDispatchDate(orderDate, leadTime);
                    }
                    else
                    {
                        // Handle case where supplier is not found
                        throw new InvalidOperationException($"Supplier not found for ProductId: {productId}");
                    }
                }
                else
                {
                    // Handle case where product is not found
                    throw new InvalidOperationException($"Product not found for ProductId: {productId}");
                }
            }

            return dispatchDate;
        }

        public DateTime AdjustedDispatchDate(DateTime orderDate, int leadTime)
        {
            var dispatchDate = orderDate;

            // Ensure the order date is not on a weekend
            while (dispatchDate.DayOfWeek == DayOfWeek.Saturday || dispatchDate.DayOfWeek == DayOfWeek.Sunday)
            {
                dispatchDate = dispatchDate.AddDays(1);
            }

            // Adjust for lead time (without adjusting the order date)
            for (var i = 0; i < leadTime; i++)
            {
                do
                {
                    dispatchDate = dispatchDate.AddDays(1);
                } 
                while (dispatchDate.DayOfWeek == DayOfWeek.Saturday || dispatchDate.DayOfWeek == DayOfWeek.Sunday);
            }

            return dispatchDate;
        }
    }
}
