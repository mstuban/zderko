using System.ComponentModel.DataAnnotations;

namespace zderko.Models
{
    public class MethodOfPayment
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }
}