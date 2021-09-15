using SalesTaxAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesTaxAPI.DAL.Contracts
{
    public interface ISalesTaxService
    {
        TaxResponse<TaxRateModel> GetSalesTaxForLocation(LocationTaxRequest request);
        TaxResponse<TaxOrderModel> CalculateSalesTaxForOrder(OrderTaxRequest request);
    }
}
