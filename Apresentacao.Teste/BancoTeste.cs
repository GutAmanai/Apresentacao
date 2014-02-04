using Apresentacao.Persistencia.Infra;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Apresentacao.Teste
{
    [TestClass]
    public class BancoTeste
    {
        [TestMethod]
        public void CriarBanco()
        {
            BDFuncoes.ExcluirBanco();
            BDFuncoes.CriarBanco();
        }

        [TestMethod]
        public void AtualizarBanco()
        {
            BDFuncoes.AtualizarBanco();
        }
    }
}
