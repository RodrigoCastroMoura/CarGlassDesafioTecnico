using CarGlassDesafioTecnico.Domain.Data;
using CarGlassDesafioTecnico.Domain.Interface.Functions;
using CarGlassDesafioTecnico.Dto;

namespace CarGlassDesafioTecnico.Application.Usecases.Usuarios.Read
{
    public class DecomporNumeroUsecases : IDecomporNumeroUsecases
    {
        private  IDecomposicaoNumeroFunction iDecomposicaoNumeroFunction;

        public DecomporNumeroUsecases(IDecomposicaoNumeroFunction iDecomposicaoNumeroFunction)
        {
            this.iDecomposicaoNumeroFunction = iDecomposicaoNumeroFunction;
        }

        public async Task<ServiceResponse<DecomposicaoNumeroDto>> Execute(int numero)
        {
            var response = new ServiceResponse<DecomposicaoNumeroDto>();

            try
            {
                var divisores = iDecomposicaoNumeroFunction.ObterDivisores(numero);
                var divisoresPrimos = iDecomposicaoNumeroFunction.ObterDivisoresPrimos(divisores);

                response.Data = new DecomposicaoNumeroDto { DivisoresPrimos = divisoresPrimos, Divisores = divisores };
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;

                return response;
            }
           
        }
    }
}
