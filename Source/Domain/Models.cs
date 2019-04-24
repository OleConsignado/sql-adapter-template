using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// TODO: REMOVER A PASTA "DOMAIN" E TODOS OS SEUS ARQUIVOS
/// Classe mockada apenas para referência do projeto.
/// </summary>
namespace SqlAdapter.Template.Domain
{
    public class Filme
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTimeOffset DataLancamento { get; set; }
    }

    public class Pesquisa
    {
        public string TermoPesquisa { get; set; }
        public int? AnoLancamento { get; set; }
    }
}
