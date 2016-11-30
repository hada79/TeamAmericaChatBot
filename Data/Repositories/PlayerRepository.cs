namespace Data.Repositories {

    using Data.DAL;
    using Data.Models;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class PlayerRepository {
        TeamAmericaContext context;

        public PlayerRepository() {
            context = new TeamAmericaContext();
        }

        public bool Create(Player player) {
            try {
                context.Players.Add(player);

                int x = context.SaveChanges();
                return (x > 0);
            } catch {
                return false;
            }
        }

        public bool UpdateInfo(Player player) {
            context.Entry(player).State = EntityState.Modified;
            try {
                int x = context.SaveChanges();
                return (x > 0);
            } catch {
                return false;
            }
        }

        public Player GetByFirstName(string firstName) {
            return context.Players.Where(x => x.FirstName == firstName).FirstOrDefault();
        }

        public List<Player> GetActive() {
            return context.Players.Where(x => x.Inactive == false).ToList();
        }

        public bool Inactivate(Player player) {
            player.Inactive = true;

            context.Entry(player).State = EntityState.Modified;
            try {
                int x = context.SaveChanges();
                return (x > 0);
            } catch {
                return false;
            }
        }
    }
}
