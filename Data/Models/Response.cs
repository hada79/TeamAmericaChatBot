using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models {
        
    public class Response {
        [Key]
        public Guid ResponseId { get; set; }
        public string PlayerStatus { get; set; }
        public DateTime ResponseDate { get; set; }
        public Player Player { get; set; }
        public Game Game { get; set; }
    }
}
