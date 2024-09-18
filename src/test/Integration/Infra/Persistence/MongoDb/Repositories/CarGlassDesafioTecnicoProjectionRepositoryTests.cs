using CarGlassDesafioTecnico.Dto.CarGlassDesafioTecnico;
using CarGlassDesafioTecnico.Infra.Persistence.MongoDb.Repositories;
using CarGlassDesafioTecnico.Test.Integration.Shared;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarGlassDesafioTecnico.Test.Integration.Infra.Persistence.MongoDb.Repositories;

[TestClass]
public class CarGlassDesafioTecnicoProjectionRepositoryTests : InfraBaseTests
{
    [TestMethod]
    public async Task SHOULD_UPDATE_BOILERPLATE()
    {
        #region Arrange
        var newCarGlassDesafioTecnicoDto = new CarGlassDesafioTecnicoCreateDto("Repository Update Test - Created", CrossCutting.Enums.CarGlassDesafioTecnicoType.AWS);
        var newCarGlassDesafioTecnico = Domain.Entities.CarGlassDesafioTecnico.Create(newCarGlassDesafioTecnicoDto.Name, newCarGlassDesafioTecnicoDto.CarGlassDesafioTecnicoType);

        var boilerplateRepositoryMongoDb = new CarGlassDesafioTecnicoProjectionRepository(MongoDatabase);

        await boilerplateRepositoryMongoDb.Insert(newCarGlassDesafioTecnico, CancellationToken.None);
        #endregion

        #region Act
        var updateCarGlassDesafioTecnicoName = "Repository Update Test - Updated";
        var updateCarGlassDesafioTecnicoDto = new CarGlassDesafioTecnicoCreateDto(updateCarGlassDesafioTecnicoName, newCarGlassDesafioTecnico.CarGlassDesafioTecnicoType);
        var updateCarGlassDesafioTecnico = Domain.Entities.CarGlassDesafioTecnico.Create(updateCarGlassDesafioTecnicoDto.Name, updateCarGlassDesafioTecnicoDto.CarGlassDesafioTecnicoType);
        updateCarGlassDesafioTecnico.Id = newCarGlassDesafioTecnico.Id;

        await boilerplateRepositoryMongoDb.Update(updateCarGlassDesafioTecnico, CancellationToken.None);
        #endregion

        #region Assert
        var checkCarGlassDesafioTecnico = await boilerplateRepositoryMongoDb.GetById(updateCarGlassDesafioTecnico.Id);

        updateCarGlassDesafioTecnico.Id.Should().Be(checkCarGlassDesafioTecnico.Id);
        updateCarGlassDesafioTecnico.Name.Should().Be(checkCarGlassDesafioTecnico.Name);
        updateCarGlassDesafioTecnico.CarGlassDesafioTecnicoType.Should().Be(checkCarGlassDesafioTecnico.CarGlassDesafioTecnicoType); 
        #endregion
    }

    [TestMethod]
    public async Task SHOULD_NOT_GET_BOILERPLATE()
    {
        #region Arrange
        var boilerplateRepositoryMongoDb = new CarGlassDesafioTecnicoProjectionRepository(MongoDatabase);
        #endregion

        #region Act
        var boilerplate = await boilerplateRepositoryMongoDb.GetById("Id doesn´t exists");
        #endregion

        #region Assert
        boilerplate.Should().BeNull(); 
        #endregion
    }
}
