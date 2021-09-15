using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SalesTaxAPI.DAL.Contracts;
using SalesTaxAPI.Models;
using System;

namespace SalesTaxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesTaxController : ControllerBase
    {
        private readonly ISalesTaxService _taxService;

        public SalesTaxController(ISalesTaxService taxService)
        {
            _taxService = taxService;
        }

        [HttpGet,Route("GetSalesTaxForLocation")]
        public TaxResponse<TaxRateModel> GetSalesTaxForLocation([FromQuery]LocationTaxRequest request)
        {
            TaxResponse<TaxRateModel> response = new();
            if (request.TaxCalculatorId <= 0)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "TaxCalculatorId is invalid.";
                return response;
            }
            else if (request.LocationRequest == null || string.IsNullOrWhiteSpace(request.LocationRequest.zip))
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Zipcode is mandatory, Please check your input.";
                return response;
            }
           

            try
            {
                response = _taxService.GetSalesTaxForLocation(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = $"GetSalrdTaxForLocation failed with the error :{ex.Message}";
            }

            return response;
        }

        [HttpPost, Route("CalculateSalesTaxForOrder")]
        public TaxResponse<TaxOrderModel> CalulateSalesTaxForOrder(OrderTaxRequest request)
        {
            TaxResponse<TaxOrderModel> response = new();
            if (request.TaxCalculatorId <= 0)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "TaxCalculatorId is invalid.";
                return response;
            }

            try
            {
                response = _taxService.CalculateSalesTaxForOrder(request);                
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = $"CalculateSalesTaxForOrder failed with error :{ex.Message}";
            }

            return response;
        }
    }
}
