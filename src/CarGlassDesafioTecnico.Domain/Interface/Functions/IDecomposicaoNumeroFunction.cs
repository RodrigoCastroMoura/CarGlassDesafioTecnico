namespace CarGlassDesafioTecnico.Domain.Interface.Functions
{
    public interface IDecomposicaoNumeroFunction
    {
        List<int> ObterDivisores(int numero);
        List<int> ObterDivisoresPrimos(List<int> divisores);

    }
}
