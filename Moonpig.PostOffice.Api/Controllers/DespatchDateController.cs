namespace Moonpig.PostOffice.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Model;

    [Route("api/[controller]")]
    public class DespatchDateController : Controller
    {
        DispatchDateCalculator _dispatchCalculator;

        public DespatchDateController(DispatchDateCalculator dispatchCalculator)
        {
            _dispatchCalculator = dispatchCalculator;
        }

        [HttpGet]
        public IActionResult Get(List<int> productIds, DateTime orderDate)
        {
            try
            {
                DateTime maxLeadTime = _dispatchCalculator.CalculateDispatchDate(productIds, orderDate);

                if (maxLeadTime == DateTime.MinValue)
                    return BadRequest("Invalid input or unable to calculate max lead time.");

                return Ok(new DespatchDate { Date = maxLeadTime });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"Invalid operation: {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Specific exception: {ex.Message}");
            }
        }

    }
}
