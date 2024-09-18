using CarGlassDesafioTecnico.Domain.Interface.Functions;

namespace CarGlassDesafioTecnico.Domain.Function
{
    public class DecomposicaoNumeroFunction : IDecomposicaoNumeroFunction
    {
        public List<int> ObterDivisores(int numero)
        {
            List<int> divisores = new List<int>();
            for (int i = 1; i <= numero; i++)
            {
                if (numero % i == 0)
                {
                    divisores.Add(i);
                }
            }
            return divisores;
        }

        public List<int> ObterDivisoresPrimos(List<int> divisores)
        {
            List<int> divisoresPrimos = new List<int>();
            foreach (int divisor in divisores)
            {
                if (EhPrimo(divisor))
                {
                    divisoresPrimos.Add(divisor);
                }
            }
            return divisoresPrimos;
        }

        private bool EhPrimo(int numero)
        {
            if (numero <= 1) return false;
            for (int i = 2; i <= Math.Sqrt(numero); i++)
            {
                if (numero % i == 0) return false;
            }
            return true;
        }
    }
}
