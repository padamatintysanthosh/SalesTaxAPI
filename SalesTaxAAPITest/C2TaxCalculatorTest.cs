using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using SalesTaxAPI.DAL;
using SalesTaxAPI.DAL.Contracts;
using SalesTaxAPI.DAL.Services;
using SalesTaxAPI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;

namespace SalesTaxAAPITest
{
    public class C2TaxCalculatorTest
    {               
        Mock<IHttpClientHelper> clientHelper;
        IConfiguration configuration;
        ITaxCalculator taxCalculator;

        public C2TaxCalculatorTest()
        {        
            clientHelper = new Mock<IHttpClientHelper>();            
           
            configuration = new ConfigurationBuilder()
         .AddJsonFile("appsettings.json", true, true)
         .Build();            

        }

        [Fact]
        public void InvalidClientTest()
        {
            // Action
            var ex = Assert.Throws<ArgumentNullException>(() => new TaxCalculator2(null));

            // Assertion
            Assert.Equal("httpclient is null. (Parameter 'client')", ex.Message);
        }

        [Fact]
        public void CalculateTaxesForOrderTest()
        {
            // Setup
            OrderRequest taxRequest = new OrderRequest();
            TaxOrderModel taxOrderModel = new TaxOrderModel();                       
            taxCalculator = new TaxCalculator2(clientHelper.Object);

            // Action
            var result = Assert.Throws<NotImplementedException>(()=> taxCalculator.CalculateSalesTaxForOrder(taxRequest));

            // Assertion
            Assert.NotNull(result);
            Assert.Equal("The method or operation is not implemented.", result.Message);
           
        }

        [Fact]
        public void GetTaxRatesForLocationTest()
        {
            // Setup
            LocationReqest request = new LocationReqest();
            TaxRateModel rateModel = new TaxRateModel();
           
            taxCalculator = new TaxCalculator2(clientHelper.Object);

            // Action
            var result = Assert.Throws<NotImplementedException>(()=>taxCalculator.GetSalesTaxForLocation(request));

            // Assertion
            Assert.NotNull(result);           
            Assert.Equal("The method or operation is not implemented.", result.Message);            
        }
    }
}
