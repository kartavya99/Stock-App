using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System;
using System.Runtime.ConstrainedExecution;
using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tests
{
    public class StockServiceTest
    {
        private readonly IStocksService _stockService;

        public StockServiceTest()
        {
            _stockService = new StockService();
        }

        #region CreateBuyOrder

        // StocksService.CreateBuyOrder():

        // When you supply BuyOrderRequest as null, it should throw ArgumentNullException.

        [Fact]
        public void CreateBuyOrder_NullBuyOrder_TobeArgumentNullException()
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _stockService.CreateBuyOrder(buyOrderRequest);
            });
        }

        //  When you supply buyOrderQuantity as 0 (as per the specification, minimum is 1)
        //  it should throw ArgumentException.

        [Theory]
        [InlineData(0)]
        public void CreateBuyOrder_QuantityIsLessThanMinimum_ToBeArgumentException(uint buyOrderQuantity)
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = buyOrderQuantity };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _stockService.CreateBuyOrder(buyOrderRequest);
            });

        }

        //When you supply buyOrderQuantity as 100001 (as per the specification, maximum is 100000),
        //it should throw ArgumentException.

        [Theory]
        [InlineData(10001)]
        public void CreateBuyorder_QuantityIsGreaterThanMaximum_ToBeArgumentException(uint buyOrderQuantity)
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = buyOrderQuantity };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _stockService.CreateBuyOrder(buyOrderRequest);
            });

        }

        //When you supply buyOrderPrice as 0 (as per the specification, minimum is 1),
        //it should throw ArgumentException.

        [Theory]
        [InlineData(0)]
        public void CreateBuyOrder_PriceIsLessThanMinimum_ToBeArgumentException(uint buyOrderPrice)
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = buyOrderPrice, Quantity = 1 };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _stockService.CreateBuyOrder(buyOrderRequest);
            });

        }

        // When you supply buyOrderPrice as 10001 (as per the specification, maximum is 10000),
        // it should throw ArgumentException.
        [Theory]
        [InlineData(0)]
        public void CreateBuyOrder_PriceIsGreaterThanMinimum_ToBeArgumentException(uint buyOrderQuantity)
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = buyOrderQuantity };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _stockService.CreateBuyOrder(buyOrderRequest);
            });

        }

        // When you supply stock symbol=null (as per the specification, stock symbol can't be null),
        // it should throw ArgumentException.
        [Fact]        
        public void CreateBuyOrder_StockSymbolIsNull_ToBeArgumentException()
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockName = null, Price = 1, Quantity = 1 };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _stockService.CreateBuyOrder(buyOrderRequest);
            });

        }

        // When you supply dateAndTimeOfOrder as "1999-12-31" (YYYY-MM-DD) - (as per the specification, it should be equal or newer date than 2000-01-01),
        // it should throw ArgumentException.
        [Fact]
        public void CreateBuyOrder_DateOfOrderIsLessThanYear2000_ToBeArgumentException()
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", DateAndTimeOfOrder = Convert.ToDateTime("1999-12-31"), Price = 1, Quantity = 1 };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _stockService.CreateBuyOrder(buyOrderRequest);
            });

        }

        // If you supply all valid values, it should be successful and return an object of BuyOrderResponse type with auto-generated BuyOrderID(guid).
        [Fact]
        public void CreateBuyOrder_ValidData_ToBeSuccessful()
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", DateAndTimeOfOrder = Convert.ToDateTime("2026-02-25"), Price = 1, Quantity = 1 };

            //Act
            BuyOrderResponse buyOrderResponseFromCreate = _stockService.CreateBuyOrder(buyOrderRequest);

            //Assert
            Assert.NotEqual(Guid.Empty, buyOrderResponseFromCreate.BuyOrderID);

        }

        #endregion
    }
}
