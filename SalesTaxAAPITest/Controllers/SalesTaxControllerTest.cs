using Microsoft.Extensions.Configuration;
using Moq;
using SalesTaxAPI.Controllers;
using SalesTaxAPI.DAL.Contracts;
using SalesTaxAPI.Models;
using Xunit;

namespace SalesTaxAAPITest.Controllers
{
    public class SalesTaxControllerTest
    {
        Mock<ISalesTaxService> mockService;
        Mock<IConfiguration> configuration;
        SalesTaxController salesTaxController;

        public SalesTaxControllerTest()
        {
            mockService = new Mock<ISalesTaxService>();
            configuration = new Mock<IConfiguration>();

            salesTaxController = new SalesTaxController(mockService.Object);
        }

        [Fact]
        public void EmptyZipCodeTest()
        {
            // Setup
            LocationTaxRequest request = new LocationTaxRequest()
            {
                TaxCalculatorId = 1,
                LocationRequest = new LocationReqest()
            };

            // Action
            var response =  salesTaxController.GetSalesTaxForLocation(request);

            // Assertion
            Assert.NotNull(response);
            Assert.False(response.IsSuccess);
            Assert.Equal("zip is mandatory, Please check input.", response.ErrorMessage);
        }

        [Fact]
        public void InvalidTaxCalculatorIdForRateLocationTest()
        {
            // Setup
            LocationTaxRequest request = new LocationTaxRequest()
            {
                TaxCalculatorId = -1,
                LocationRequest = new LocationReqest()
        };

            // Action
            var response = salesTaxController.GetSalesTaxForLocation(request);

            // Assertion
            Assert.NotNull(response);
            Assert.False(response.IsSuccess);
            Assert.Equal("Invalid TaxCalculatorId.", response.ErrorMessage);
        }

        [Fact]
        public void InvalidTaxCalculatorIdForCalTaxForOrderTest()
        {
            // Setup
            OrderTaxRequest request = new OrderTaxRequest()
            {
                TaxCalculatorId = -1,
                OrderRequest = new OrderRequest()
            };

            // Action
            var response = salesTaxController.CalulateSalesTaxForOrder(request);

            // Assertion
            Assert.NotNull(response);
            Assert.False(response.IsSuccess);
            Assert.Equal("Invalid TaxCalculatorId.", response.ErrorMessage);
        }

        [Fact]       
        public void GetTaxRatesForLocation()
        {
            // Setup
            LocationTaxRequest request = new LocationTaxRequest()
            {
                TaxCalculatorId = 11,
                LocationRequest = new LocationReqest() { zip= "90404", country="US"}
            };

            TaxResponse<TaxRateModel> taxRes = new TaxResponse<TaxRateModel>()
            {
                IsSuccess = true,
                Result= new TaxRateModel()
            };

            mockService.Setup(x => x.GetSalesTaxForLocation(request)).Returns(taxRes);

            // Action
            var response = salesTaxController.GetSalesTaxForLocation(request);

            // Assertion
            Assert.NotNull(response);
            Assert.True(response.IsSuccess);            
        }

        [Fact]
        public void CalulateTaxForOrder()
        {
            // Setup
             OrderTaxRequest request = new OrderTaxRequest()
            {
                TaxCalculatorId = 1,
                OrderRequest = new OrderRequest()
            };

            TaxResponse<TaxOrderModel> taxRes = new TaxResponse<TaxOrderModel>()
            {
                IsSuccess = true,
                Result = new TaxOrderModel()
            };

            mockService.Setup(x => x.CalculateSalesTaxForOrder(request)).Returns(taxRes);

            // Action
            var response = salesTaxController.CalulateSalesTaxForOrder(request);

            // Assertion
            Assert.NotNull(response);
            Assert.True(response.IsSuccess);            
        }
    }
}
