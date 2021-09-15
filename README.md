This Project is developed to handle Multiple Tax Calculators to satisfy customer needs.

Available APIs are 

1) GetSalesTaxForLocation
Sample Input:

TaxCalculatorId: 1
LocationRequest.country: USA
LocationRequest.zip: 23294
LocationRequest.state: Virgnia
LocationRequest.city: Henrico
LocationRequest.street: Board st

2) CalculateSalesTaxForOrder

Sample Input:
{
  "taxCalculatorId": 1,
  "orderRequest": {
    "from_country": "US",
  "from_zip": "07001",
  "from_state": "NJ",
  "to_country": "US",
  "to_zip": "07446",
  "to_state": "NJ",
  "amount": 16.50,
  "shipping": 1.5,
  "line_items": [
    {
      "quantity": 1,
      "unit_price": 15.0,
      "product_tax_code": "31000"
    }
  ]
  }
}






