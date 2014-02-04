using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apresentacao.Dominio.Entidades
{
    public class Empresa : EntidadeBase
    {
        public virtual string Nome          { get; protected set; }
        public virtual string NomeFantasia  { get; protected set; }
        public virtual string WebSite       { get; protected set; }
        public virtual string Cnpj             { get; protected set; }
        
        private ICollection<Contato> _contatos;
        public virtual IEnumerable<Contato> Contatos
        {
            get { return _contatos; }
        }        

        public Empresa()
        {
        }

        public Empresa(string nome, string nomeFantasia, string website, string cnpj) : this()
        {
            this.Nome = nome;
            this.NomeFantasia = nomeFantasia;
            this.WebSite = website;
            this.Cnpj = cnpj;
        }

        public virtual void AdicionarNome(string nome)
        {
            this.Nome = nome;
        }

        public virtual void AdicionarNomeFantasia(string nomeFantasia)
        {
            this.NomeFantasia = nomeFantasia;
        }

        public virtual void AdicionarWebSite(string webSite)
        {
            this.WebSite = webSite;
        }

        public virtual void AdicionarCnpj(string cnpj)
        {
            this.Cnpj = cnpj;
        }

        public virtual void AdicionarContatos(Contato contato)
        {
            if(this._contatos == null)
                this._contatos = new Collection<Contato>();
            this._contatos.Add(contato);
        }
    }
}
