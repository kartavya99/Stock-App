using AutoFixture;
using Entities;
using FluentAssertions;
using Moq;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System;
using System.Threading.Tasks;
using Xunit;


namespace Tests
{
    public class StockServiceTest
    {
        private readonly IStocksService _stockService;
        
        private readonly Mock<IStockRepository> _stocksRepositoryMock;
        private readonly IStockRepository _stocksRepository;

        private readonly IFixture _fixture;
        public StockServiceTest()
        {
            _fixture = new Fixture();

            _stocksRepositoryMock = new Mock<IStockRepository>();
            _stocksRepository = _stocksRepositoryMock.Object;

            _stockService = new StockService(_stocksRepository);
        }
        #region CreateBuyOrder

        // StocksService.CreateBuyOrder():

        // When you supply BuyOrderRequest as null, it should throw ArgumentNullException.

        [Fact]
        public async Task CreateBuyOrder_NullBuyOrder_TobeArgumentNullException()
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = null;

            //Mock:
            BuyOrder buyOrderFixture = _fixture.Build<BuyOrder>().Create();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrderFixture);

            //Act
            Func<Task> action = async () =>
            {
             await _stockService.CreateBuyOrder(buyOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
            
        }

        //  When you supply buyOrderQuantity as 0 (as per the specification, minimum is 1)
        //  it should throw ArgumentException.

        [Theory]
        [InlineData(0)]
        public async Task CreateBuyOrder_QuantityIsLessThanMinimum_ToBeArgumentException(uint buyOrderQuantity)
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.Quantity, buyOrderQuantity)
                .Create();

            //Mock
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            //Act
            Func<Task> action = async () =>
            {
                await _stockService.CreateBuyOrder(buyOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();

        }

        //When you supply buyOrderQuantity as 100001 (as per the specification, maximum is 100000),
        //it should throw ArgumentException.

        [Theory]
        [InlineData(100001)]
        public async Task CreateBuyorder_QuantityIsGreaterThanMaximum_ToBeArgumentException(uint buyOrderQuantity)
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.Quantity, buyOrderQuantity)
                .Create();

            //Mock
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            //Act
            Func<Task> action = async () =>
            {
                await _stockService.CreateBuyOrder(buyOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();


        }

        //When you supply buyOrderPrice as 0 (as per the specification, minimum is 1),
        //it should throw ArgumentException.

        [Theory]
        [InlineData(0)]
        public async Task CreateBuyOrder_PriceIsLessThanMinimum_ToBeArgumentException(uint buyOrderPrice)
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.Price, buyOrderPrice)
                .Create();

            //Mock
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            //Act
            Func<Task> action = async () =>
            {
                await _stockService.CreateBuyOrder(buyOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();

        }

        // When you supply buyOrderPrice as 10001 (as per the specification, maximum is 10000),
        // it should throw ArgumentException.
        [Theory]
        [InlineData(10001)]
        public async Task CreateBuyOrder_PriceIsGreaterThanMinimum_ToBeArgumentException(uint buyOrderPrice)
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.Price, buyOrderPrice)
                .Create();

            //Mock
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            //Act
            Func<Task> action = async () =>
            {
                await _stockService.CreateBuyOrder(buyOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();

        }

        // When you supply stock symbol=null (as per the specification, stock symbol can't be null),
        // it should throw ArgumentException.
        [Fact]
        public async Task CreateBuyOrder_StockSymbolIsNull_ToBeArgumentException()
        {

            //Arrange
            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.StockSymbol, null as string)
                .Create();

            //Mock
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            //Act
            Func<Task> action = async () =>
            {
                await _stockService.CreateBuyOrder(buyOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();

        }

        // When you supply dateAndTimeOfOrder as "1999-12-31" (YYYY-MM-DD) - (as per the specification, it should be equal or newer date than 2000-01-01),
        // it should throw ArgumentException.
        [Fact]
        public async Task CreateBuyOrder_DateOfOrderIsLessThanYear2000_ToBeArgumentException()
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.DateAndTimeOfOrder, Convert.ToDateTime("1999-12-31"))
                .Create();

            //Mock
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            //Act
            Func<Task> action = async () =>
            {
                await _stockService.CreateBuyOrder(buyOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();

        }

        // If you supply all valid values, it should be successful and return an object of BuyOrderResponse type with auto-generated BuyOrderID(guid).
        [Fact]
        public async Task CreateBuyOrder_ValidData_ToBeSuccessful()
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()                
                .Create();

            //Mock
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            //Act
            BuyOrderResponse buyOrderResponseFromCreate = await _stockService.CreateBuyOrder(buyOrderRequest);

            //Assert
            buyOrder.BuyOrderID = buyOrderResponseFromCreate.BuyOrderID;
            BuyOrderResponse buyOrderResponse_expected = buyOrder.ToBuyOrderResponse();
            buyOrderResponseFromCreate.BuyOrderID.Should().NotBe(Guid.Empty);
            buyOrderResponseFromCreate.Should().Be(buyOrderResponse_expected);

        }

        #endregion

        #region CreateSellOrder
        //StocksService.CreateSellOrder():

        // When you supply SellOrderRequest as null, it should throw ArgumentNullException.

        [Fact]
        public async Task CreateSellOrder_NullSellOrder_ToBeArgumentException()
        {
            //Arrange
            SellOrderRequest? sellOrderRequest = null;

            //Mock:
            SellOrder sellOrderFixture = _fixture.Build<SellOrder>().Create();
            _stocksRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrderFixture);

            //Act
            Func<Task> action = async () =>
            {
                await _stockService.CreateSellOrder(sellOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        // When you supply sellOrderQuantity as 0 (as per the specification, minimum is 1), 
        // it should throw ArgumentException.
        [Theory]
        [InlineData(0)]
        public async Task CreateSellOrder_QuantityIsLessThanMinimum_ToBeArgumentExeption(uint sellOrderQuantity)
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.Quantity, sellOrderQuantity)
                .Create();

            //Mock:
            SellOrder sellOrderFixture = _fixture.Build<SellOrder>().Create();
            _stocksRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrderFixture);

            //Act
            Func<Task> action = async () =>
            {
                await _stockService.CreateSellOrder(sellOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // When you supply sellOrderQuantity as 100001 (as per the specification, maximum is 100000),
        // it should throw ArgumentException.
        [Theory]
        [InlineData(100001)]
        public async Task CreateSellOrder_QuantityIsGreaterThanMaximum_ToBeArgumentExeption(uint sellOrderQuantity)
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.Quantity, sellOrderQuantity)
                .Create();

            //Mock:
            SellOrder sellOrderFixture = _fixture.Build<SellOrder>().Create();
            _stocksRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrderFixture);

            //Act
            Func<Task> action = async () =>
            {
                await _stockService.CreateSellOrder(sellOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // When you supply sellOrderPrice as 0 (as per the specification, minimum is 1),
        // it should throw ArgumentException.

        [Theory]
        [InlineData(0)]
        public async Task CreateSellOrder_PriceIsLessThanMinimum_ToBeArgumentExeption(uint sellOrderPrice)
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.Price, sellOrderPrice)
                .Create();

            //Mock:
            SellOrder sellOrderFixture = _fixture.Build<SellOrder>().Create();
            _stocksRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrderFixture);

            //Act
            Func<Task> action = async () =>
            {
                await _stockService.CreateSellOrder(sellOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        //When you supply sellOrderPrice as 10001 (as per the specification, maximum is 10000),
        //it should throw ArgumentException.
        [Theory]
        [InlineData(10001)]
        public async Task CreateSellOrder_PriceIsGreaterThanMaximum_ToBeArgumentExeption(uint sellOrderPrice)
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.Price, sellOrderPrice)
                .Create();

            //Mock:
            SellOrder sellOrderFixture = _fixture.Build<SellOrder>().Create();
            _stocksRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrderFixture);

            //Act
            Func<Task> action = async () =>
            {
                await _stockService.CreateSellOrder(sellOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        //When you supply stock symbol=null (as per the specification, stock symbol can't be null),
        //it should throw ArgumentException.
        [Fact]

        public async Task CreateSellOrder_StockSymbolIsNull_ToBeArgumentExeption()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.StockSymbol, null as string)
                .Create();

            //Mock:
            SellOrder sellOrderFixture = _fixture.Build<SellOrder>().Create();
            _stocksRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrderFixture);

            //Act
            Func<Task> action = async () =>
            {
                await _stockService.CreateSellOrder(sellOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // When you supply dateAndTimeOfOrder as "1999-12-31" (YYYY-MM-DD) - (as per the specification, it should be equal or newer date than 2000-01-01),
        // it should throw ArgumentException.

        [Fact]

        public async Task CreateSellOrder_DateOfOrderIsLessThanYear2000_ToBeArgumentExeption()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.DateAndTimeOfOrder, Convert.ToDateTime("1999-12-31"))
                .Create();

            //Mock:
            SellOrder sellOrderFixture = _fixture.Build<SellOrder>().Create();
            _stocksRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrderFixture);

            //Act
            Func<Task> action = async () =>
            {
                await _stockService.CreateSellOrder(sellOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }


        //If you supply all valid values, it should be successful and return an object of SellOrderResponse type with auto-generated SellOrderID (guid).
        [Fact]
        public async Task CreateSellOrder_ValidData_ToBeSuccessful()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()              
                .Create();

            //Mock:
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrder);

            //Act       
            SellOrderResponse sellOrderRespnseFromCreate = await _stockService.CreateSellOrder(sellOrderRequest);

            //Asset
            sellOrder.SellOrderID = sellOrderRespnseFromCreate.SellOrderID;
            SellOrderResponse sellOrderResponse_expected = sellOrder.ToSellOrderResponse();
            sellOrderRespnseFromCreate.SellOrderID.Should().NotBe(Guid.Empty);
            sellOrderRespnseFromCreate.Should().Be(sellOrderResponse_expected);
            

        }
        #endregion

        #region GetBuyOrders

        // StocksService.GetAllBuyOrders():
        // When you invoke this method, by default, the returned list should be empty.

        [Fact]
        public async Task GetAllBuyOrders_DafualtList_ToBeEmpty()
        {
            //Arrange
            List<BuyOrder> buyOrders = new List<BuyOrder>();

            //Mock
            _stocksRepositoryMock.Setup(temp => temp.GetBuyOrders()).ReturnsAsync(buyOrders);

            //Act
            List<BuyOrderResponse> buyOrdersFromGet = await _stockService.GetBuyOrders();

            //Assert
            Assert.Empty(buyOrdersFromGet);
        }


        //When you first add few buy orders using CreateBuyOrder() method; and then invoke GetAllBuyOrders() method; 
        //the returned list should contain all the same buy orders.

        [Fact]
        public async Task GetAllBuyOrders_WithFewBuyOrders_ToBeSuccessful()
        {
            //Arrange
            List<BuyOrder> buyOrder_request = new List<BuyOrder>()
            {
                _fixture.Build<BuyOrder>().Create(),
                _fixture.Build<BuyOrder>().Create()
            };

            List<BuyOrderResponse> buyOrders_list_expected = buyOrder_request.Select(temp => temp.ToBuyOrderResponse()).ToList();
            List<BuyOrderResponse> buyOrder_response_list_from_add = new List<BuyOrderResponse>();

            //Mock
            _stocksRepositoryMock.Setup(temp => temp.GetBuyOrders()).ReturnsAsync(buyOrder_request);

            //Act
            List<BuyOrderResponse> buyOrders_list_from_get = await _stockService.GetBuyOrders();

            //Assert
            buyOrders_list_from_get.Should().BeEquivalentTo(buyOrders_list_expected);

        }

        #endregion

        #region GetSellOrders

        // StocksService.GetAllSellOrders():
        // When you invoke this method, by default, the returned list should be empty.

        [Fact]
        public async Task GetAllSellOrders_DafualtList_ToBeEmpty()
        {
            //Arrange
            List<SellOrder> sellOrders = new List<SellOrder>();

            //Mock
            _stocksRepositoryMock.Setup(temp => temp.GetSellOrders()).ReturnsAsync(sellOrders);

            //Act
            List<SellOrderResponse> sellOrdersFromGet = await _stockService.GetSellOrders();

            //Assert
            Assert.Empty(sellOrdersFromGet);
        }

        // When you first add few sell orders using CreateSellOrder() method; and then invoke GetAllSellOrders() method; the returned list should contain all the same sell orders. 

        [Fact]
        public async Task GetAllSellOrders_WithFewSellOrders_ToBeSuccessful()
        {
            //Arrange
            List<SellOrder> sellOrder_request = new List<SellOrder>()
            {
                _fixture.Build<SellOrder>().Create(),
                _fixture.Build<SellOrder>().Create()
            };

            List<SellOrderResponse> sellOrders_list_expected = sellOrder_request.Select(temp => temp.ToSellOrderResponse()).ToList();
            List<SellOrderResponse> sellOrder_response_list_from_add = new List<SellOrderResponse>();

            //Mock
            _stocksRepositoryMock.Setup(temp => temp.GetSellOrders()).ReturnsAsync(sellOrder_request);

            //Act
            List<SellOrderResponse> sellOrders_list_from_get = await _stockService.GetSellOrders();

            //Assert
            sellOrders_list_from_get.Should().BeEquivalentTo(sellOrders_list_expected);
        }

        #endregion

    }
}

