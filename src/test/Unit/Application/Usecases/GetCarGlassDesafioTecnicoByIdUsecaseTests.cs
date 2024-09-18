using CarGlassDesafioTecnico.Application.Usecases.GetCarGlassDesafioTecnicoById;
using CarGlassDesafioTecnico.Domain.Repositories.MongoDb;
using CarGlassDesafioTecnico.Dto.CarGlassDesafioTecnico;
using ErrorOr;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CarGlassDesafioTecnico.Test.Unit.Application.Usecases;

[TestClass]
public class GetCarGlassDesafioTecnicoByIdUsecaseTests : UsecaseFixture
{        
    [TestMethod]
    public async Task SHOULD_GET_BOILERPLATE()
    {
        #region Arrange
        var boilerplateDto = new CarGlassDesafioTecnicoCreateDto("Test Name", CrossCutting.Enums.CarGlassDesafioTecnicoType.Azure);
        var boilerplateEntity = CarGlassDesafioTecnico.Domain.Entities.CarGlassDesafioTecnico.Create(boilerplateDto.Name, boilerplateDto.CarGlassDesafioTecnicoType);
        boilerplateEntity.Id = Guid.NewGuid().ToString();

        var boilerplateRepositoryMongoDB = new Mock<ICarGlassDesafioTecnicoProjectionRepository>();
        boilerplateRepositoryMongoDB.Setup(x => x.GetById(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(boilerplateEntity);

        var getCarGlassDesafioTecnicoByIdUsecase = new GetCarGlassDesafioTecnicoByIdUsecase(_mapper, boilerplateRepositoryMongoDB.Object);
        #endregion

        #region Act
        var boilerplateDtoResult = await getCarGlassDesafioTecnicoByIdUsecase.Execute(CarGlassDesafioTecnicoGetByIdFilterDto.From(boilerplateEntity.Id), default);
        #endregion

        #region Assert
        boilerplateDtoResult.Should().NotBeNull();
        boilerplateDtoResult.Should().BeOfType<ErrorOr<CarGlassDesafioTecnicoDto>>();
        boilerplateDto.Name.Should().Be(boilerplateDtoResult.Value.Name);
        #endregion

    }

    [TestMethod]
    public async Task SHOULD_BOILERPLATE_NOT_FOUND()
    {
        #region Arrange
        var boilerplateRepositoryMongoDB = new Mock<ICarGlassDesafioTecnicoProjectionRepository>();
        boilerplateRepositoryMongoDB.Setup(x => x.GetById(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));

        var getCarGlassDesafioTecnicoByIdUsecase = new GetCarGlassDesafioTecnicoByIdUsecase(_mapper, boilerplateRepositoryMongoDB.Object);
        #endregion

        #region Act
        var boilerplate = await getCarGlassDesafioTecnicoByIdUsecase.Execute(CarGlassDesafioTecnicoGetByIdFilterDto.From("Id doesn´t exists"), default);
        #endregion

        #region Assert
        boilerplate.IsError.Should().BeTrue();
        boilerplate.FirstError.Should().Match<Error>(x => x.Description == "CarGlassDesafioTecnico não encontrado");
        #endregion
    }
}
