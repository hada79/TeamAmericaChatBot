using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models {
    public class Game {
        [Key]
        public Guid GameId { get; set; }
        public DateTime DateTime { get; set; }
        public string Field { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public ICollection<Response> Responses { get; set; }

    }
}
