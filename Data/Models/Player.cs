using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models {
    public class Player {
        [Key]
        public Guid PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CellPhone { get; set; }
        public bool Coach { get; set; }
        public bool Admin { get; set; }
        public bool Inactive { get; set; }
        public ICollection<Response> Responses { get; set; }
    }
}
