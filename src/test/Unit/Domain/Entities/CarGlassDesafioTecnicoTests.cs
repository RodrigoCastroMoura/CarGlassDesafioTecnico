using CarGlassDesafioTecnico.Test.Shared.Dto;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarGlassDesafioTecnico.Test.Unit.Domain.Entities;

[TestClass]
public class CarGlassDesafioTecnicoDomainTests
{
    [TestMethod]
    public void SHOULD_CREATE_BOILERPLATE()
    {
        var boilerplateDTO = CreateCarGlassDesafioTecnicoDefaultTestDto.GetDefault();
        var boilerplate = CarGlassDesafioTecnico.Domain.Entities.CarGlassDesafioTecnico.Create(boilerplateDTO.Name, boilerplateDTO.CarGlassDesafioTecnicoType);

        boilerplate.Name.Should().Be(boilerplateDTO.Name);
        boilerplate.CarGlassDesafioTecnicoType.Should().Be(boilerplateDTO.CarGlassDesafioTecnicoType);
    }


    [TestMethod]
    public void SHOULD_CREATE_BOILERPLATE_WITH_EMPTY_CONSTRUCTOR()
    {
        var id = Guid.NewGuid().ToString();
        var boilerplate = CarGlassDesafioTecnico.Domain.Entities.CarGlassDesafioTecnico.Create(id);

        boilerplate.Id.Should().Be(id);
        boilerplate.OwnerId.Should().Be(null);
    }
}
