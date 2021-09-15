using Newtonsoft.Json;
using SalesTaxAPI.DAL.Contracts;
using SalesTaxAPI.Models;
using System;

namespace SalesTaxAPI.DAL
{
    public class TaxCalculator1 : ITaxCalculator
    {
        IHttpClientHelper httpClient;
        public TaxCalculator1(IHttpClientHelper client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client", "httpclient is null.");
            }

            httpClient = client;
        }

        public TaxOrderModel CalculateSalesTaxForOrder(OrderRequest request)
        {
            var result = httpClient.ExecutePost(request);
            TaxOrderModel response = JsonConvert.DeserializeObject<TaxOrderModel>(result);

            return response;
        }

        public TaxRateModel GetSalesTaxForLocation(LocationReqest request)
        {
            TaxRateModel response = new();

            var result = httpClient.ExecuteGet(request);
            response = JsonConvert.DeserializeObject<TaxRateModel>(result);

            return response;
        }
    }
}
