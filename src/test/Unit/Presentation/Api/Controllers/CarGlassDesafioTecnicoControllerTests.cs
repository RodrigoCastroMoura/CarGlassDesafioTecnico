using CarGlassDesafioTecnico.Api.Controllers.v1;
using CarGlassDesafioTecnico.Application.Usecases.CreateCarGlassDesafioTecnico;
using CarGlassDesafioTecnico.Application.Usecases.GetCarGlassDesafioTecnicoById;
using CarGlassDesafioTecnico.Application.Usecases.SearchCarGlassDesafioTecnico;
using CarGlassDesafioTecnico.Dto.CarGlassDesafioTecnico;
using Common.Core.Dto.Search;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CarGlassDesafioTecnico.Test.Unit.Presentation.Api.Controllers;

[TestClass]
public class CarGlassDesafioTecnicoControllerTests
{
    [TestMethod]
    public async Task SHOULD_CREATE_BOILERPLATE()
    {
        #region arrange
        var nameResponse = "Create Async Test";
        var boilerplateResponseDto = new CarGlassDesafioTecnicoDto(Guid.NewGuid().ToString(), nameResponse, CrossCutting.Enums.CarGlassDesafioTecnicoType.Azure);
        var createCarGlassDesafioTecnicoUsecaseMock = new Mock<ICreateCarGlassDesafioTecnicoUsecase>();
        createCarGlassDesafioTecnicoUsecaseMock
            .Setup(x => x.Execute(It.IsAny<CarGlassDesafioTecnicoCreateDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(boilerplateResponseDto);

        var boilerplateController = new CarGlassDesafioTecnicoController(createCarGlassDesafioTecnicoUsecaseMock.Object, default, default);
        #endregion

        #region act
        var result = await boilerplateController.Create(new CarGlassDesafioTecnicoCreateDto(nameResponse, CrossCutting.Enums.CarGlassDesafioTecnicoType.Azure), default);
        #endregion

        #region assert
        result.Should().NotBeNull();
        var createdResult = result.Should().BeOfType<ActionResult<CarGlassDesafioTecnicoDto>>().Subject;
        var boilerplateResult = (createdResult.Result as ObjectResult).Value.Should().BeAssignableTo<CarGlassDesafioTecnicoDto>().Subject;
        boilerplateResult.Id.Should().Be(boilerplateResponseDto.Id);
        #endregion
    }

    [TestMethod]
    public async Task SHOULD_LIST_BOILERPLATES()
    {
        #region arrange
        var nameOfCarGlassDesafioTecnico = "TEST SHOULD_GET_BOILERPLATES";

        var boilerplateResponseDto = new CarGlassDesafioTecnicoDto(Guid.NewGuid().ToString(), nameOfCarGlassDesafioTecnico, CrossCutting.Enums.CarGlassDesafioTecnicoType.Azure);
        var listOfCarGlassDesafioTecnicosDto = new List<CarGlassDesafioTecnicoDto> { boilerplateResponseDto };
        var boilerplateGetResponse = new PagedResultDto<CarGlassDesafioTecnicoDto>(listOfCarGlassDesafioTecnicosDto.Count, listOfCarGlassDesafioTecnicosDto);

        var listCarGlassDesafioTecnicoUsecaseMock = new Mock<ISearchCarGlassDesafioTecnicoUsecase>();
        listCarGlassDesafioTecnicoUsecaseMock
            .Setup(x => x.Execute(It.IsAny<CarGlassDesafioTecnicoSearchFilterDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(boilerplateGetResponse);

        var boilerplateController = new CarGlassDesafioTecnicoController(default, listCarGlassDesafioTecnicoUsecaseMock.Object, default);
        #endregion

        #region act
        var result = await boilerplateController.Search(new CarGlassDesafioTecnicoSearchFilterDto { Offset = 0, Limit = 10 }, default);
        #endregion

        #region assert
        result.Should().NotBeNull();
        var objectResult = result.Should().BeOfType<ActionResult<PagedResultDto<CarGlassDesafioTecnicoDto>>>().Subject;
        var boilerplateResult = (objectResult.Result as ObjectResult).Value.Should().BeAssignableTo<PagedResultDto<CarGlassDesafioTecnicoDto>>().Subject;
        boilerplateResult.Total.Should().Be(listOfCarGlassDesafioTecnicosDto.Count);
        boilerplateResult.Items.First().Name.Should().Be(boilerplateResponseDto.Name);
        #endregion
    }

    [TestMethod]
    public async Task SHOULD_GET_BOILERPLATE_BY_ID()
    {
        #region arrange
        var nameOfCarGlassDesafioTecnico = "TEST SHOULD_GET_BOILERPLATE_BY_ID";
        var boilerplateDtoResponse = new CarGlassDesafioTecnicoDto(Guid.NewGuid().ToString(), nameOfCarGlassDesafioTecnico, CrossCutting.Enums.CarGlassDesafioTecnicoType.Azure);

        var getCarGlassDesafioTecnicoByIdUsecaseMock = new Mock<IGetCarGlassDesafioTecnicoByIdUsecase>();
        getCarGlassDesafioTecnicoByIdUsecaseMock
            .Setup(x => x.Execute(It.IsAny<CarGlassDesafioTecnicoGetByIdFilterDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(boilerplateDtoResponse);

        var boilerplateController = new CarGlassDesafioTecnicoController(default, default, getCarGlassDesafioTecnicoByIdUsecaseMock.Object);
        #endregion

        #region act
        var result = await boilerplateController.GetById(Guid.NewGuid().ToString(), default);
        #endregion

        #region assert
        result.Should().NotBeNull();
        var okResult = result.Should().BeOfType<ActionResult<CarGlassDesafioTecnicoDto>>().Subject;
        var boilerplateResult = (okResult.Result as ObjectResult).Value.Should().BeAssignableTo<CarGlassDesafioTecnicoDto>().Subject;
        boilerplateResult.Name.Should().Be(nameOfCarGlassDesafioTecnico);
        #endregion

    }
}
