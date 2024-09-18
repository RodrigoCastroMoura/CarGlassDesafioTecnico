using CarGlassDesafioTecnico.Domain.Data;
using CarGlassDesafioTecnico.Dto;

namespace CarGlassDesafioTecnico.Application.Usecases.Usuarios.Read
{
    public interface IDecomporNumeroUsecases
    {
        Task<ServiceResponse<DecomposicaoNumeroDto>> Execute(int numero);
    }
}
