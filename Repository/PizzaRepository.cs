using CompetitionEventsManager.Data;
using CompetitionEventsManager.Models;
using CompetitionEventsManager.Repository.IRepository;

namespace CompetitionEventsManager.Repository
{
    public class PizzaRepository : Repository<Pizza>, IPizzaRepository
    {

        private readonly DBContext _db;

        public PizzaRepository(DBContext db) : base(db)
        {
            _db = db;
        }






    }
}
