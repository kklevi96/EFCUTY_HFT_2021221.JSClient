using EFCUTY_HFT_2021221.Data;
using EFCUTY_HFT_2021221.Models;
using System.Linq;

namespace EFCUTY_HFT_2021221.Repository
{
    public class CitizenRepository : ICitizenRepository
    {
        WorldDbContext db;
        public CitizenRepository(WorldDbContext db)
        {
            this.db = db;
        }

        public void Create(Citizen citizen)
        {
            db.Citizens.Add(citizen);
            db.SaveChanges();
        }

        public Citizen Read(int id)
        {
            return db.Citizens.FirstOrDefault(t => t.PersonID == id);
        }

        public void Update(Citizen citizen)
        {
            var oldCitizen = Read(citizen.PersonID);
            oldCitizen.Name = citizen.Name;
            oldCitizen.SettlementID = citizen.SettlementID;
            oldCitizen.CitizenshipID = citizen.CitizenshipID;
            oldCitizen.HasCriminalRecord = citizen.HasCriminalRecord;
            oldCitizen.IncomeInUSD = citizen.IncomeInUSD;
            oldCitizen.BirthDate = citizen.BirthDate;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.Citizens.Remove(Read(id));
            db.SaveChanges();
        }

        public IQueryable<Citizen> ReadAll()
        {
            return db.Citizens;
        }
    }
}