using Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL {
    public class TeamAmericaContext : DbContext {
        public TeamAmericaContext() 
            : base("name=azureDb") {
        }
             
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Response> Responses { get; set; }
    }
}
