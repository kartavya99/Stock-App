using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System;
using Xunit;


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

        #region CreateSellOrder
        //StocksService.CreateSellOrder():

        // When you supply SellOrderRequest as null, it should throw ArgumentNullException.

        [Fact]
        public void CreateSellOrder_NullSellOrder_ToBeArgumentException()
        {
            //Arrange
            SellOrderRequest? sellOrderRequest = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _stockService.CreateSellOrder(sellOrderRequest);
            });
        }

        // When you supply sellOrderQuantity as 0 (as per the specification, minimum is 1), 
        // it should throw ArgumentException.
        [Theory]
        [InlineData(0)]
        public void CreateSellOrder_QuantityIsLessThanMinimum_ToBeArgumentExeption(uint sellOrderQuantity)
        {
            //Arrange
            SellOrderRequest? sellorderRequest = new SellOrderRequest() { StockSymbol = "MFST", StockName = "Microsoft", Price = 1, Quantity = sellOrderQuantity };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _stockService.CreateSellOrder(sellorderRequest);
            });
        }

        // When you supply sellOrderQuantity as 100001 (as per the specification, maximum is 100000),
        // it should throw ArgumentException.
        [Theory]
        [InlineData(100001)]
        public void CreateSellOrder_QuantityIsGreaterThanMaximum_ToBeArgumentExeption(uint sellOrderQuantity)
        {
            //Arrange
            SellOrderRequest? sellorderRequest = new SellOrderRequest() { StockSymbol = "MFST", StockName = "Microsoft", Price = 1, Quantity = sellOrderQuantity };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _stockService.CreateSellOrder(sellorderRequest);
            });
        }

        // When you supply sellOrderPrice as 0 (as per the specification, minimum is 1),
        // it should throw ArgumentException.

        [Theory]
        [InlineData(0)]
        public void CreateSellOrder_PriceIsLessThanMinimum_ToBeArgumentExeption(uint sellOrderPrice)
        {
            //Arrange
            SellOrderRequest? sellorderRequest = new SellOrderRequest() { StockSymbol = "MFST", StockName = "Microsoft", Price = sellOrderPrice, Quantity = 1 };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _stockService.CreateSellOrder(sellorderRequest);
            });
        }

        //When you supply sellOrderPrice as 10001 (as per the specification, maximum is 10000),
        //it should throw ArgumentException.
        [Theory]
        [InlineData(10001)]
        public void CreateSellOrder_PriceIsGreaterThanMaximum_ToBeArgumentExeption(uint sellOrderPrice)
        {
            //Arrange
            SellOrderRequest? sellorderRequest = new SellOrderRequest() { StockSymbol = "MFST", StockName = "Microsoft", Price = sellOrderPrice, Quantity = 1 };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _stockService.CreateSellOrder(sellorderRequest);
            });
        }

        //When you supply stock symbol=null (as per the specification, stock symbol can't be null),
        //it should throw ArgumentException.
        [Fact]

        public void CreateSellOrder_StockSymbolIsNull_ToBeArgumentExeption()
        {
            //Arrange
            SellOrderRequest? sellorderRequest = new SellOrderRequest() { StockSymbol = null, Price = 1, Quantity = 1 };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _stockService.CreateSellOrder(sellorderRequest);
            });
        }

        // When you supply dateAndTimeOfOrder as "1999-12-31" (YYYY-MM-DD) - (as per the specification, it should be equal or newer date than 2000-01-01),
        // it should throw ArgumentException.

        [Fact]

        public void CreateSellOrder_DateOfOrderIsLessThanYear2000_ToBeArgumentExeption()
        {
            //Arrange
            SellOrderRequest? sellorderRequest = new SellOrderRequest() { StockSymbol = "MFST", StockName = "Microsoft", DateAndTimeOfOrder = Convert.ToDateTime("1999-12-31"), Price = 1, Quantity = 1 };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _stockService.CreateSellOrder(sellorderRequest);
            });
        }


        //If you supply all valid values, it should be successful and return an object of SellOrderResponse type with auto-generated SellOrderID (guid).
        [Fact]
        public void CreateSellOrder_ValidData_ToBeSuccessful()
        {
            //Arrange
            SellOrderRequest? sellorderRequest = new SellOrderRequest() { StockSymbol = "MFST", StockName = "Microsoft", DateAndTimeOfOrder = Convert.ToDateTime("2026-02-25"), Price = 1, Quantity = 1 };

            //Act
            SellOrderResponse sellOrderResponseFromCreate = _stockService.CreateSellOrder(sellorderRequest);

            //Assert
            Assert.NotEqual(Guid.Empty, sellOrderResponseFromCreate.SellOrderID);
        }
        #endregion

        #region GetBuyOrders

        // StocksService.GetAllBuyOrders():
        // When you invoke this method, by default, the returned list should be empty.

        [Fact]
        public void GetAllBuyOrders_DafualtList_ToBeEmpty()
        {
            //Act
            List<BuyOrderResponse> buyOrdersFromGet = _stockService.GetBuyOrders();

            //Assert
            Assert.Empty(buyOrdersFromGet);
        }


        //When you first add few buy orders using CreateBuyOrder() method; and then invoke GetAllBuyOrders() method; 
        //the returned list should contain all the same buy orders.

        [Fact]
        public void GetAllBuyOrders_WithFewBuyOrders_ToBeSuccessful()
        {
            //Arrange
            BuyOrderRequest buyOrder_request1 = new BuyOrderRequest() { StockName = "MSFT", StockSymbol = "Microsoft", Price = 1, Quantity = 1, DateAndTimeOfOrder = DateTime.Parse("2026-02-26 06:00") };

            BuyOrderRequest buyOrder_request2 = new BuyOrderRequest() { StockName = "MSFT", StockSymbol = "Microsoft", Price = 1, Quantity = 1, DateAndTimeOfOrder = DateTime.Parse("2026-02-26 06:00") };

            List<BuyOrderRequest> buyOrder_requests = new List<BuyOrderRequest>() { buyOrder_request1, buyOrder_request2 };

            List<BuyOrderResponse> buyOrder_response_list_from_add = new List<BuyOrderResponse>();

            foreach (BuyOrderRequest buyOrder_request in buyOrder_requests )
            {
                BuyOrderResponse buyOrder_response = _stockService.CreateBuyOrder(buyOrder_request);
                buyOrder_response_list_from_add.Add(buyOrder_response);
            }

            //Act
            List<BuyOrderResponse> buyOrders_list_from_get = _stockService.GetBuyOrders();

            //Assert
            foreach(BuyOrderResponse buyOrder_list_from_add in buyOrder_response_list_from_add)
            {
                Assert.Contains(buyOrder_list_from_add, buyOrders_list_from_get);
            }
        }

        #endregion

        #region GetSellOrders

        // StocksService.GetAllSellOrders():
        // When you invoke this method, by default, the returned list should be empty.

        [Fact]
        public void GetAllSellOrders_DafualtList_ToBeEmpty()
        {
            //Act
            List<SellOrderResponse> sellOrdersFromGet = _stockService.GetSellOrders();

            //Assert
            Assert.Empty(sellOrdersFromGet);
        }

        // When you first add few sell orders using CreateSellOrder() method; and then invoke GetAllSellOrders() method; the returned list should contain all the same sell orders. 

        [Fact]
        public void GetAllSellOrders_WithFewSellOrders_ToBeSuccessful()
        {
            //Arrange
            SellOrderRequest sellOrder_request1 = new SellOrderRequest() { StockName = "MSFT", StockSymbol = "Microsoft", Price = 1, Quantity = 1, DateAndTimeOfOrder = DateTime.Parse("2026-02-26 06:00") };

            SellOrderRequest sellOrder_request2 = new SellOrderRequest() { StockName = "MSFT", StockSymbol = "Microsoft", Price = 1, Quantity = 1, DateAndTimeOfOrder = DateTime.Parse("2026-02-26 06:00") };

            List<SellOrderRequest> sellOrder_requests = new List<SellOrderRequest>() { sellOrder_request1, sellOrder_request2 };

            List<SellOrderResponse> sellOrder_response_list_from_add = new List<SellOrderResponse>();

            foreach (SellOrderRequest sellOrder_request in sellOrder_requests)
            {
                SellOrderResponse sellOrder_response = _stockService.CreateSellOrder(sellOrder_request);
                sellOrder_response_list_from_add.Add(sellOrder_response);
            }

            //Act
            List<SellOrderResponse> sellOrders_list_from_get = _stockService.GetSellOrders();

            //Assert
            foreach (SellOrderResponse sellOrder_list_from_add in sellOrder_response_list_from_add)
            {
                Assert.Contains(sellOrder_list_from_add, sellOrders_list_from_get);
            }
        }

        #endregion

    }
}

