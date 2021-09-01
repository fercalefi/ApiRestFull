using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRestFull.Model.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public long Id { get; set; }
    }
}
