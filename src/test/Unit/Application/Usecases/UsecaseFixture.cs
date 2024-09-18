using AutoMapper;
using CarGlassDesafioTecnico.Infra.Mappers.CarGlassDesafioTecnicoProfile;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarGlassDesafioTecnico.Test.Unit.Application.Usecases;

public abstract class UsecaseFixture
{
    protected IMapper _mapper;

    [TestInitialize]
    public virtual void TestInitialize()
    {
        var config = new MapperConfiguration(opts =>
        {
            opts.AddProfile<CarGlassDesafioTecnicoProfile>();
        });

        _mapper = config.CreateMapper();
    }
}
