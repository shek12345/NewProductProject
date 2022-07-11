using System.ComponentModel.DataAnnotations;

namespace NewProductProject.Models
{
    public class Cart
    {
        [Key]
        public int Cid { get; set; }
        public int Pid { get; set; }
        public int Uid { get; set; }

        public int Quantity { get; set; }
    }
}
