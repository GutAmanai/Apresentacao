using Apresentacao.Dominio.Repositorio;
using Apresentacao.Persistencia.Infra;
using NHibernate;

namespace Apresentacao.Persistencia.Repositorio
{
    public class UnidadeDeTrabalho : IUnidadeDeTrabalho
    {
        public ISession Sessao { get; private set; }

        public UnidadeDeTrabalho()
        {
            this.Inicializar();
        }

        public bool ATransacaoEstaAtiva
        {
            get { return Sessao.Transaction.IsActive; }
        }

        public bool ASessaoEstaAberta
        {
            get { return Sessao.IsOpen; }
        }

        public void Inicializar()
        {
            if (Sessao == null || !ASessaoEstaAberta)
                Sessao = SessionFactory.Instancia.OpenSession();

            this.Sessao.FlushMode = FlushMode.Commit;
            this.Sessao.BeginTransaction();

        }

        public void Salvar()
        {
            Sessao.Flush();
            Sessao.Transaction.Commit();
        }

        public void Dispose()
        {
            if (this.Sessao.IsOpen)
                Sessao.Close();

            Sessao.Dispose();
        }

    }
}
