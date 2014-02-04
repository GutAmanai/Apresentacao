using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apresentacao.Dominio.Entidades
{
    public class Contato : EntidadeBase
    {
        public virtual Empresa Empresa      { get; protected set; }
        public virtual string Nome          { get; protected set; }
        public virtual string Telefone      { get; protected set; }
        public virtual string Email         { get; protected set; }
        public virtual string Celular       { get; protected set; }

        protected Contato()
        {
        }

        public Contato(Empresa empresa) : this()
        {
            this.Empresa = empresa;
        }

        public virtual void AdicionarNome(string nome)
        {
            this.Nome = nome;
        }

        public virtual void AdicionarTelefone(string telefone)
        {
            this.Telefone = telefone;
        }

        public virtual void AdicionarEmail(string email)
        {
            this.Email = email;
        }

        public virtual void AdicionarCelular(string celular)
        {
            this.Celular = celular;
        }
    }
}
