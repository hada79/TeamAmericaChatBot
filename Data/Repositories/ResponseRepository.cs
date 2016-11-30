using Data.DAL;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories {
    class ResponseRepository {
        TeamAmericaContext context;

        public ResponseRepository() {
            context = new TeamAmericaContext();
        }

        public bool Create(Response response) {
            try {
                context.Responses.Add(response);

                int x = context.SaveChanges();
                return (x > 0);
            } catch {
                return false;
            }
        }

        public List<Response> GetByGame(Guid gameId) {            
            return context.Responses.Where(x => x.Game.GameId == gameId).ToList();
        }

        public bool Update(Response response) {
            context.Entry(response).State = EntityState.Modified;
            try {
                int x = context.SaveChanges();
                return (x > 0);
            } catch {
                return false;
            }
        }
    }
}
