using CarGlassDesafioTecnico.Application.ExternalServices;
using CarGlassDesafioTecnico.Application.Usecases.CreateCarGlassDesafioTecnico;
using CarGlassDesafioTecnico.Domain.Repositories.MongoDb;
using CarGlassDesafioTecnico.Domain.Repositories.Sql;
using CarGlassDesafioTecnico.Dto.CarGlassDesafioTecnico;
using ErrorOr;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CarGlassDesafioTecnico.Test.Unit.Application.Usecases;

[TestClass]
public class CreateCarGlassDesafioTecnicoUsecaseTests : UsecaseFixture
{
    [TestMethod]
    [DataRow("Test Name")]
    [DataRow("Other Name")]
    public async Task SHOULD_CREATE_BOILERPLATE(string name)
    {
        #region Arrange
        var messageBroker = new Mock<IMessageBroker>();
        var eventStreaming = new Mock<IEventStreaming>();

        var boilerplateDto = new CarGlassDesafioTecnicoCreateDto(name, CrossCutting.Enums.CarGlassDesafioTecnicoType.Azure);
        var boilerplateEntity = CarGlassDesafioTecnico.Domain.Entities.CarGlassDesafioTecnico.Create(boilerplateDto.Name, boilerplateDto.CarGlassDesafioTecnicoType);
        boilerplateEntity.Id = Guid.NewGuid().ToString();

        var boilerplateProjectionRepository = new Mock<ICarGlassDesafioTecnicoProjectionRepository>();
        boilerplateProjectionRepository.Setup(x => x.Insert(It.IsAny<CarGlassDesafioTecnico.Domain.Entities.CarGlassDesafioTecnico>(), It.IsAny<CancellationToken>()))
            .Callback<CarGlassDesafioTecnico.Domain.Entities.CarGlassDesafioTecnico, CancellationToken>((boilerplate, _) =>
            {
                boilerplate.Id = boilerplateEntity.Id;
            });

        var boilerplateRepository = new Mock<ICarGlassDesafioTecnicoRepository>();
        boilerplateRepository.Setup(x => x.Insert(It.IsAny<CarGlassDesafioTecnico.Domain.Entities.CarGlassDesafioTecnico>(), It.IsAny<CancellationToken>()))
            .Callback<CarGlassDesafioTecnico.Domain.Entities.CarGlassDesafioTecnico, CancellationToken>((boilerplate, _) =>
            {
                boilerplate.Id = boilerplateEntity.Id;
            });

        var createCarGlassDesafioTecnicoUsecase = new CreateCarGlassDesafioTecnicoUsecase(
            _mapper, messageBroker.Object, eventStreaming.Object,
            boilerplateProjectionRepository.Object, boilerplateRepository.Object
        );
        #endregion

        #region Act
        var boilerplateId = await createCarGlassDesafioTecnicoUsecase.Execute(boilerplateDto, default);
        #endregion

        #region Assert
        boilerplateId.Should().NotBeNull();
        boilerplateId.Should().BeOfType<ErrorOr<CarGlassDesafioTecnicoDto>>();
        boilerplateId.Value.Id.Should().Be(boilerplateEntity.Id);
        boilerplateEntity.Name.Should().Be(boilerplateDto.Name);
        boilerplateEntity.CarGlassDesafioTecnicoType.Should().Be(boilerplateDto.CarGlassDesafioTecnicoType);

        messageBroker.Verify(x => x.SendMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, object>>(), It.IsAny<CancellationToken>()), Times.Once);
        messageBroker.Verify(x => x.PublishMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, object>>(), It.IsAny<CancellationToken>()), Times.Once);
        eventStreaming.Verify(x => x.SendEvent(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once());
        boilerplateProjectionRepository.Verify(x => x.Insert(It.IsAny<CarGlassDesafioTecnico.Domain.Entities.CarGlassDesafioTecnico>(), It.IsAny<CancellationToken>()), Times.Once());
        boilerplateRepository.Verify(x => x.Insert(It.IsAny<CarGlassDesafioTecnico.Domain.Entities.CarGlassDesafioTecnico>(), It.IsAny<CancellationToken>()), Times.Once());
        #endregion
    }

    [TestMethod]
    [DataRow(null)]
    public async Task SHOULD_NOT_CREATE_BOILERPLATE_WITH_NULL_NAME(string name)
    {
        #region Arrange
        var messageBroker = new Mock<IMessageBroker>();
        var eventStreaming = new Mock<IEventStreaming>();

        var boilerplateDto = new CarGlassDesafioTecnicoCreateDto(name, CrossCutting.Enums.CarGlassDesafioTecnicoType.Azure);
        var boilerplateEntity = CarGlassDesafioTecnico.Domain.Entities.CarGlassDesafioTecnico.Create(boilerplateDto.Name, boilerplateDto.CarGlassDesafioTecnicoType);
        boilerplateEntity.Id = Guid.NewGuid().ToString();

        var boilerplateProjectionRepository = new Mock<ICarGlassDesafioTecnicoProjectionRepository>();
        boilerplateProjectionRepository.Setup(x => x.Insert(It.IsAny<CarGlassDesafioTecnico.Domain.Entities.CarGlassDesafioTecnico>(), It.IsAny<CancellationToken>()))
            .Callback<CarGlassDesafioTecnico.Domain.Entities.CarGlassDesafioTecnico, CancellationToken>((boilerplate, _) =>
            {
                boilerplate.Id = boilerplateEntity.Id;
            });

        var createCarGlassDesafioTecnicoUsecase = new CreateCarGlassDesafioTecnicoUsecase(
           _mapper, messageBroker.Object, eventStreaming.Object, boilerplateProjectionRepository.Object, null
        );
        #endregion

        #region Act
        var boilerplateId = await createCarGlassDesafioTecnicoUsecase.Execute(boilerplateDto, default);
        #endregion

        #region Assert
        boilerplateId.Should().NotBeNull();
        boilerplateId.Should().BeOfType<ErrorOr<CarGlassDesafioTecnicoDto>>();
        boilerplateId.IsError.Should().BeTrue();
        boilerplateId.Errors.Count.Should().Be(1);
        boilerplateId.FirstError.Should().Match<Error>(x => x.Description == "'Nome do CarGlassDesafioTecnico' must not be empty.");

        messageBroker.Verify(x => x.SendMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, object>>(), It.IsAny<CancellationToken>()), Times.Never);
        messageBroker.Verify(x => x.PublishMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, object>>(), It.IsAny<CancellationToken>()), Times.Never);
        eventStreaming.Verify(x => x.SendEvent(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
        #endregion
    }

    [TestMethod]
    [DataRow("a")]
    [DataRow("ab")]
    public async Task SHOULD_NOT_CREATE_BOILERPLATE_WITH_INVALID_NAME(string name)
    {
        #region Arrange
        var messageBroker = new Mock<IMessageBroker>();
        var eventStreaming = new Mock<IEventStreaming>();

        var boilerplateDto = new CarGlassDesafioTecnicoCreateDto(name, CrossCutting.Enums.CarGlassDesafioTecnicoType.Azure);
        var boilerplateEntity = CarGlassDesafioTecnico.Domain.Entities.CarGlassDesafioTecnico.Create(boilerplateDto.Name, boilerplateDto.CarGlassDesafioTecnicoType);
        boilerplateEntity.Id = Guid.NewGuid().ToString();

        var boilerplateProjectionRepository = new Mock<ICarGlassDesafioTecnicoProjectionRepository>();
        boilerplateProjectionRepository.Setup(x => x.Insert(It.IsAny<CarGlassDesafioTecnico.Domain.Entities.CarGlassDesafioTecnico>(), It.IsAny<CancellationToken>()))
            .Callback<CarGlassDesafioTecnico.Domain.Entities.CarGlassDesafioTecnico, CancellationToken>((boilerplate, _) =>
            {
                boilerplate.Id = boilerplateEntity.Id;
            });

        var createCarGlassDesafioTecnicoUsecase = new CreateCarGlassDesafioTecnicoUsecase(
           _mapper, messageBroker.Object, eventStreaming.Object, boilerplateProjectionRepository.Object, null
        );
        #endregion

        #region Act
        var boilerplateId = await createCarGlassDesafioTecnicoUsecase.Execute(boilerplateDto, default);
        #endregion

        #region Assert
        boilerplateId.Should().NotBeNull();
        boilerplateId.Should().BeOfType<ErrorOr<CarGlassDesafioTecnicoDto>>();
        boilerplateId.IsError.Should().BeTrue();
        boilerplateId.Errors.Count.Should().Be(1);
        boilerplateId.FirstError.Should().Match<Error>(x => x.Description.Contains("'Nome do CarGlassDesafioTecnico' must be between 3 and 100 characters"));

        messageBroker.Verify(x => x.SendMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, object>>(), It.IsAny<CancellationToken>()), Times.Never);
        messageBroker.Verify(x => x.PublishMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, object>>(), It.IsAny<CancellationToken>()), Times.Never);
        eventStreaming.Verify(x => x.SendEvent(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
        #endregion
    }
}
