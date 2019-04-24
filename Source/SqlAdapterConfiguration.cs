using Otc.ComponentModel.DataAnnotations;

namespace SqlAdapter.Template.TmdbAdapter
{
    public class SqlAdapterConfiguration
    {
        [Required]
        public string SqlConnectionString { get; set; }

        [Required]
        public int SegundosValidadeCacheParametro { get; set; }
    }
}
