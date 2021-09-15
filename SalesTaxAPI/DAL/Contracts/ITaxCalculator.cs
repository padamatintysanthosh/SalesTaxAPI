using SalesTaxAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesTaxAPI.DAL.Contracts
{
    public interface ITaxCalculator
    {
        TaxRateModel GetSalesTaxForLocation(LocationReqest request);
        TaxOrderModel CalculateSalesTaxForOrder(OrderRequest request);
    }
}
