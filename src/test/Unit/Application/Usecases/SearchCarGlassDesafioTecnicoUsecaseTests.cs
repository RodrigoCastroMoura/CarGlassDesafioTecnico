using CarGlassDesafioTecnico.Application.Usecases.SearchCarGlassDesafioTecnico;
using CarGlassDesafioTecnico.Domain.Repositories.MongoDb;
using CarGlassDesafioTecnico.Dto.CarGlassDesafioTecnico;
using Common.Core.Dto.Search;
using ErrorOr;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CarGlassDesafioTecnico.Test.Unit.Application.Usecases;

[TestClass]
public class SearchCarGlassDesafioTecnicoUsecaseTests : UsecaseFixture
{
    [TestMethod]
    public async Task SHOULD_SEARCH_BOILERPLATES()
    {
        #region Arrange
        var boilerplateDto = new CarGlassDesafioTecnicoCreateDto("Test Name", CrossCutting.Enums.CarGlassDesafioTecnicoType.Azure);
        var boilerplateEntity = CarGlassDesafioTecnico.Domain.Entities.CarGlassDesafioTecnico.Create(boilerplateDto.Name, boilerplateDto.CarGlassDesafioTecnicoType);
        boilerplateEntity.Id = Guid.NewGuid().ToString();
        var boilerplateList = new List<CarGlassDesafioTecnico.Domain.Entities.CarGlassDesafioTecnico> { boilerplateEntity };

        var boilerplateRepositoryMongoDB = new Mock<ICarGlassDesafioTecnicoProjectionRepository>();
        boilerplateRepositoryMongoDB.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((boilerplateList.Count, boilerplateList));

        var listCarGlassDesafioTecnicoUsecase = new SearchCarGlassDesafioTecnicoUsecase(_mapper, boilerplateRepositoryMongoDB.Object);
        #endregion

        #region Act
        var boilerplatesResult = await listCarGlassDesafioTecnicoUsecase.Execute(new CarGlassDesafioTecnicoSearchFilterDto(), default);
        #endregion

        #region Assert
        boilerplatesResult.Should().NotBeNull();
        boilerplatesResult.Should().BeOfType<ErrorOr<PagedResultDto<CarGlassDesafioTecnicoDto>>>();
        boilerplateList.LongCount().Should().Be(boilerplatesResult.Value.Total);
        boilerplateList.Count.Should().Be(boilerplatesResult.Value.Count);
        boilerplateDto.Name.Should().Be(boilerplatesResult.Value.Items.First().Name);
        #endregion
    }
}
