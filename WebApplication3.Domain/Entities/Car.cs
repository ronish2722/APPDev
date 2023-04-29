using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.Domain.Entities
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarId { get; set; }

        public string CarName { get; set; }

        public string Brand { get; set; }

        public string Description { get; set; }

        public string CarStatus { get; set; }

        public int? NumberOfRents { get; set; }

        public ICollection<Damage> Damages { get; set; }

        public ICollection<Request> Requests { get; set; }

    }
}
