using Boticario.EuRevendedor.Interfaces.Enumerators;
using Boticario.EuRevendedor.Models;
using Boticario.EuRevendedor.Service.Handlers.OrderHandlers;
using Boticario.EuRevendedor.Service.Handlers.OrderHandlers.Commands;
using Boticario.EuRevendedor.Tests.Fakes;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Boticario.EuRevendedor.Tests.Handlers.Orders
{
    public class UpdateOrderHandlerTests
    {
        [Fact]
        public async Task Should_Update_When_Order_Is_Valid()
        {
            var repository = new FakeOrderRepository();
            var order = new Order("123", 1000, new DateTimeOffset(2020, 1, 21, 20, 0, 0, TimeSpan.Zero), "21330371011");
            
            repository.InsertFake(order);

            var handler = new UpdateOrderHandler(repository);
            var command = new UpdateOrderCommand(order.Id, "123", 2000, new DateTimeOffset(2020, 1, 22, 20, 0, 0, TimeSpan.Zero), "21330371011");
            var response = await handler.Handle(command, System.Threading.CancellationToken.None);

            Assert.False(response.Invalid);
        }

        [Fact]
        public async Task Should_Not_Update_When_There_Is_No_Order()
        {
            var repository = new FakeOrderRepository();
            var order = new Order("123", 1000, new DateTimeOffset(2020, 1, 21, 20, 0, 0, TimeSpan.Zero), "21330371011");
            repository.InsertFake(order);


            var handler = new UpdateOrderHandler(repository);
            var command = new UpdateOrderCommand(Guid.NewGuid().ToString("N"), "123", 2000, new DateTimeOffset(2020, 1, 22, 20, 0, 0, TimeSpan.Zero), "21330371011");
            
            var response = await handler.Handle(command, System.Threading.CancellationToken.None);

            Assert.True(response.Invalid);
            Assert.Equal("Compra não encontrada!", response.Notifications.First().Message);
        }

        [Fact]
        public async Task Should_Not_Update_When_Status_Is_Approved()
        {
            var repository = new FakeOrderRepository();
            var order = new Order("123", 1000, new DateTimeOffset(2020, 1, 21, 20, 0, 0, TimeSpan.Zero), "15350946056");
            repository.InsertFake(order);


            var handler = new UpdateOrderHandler(repository);
            var command = new UpdateOrderCommand(order.Id, "123", 2000, new DateTimeOffset(2020, 1, 22, 20, 0, 0, TimeSpan.Zero), "15350946056");
            
            var response = await handler.Handle(command, System.Threading.CancellationToken.None);

            Assert.True(response.Invalid);
            Assert.Equal("Compras já aprovadas, não podem ser editadas!", response.Notifications.First().Message);
        }

        [Fact]
        public async Task Should_Not_Update_When_There_Is_Already_Code()
        {
            var repository = new FakeOrderRepository();
            var order = new Order("123", 1000, new DateTimeOffset(2020, 1, 21, 20, 0, 0, TimeSpan.Zero), "21330371011");
            repository.InsertFake(order);

            var order2 = new Order("1234", 1000, new DateTimeOffset(2020, 1, 21, 20, 0, 0, TimeSpan.Zero), "21330371011");
            repository.InsertFake(order2);


            var handler = new UpdateOrderHandler(repository);
            var command = new UpdateOrderCommand(order.Id, "1234", 2000, new DateTimeOffset(2020, 1, 22, 20, 0, 0, TimeSpan.Zero), "21330371011");
            var response = await handler.Handle(command, System.Threading.CancellationToken.None);

            Assert.True(response.Invalid);
            Assert.Equal("Já existe uma Compra com esse código!", response.Notifications.First().Message);
        }
    }

    public class InsertOrderHandlerTests
    {
        [Fact]
        public async Task Should_Insert_When_Order_Is_Valid()
        {
            var handler = new InsertOrderHandler(new FakeOrderRepository());
            var command = new InsertOrderCommand("123", 1000, new DateTimeOffset(2020, 1, 21, 20, 0, 0, TimeSpan.Zero), "09230359661");
            var response = await handler.Handle(command, System.Threading.CancellationToken.None);

            Assert.False(response.Invalid);
        }

        [Fact]
        public async Task Should_Return_Error_When_Cpf_Is_Invalid()
        {
            var handler = new InsertOrderHandler(new FakeOrderRepository());
            var command = new InsertOrderCommand("123", 1000, new DateTimeOffset(2020, 1, 21, 20, 0, 0, TimeSpan.Zero), "09230356661");
            var response = await handler.Handle(command, System.Threading.CancellationToken.None);

            Assert.True(response.Invalid);
            Assert.Equal("Cpf informado é inválido!", response.Notifications.First().Message);
        }

        [Fact]
        public async Task Should_Insert_When_Order_Approved_By_Cpf_15350946056()
        {
            var repository = new FakeOrderRepository();
            var handler = new InsertOrderHandler(repository);
            var command = new InsertOrderCommand("123", 1000, new DateTimeOffset(2020, 1, 21, 20, 0, 0, TimeSpan.Zero), "15350946056");
            var response = await handler.Handle(command, System.Threading.CancellationToken.None);

            var order = await repository.GetByCode("123");

            Assert.False(response.Invalid);
            Assert.Equal(EnumOrderStatus.Approved, order.OrderStatus);

        }

        [Theory]
        [InlineData("60317114000")]
        [InlineData("73041621045")]
        [InlineData("58811621020")]
        public async Task Should_Insert_Order_InCheking_When_Cpf_Is_Different_To_15350946056(string cpf)
        {
            var repository = new FakeOrderRepository();
            var handler = new InsertOrderHandler(repository);
            var command = new InsertOrderCommand("123", 1000, new DateTimeOffset(2020, 1, 21, 20, 0, 0, TimeSpan.Zero), cpf);
            var response = await handler.Handle(command, System.Threading.CancellationToken.None);

            var order = await repository.GetByCode("123");

            Assert.False(response.Invalid);
            Assert.Equal(EnumOrderStatus.OnChecking, order.OrderStatus);
        }

        [Theory]
        [InlineData(500)]
        [InlineData(999)]
        [InlineData(10.50)]
        public async Task Should_Insert_When_Order_With_10_Percent_When_Informed_Values_​​Less_Than_1000(decimal value)
        {
            var repository = new FakeOrderRepository();
            var handler = new InsertOrderHandler(repository);
            var command = new InsertOrderCommand("123", value, new DateTimeOffset(2020, 1, 21, 20, 0, 0, TimeSpan.Zero), "73041621045");
            var response = await handler.Handle(command, System.Threading.CancellationToken.None);

            var order = await repository.GetByCode("123");

            byte porcentagemEsperada = 10;
            Assert.False(response.Invalid);
            Assert.Equal(porcentagemEsperada, order.AppliedCashback);
        }

        [Theory]
        [InlineData(1000)]
        [InlineData(1200.50)]
        [InlineData(1500)]
        public async Task Should_Insert_When_Order_With_15_Percent_When_Informed_Values_between_1000_and_1500(decimal value)
        {
            var repository = new FakeOrderRepository();
            var handler = new InsertOrderHandler(repository);
            var command = new InsertOrderCommand("123", value, new DateTimeOffset(2020, 1, 21, 20, 0, 0, TimeSpan.Zero), "73041621045");
            var response = await handler.Handle(command, System.Threading.CancellationToken.None);

            var order = await repository.GetByCode("123");

            byte porcentagemEsperada = 15;
            Assert.False(response.Invalid);
            Assert.Equal(porcentagemEsperada, order.AppliedCashback);
        }

        [Theory]
        [InlineData(1500.01)]
        [InlineData(2000)]
        [InlineData(10000)]
        public async Task Should_Insert_When_Order_With_20_Percent_When_Informed_Values_More_Than_1500(decimal value)
        {
            var repository = new FakeOrderRepository();
            var handler = new InsertOrderHandler(repository);
            var command = new InsertOrderCommand("123", value, new DateTimeOffset(2020, 1, 21, 20, 0, 0, TimeSpan.Zero), "73041621045");
            var response = await handler.Handle(command, System.Threading.CancellationToken.None);

            var order = await repository.GetByCode("123");

            byte porcentagemEsperada = 20;
            Assert.False(response.Invalid);
            Assert.Equal(porcentagemEsperada, order.AppliedCashback);
        }

        [Fact]
        public async Task Should_Insert_When_Order_With_Cashback_Is_50()
        {
            var repository = new FakeOrderRepository();
            var handler = new InsertOrderHandler(repository);
            var command = new InsertOrderCommand("123", 500, new DateTimeOffset(2020, 1, 21, 20, 0, 0, TimeSpan.Zero), "73041621045");
            var response = await handler.Handle(command, System.Threading.CancellationToken.None);

            var order = await repository.GetByCode("123");

            byte porcentagemEsperada = 10;
            decimal valorCashbackEsperada = 50;

            Assert.False(response.Invalid);
            Assert.Equal(porcentagemEsperada, order.AppliedCashback);
            Assert.Equal(valorCashbackEsperada, order.ValueCashback);
        }

        [Fact]
        public async Task Should_Not_Insert_When_Code_Already_Exists()
        {
            var repository = new FakeOrderRepository();
            var order = new Order("123", 1000, new DateTimeOffset(2020, 1, 21, 20, 0, 0, TimeSpan.Zero), "21330371011");
            repository.InsertFake(order);

            var order2 = new Order("1234", 1000, new DateTimeOffset(2020, 1, 21, 20, 0, 0, TimeSpan.Zero), "21330371011");
            repository.InsertFake(order2);


            var handler = new InsertOrderHandler(repository);
            var command = new InsertOrderCommand("1234", 2000, new DateTimeOffset(2020, 1, 22, 20, 0, 0, TimeSpan.Zero), "21330371011");
            var response = await handler.Handle(command, System.Threading.CancellationToken.None);

            Assert.True(response.Invalid);
            Assert.Equal("Já existe uma Compra com esse código!", response.Notifications.First().Message);
        }
    }

    public class DeleteOrderHandlerTests
    {
        [Fact]
        public async Task Should_Delete_When_Order_Is_Valid()
        {
            var repository = new FakeOrderRepository();
            var order = new Order("123", 1000, new DateTimeOffset(2020, 1, 21, 20, 0, 0, TimeSpan.Zero), "09230359661");
            repository.InsertFake(order);

            var handler = new DeleteOrderHandler(repository);
            var command = new DeleteOrderCommand(order.Id.ToString());
            var response = await handler.Handle(command, System.Threading.CancellationToken.None);

            Assert.False(response.Invalid);
        }

        [Fact]
        public async Task Should_Not_Delete_When_There_Is_No()
        {
            var repository = new FakeOrderRepository();
            var order = new Order("123", 1000, new DateTimeOffset(2020, 1, 21, 20, 0, 0, TimeSpan.Zero), "15350946056");
            repository.InsertFake(order);


            var handler = new DeleteOrderHandler(repository);
            var command = new DeleteOrderCommand(Guid.NewGuid().ToString("N"));
            var response = await handler.Handle(command, System.Threading.CancellationToken.None);

            Assert.True(response.Invalid);
            Assert.Equal("Compra não encontrada!", response.Notifications.First().Message);
        }

        [Fact]
        public async Task Should_Not_Delete_When_Order_Is_Approved()
        {
            var repository = new FakeOrderRepository();
            var order = new Order("123", 1000, new DateTimeOffset(2020, 1, 21, 20, 0, 0, TimeSpan.Zero), "15350946056");
            repository.InsertFake(order);


            var handler = new DeleteOrderHandler(repository);
            var command = new DeleteOrderCommand(order.Id.ToString());
            var response = await handler.Handle(command, System.Threading.CancellationToken.None);

            Assert.True(response.Invalid);
            Assert.Equal("Compras já aprovadas, não podem ser excluídas!", response.Notifications.First().Message);
        }
    }
}
