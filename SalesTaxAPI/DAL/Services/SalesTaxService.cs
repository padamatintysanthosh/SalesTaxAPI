using Microsoft.Extensions.Configuration;
using SalesTaxAPI.DAL.Contracts;
using SalesTaxAPI.Helpers;
using SalesTaxAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace SalesTaxAPI.DAL.Services
{
    public class SalesTaxService: ISalesTaxService
    {
        private IConfiguration _configuration;
        private IHttpClientHelper _httpClient;
        private ITaxCalculator _taxCalculator;
        private readonly List<TaxCalculator> TaxCalculators;

        private string errorMessage = string.Empty;
        public SalesTaxService(IConfiguration configuration)
        {            
            TaxCalculators = new List<TaxCalculator>();
            _configuration = configuration;
            _configuration.GetSection("TaxCalculators").Bind(TaxCalculators);
        }

        public TaxResponse<TaxOrderModel> CalculateSalesTaxForOrder(OrderTaxRequest request)
        {
            TaxResponse<TaxOrderModel> response = new();
            InitializeTaxCalculator(request.TaxCalculatorId);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                response.ErrorMessage = errorMessage;
                response.IsSuccess = false;

                return response;
            }

            response.Result = _taxCalculator.CalculateSalesTaxForOrder(request.OrderRequest);
            response.ErrorMessage = errorMessage;
            response.IsSuccess = string.IsNullOrEmpty(errorMessage);

            return response;
        }

        public TaxResponse<TaxRateModel> GetSalesTaxForLocation(LocationTaxRequest request)
        {
            TaxResponse<TaxRateModel> response = new();
            InitializeTaxCalculator(request.TaxCalculatorId);

            if(!string.IsNullOrEmpty(errorMessage))
            {
                response.ErrorMessage = errorMessage;
                response.IsSuccess = false;

                return response;
            }

            response.Result = _taxCalculator.GetSalesTaxForLocation(request.LocationRequest);
            
            response.ErrorMessage = errorMessage;
            response.IsSuccess = string.IsNullOrEmpty(errorMessage);

            return response;
        }

        private void InitializeTaxCalculator(int taxCalculatorId)
        {
            var taxCalculator = TaxCalculators.FirstOrDefault(x => x.Id == taxCalculatorId);

            if (taxCalculator != null)
            {               
                if (_httpClient == null)
                {
                    _httpClient = new HttpClientHelper(taxCalculator);
                }

                switch (taxCalculatorId)
                {
                    case 1:

                        _taxCalculator = new TaxCalculator1(_httpClient);
                        break;
                    case 2:

                        _taxCalculator = new TaxCalculator2(_httpClient);
                        break;
                    default:
                        errorMessage = "Invalid TaxCalculator";
                        return;
                }
            }
            else
            {
                // Logging error
                errorMessage = "TaxAPI not available for the given TaxCalculator";
            }
        }

        public IHttpClientHelper ClientHelper
        {
            get { return _httpClient; }
            set { this._httpClient = value; }
        }       
    }
}
