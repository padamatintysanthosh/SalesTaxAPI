using SalesTaxAPI.DAL.Contracts;
using SalesTaxAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SalesTaxAPI.DAL
{
    public class TaxCalculator2 : ITaxCalculator
    {
        private readonly IHttpClientHelper _httpClient;
        public TaxCalculator2(IHttpClientHelper httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException("client", "httpclient is null.");
        }

        public TaxOrderModel CalculateSalesTaxForOrder(OrderRequest request)
        {
            throw new NotImplementedException();
        }

        public TaxRateModel GetSalesTaxForLocation(LocationReqest request)
        {
            throw new NotImplementedException();
        }
    }
}
