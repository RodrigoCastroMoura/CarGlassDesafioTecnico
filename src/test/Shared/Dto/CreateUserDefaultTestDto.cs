using CarGlassDesafioTecnico.CrossCutting.Enums;
using CarGlassDesafioTecnico.Dto.CarGlassDesafioTecnico;

namespace CarGlassDesafioTecnico.Test.Shared.Dto
{
    public static class CreateCarGlassDesafioTecnicoDefaultTestDto
    {
        public static CarGlassDesafioTecnicoCreateDto GetDefault() =>
            new("Nalfu", CarGlassDesafioTecnicoType.AWS);
    }
}

