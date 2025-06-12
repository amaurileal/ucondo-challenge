using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ucondo_challenge.application.ChartOfAccounts.Commands.Create;
using ucondo_challenge.business.Entities;
using ucondo_challenge.business.Exceptions;
using ucondo_challenge.business.Repositories;

namespace ucondo_challenge.tests
{
    public class ChartOfAccountsCreateCommandHandlerTests
    {
        private readonly Mock<ILogger<ChartOfAccountsCreateCommandHandler>> _loggerMock = new();
        private readonly Mock<IChartOfAccountsRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly ChartOfAccountsCreateCommandHandler _handler;

        public ChartOfAccountsCreateCommandHandlerTests()
        {
            _handler = new ChartOfAccountsCreateCommandHandler(
                _loggerMock.Object,
                _repositoryMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task Handle_Should_Create_Entity_When_Valid_Request()
        {
            // Arrange
            var command = new ChartOfAccountsCreateCommand
            {
                TenantId = Guid.NewGuid(),
                Code = "1.01",
                Name = "Test Account",
                Type = 1
            };

            var entity = new ChartOfAccountsEntity();
            _mapperMock.Setup(m => m.Map<ChartOfAccountsEntity>(command)).Returns(entity);
            _repositoryMock.Setup(r => r.GetByCodeAsync(command.TenantId, command.Code, It.IsAny<CancellationToken>())).ReturnsAsync((ChartOfAccountsEntity)null);
            _repositoryMock.Setup(r => r.CreateAsync(entity, It.IsAny<CancellationToken>())).ReturnsAsync(Guid.NewGuid());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsType<Guid>(result);
            _repositoryMock.Verify(r => r.CreateAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Throw_When_Code_Already_Exists()
        {
            // Arrange
            var command = new ChartOfAccountsCreateCommand
            {
                TenantId = Guid.NewGuid(),
                Code = "1.01",
                Name = "Test Account",
                Type = 1
            };

            var entity = new ChartOfAccountsEntity();
            _repositoryMock.Setup(r => r.GetByCodeAsync(command.TenantId, command.Code, It.IsAny<CancellationToken>())).ReturnsAsync(entity);

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_Should_Throw_When_Parent_Not_Found()
        {
            // Arrange
            var command = new ChartOfAccountsCreateCommand
            {
                TenantId = Guid.NewGuid(),
                Code = "1.01",
                Name = "Test Account",
                Type = 1,
                ParentId = Guid.NewGuid()
            };

            _repositoryMock.Setup(r => r.GetByCodeAsync(command.TenantId, command.Code, It.IsAny<CancellationToken>())).ReturnsAsync((ChartOfAccountsEntity)null);
            _repositoryMock.Setup(r => r.GetByIdAsync(command.TenantId, command.ParentId.Value, It.IsAny<CancellationToken>())).ReturnsAsync((ChartOfAccountsEntity)null);

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
