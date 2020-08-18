using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace MVCFlowerShop.Models
{
    public class Flower
    {
        public int ID { get; set; }
        public string FlowerName { get; set; }
        public DateTime FlowerProducedDate { get; set; }
        public string Type { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
    }
}
