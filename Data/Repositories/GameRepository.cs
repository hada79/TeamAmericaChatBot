namespace Data.Repositories {

    using Data.DAL;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GameRepository {
        TeamAmericaContext context;

        public GameRepository() {
            context = new TeamAmericaContext();
        }

        public bool Create(Game game) {
            try {
                context.Games.Add(game);

                int x = context.SaveChanges();
                return (x > 0);
            } catch {
                return false;
            }
        }

        public List<Game> GetNext() {
            DateTime maxDate = context.Games.Max(x => x.DateTime);
            return context.Games.Where(x => x.DateTime == maxDate).ToList();
        }

        public List<Game> GetPastFive() {
            return context.Games.OrderByDescending(x => x.DateTime).Take(5).ToList();
        }
    }
}
