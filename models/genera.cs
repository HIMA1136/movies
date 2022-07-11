

using System.ComponentModel.DataAnnotations.Schema;

namespace api_movia.models
{
    public class genera
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }
        [MaxLength(length :100)]
        public string Name { get; set; }

    }
}
