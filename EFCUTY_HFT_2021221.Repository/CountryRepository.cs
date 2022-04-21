using EFCUTY_HFT_2021221.Data;
using EFCUTY_HFT_2021221.Models;
using System.Linq;

namespace EFCUTY_HFT_2021221.Repository
{
    public class CountryRepository : ICountryRepository
    {
        WorldDbContext db;
        public CountryRepository(WorldDbContext db)
        {
            this.db = db;
        }

        public void Create(Country country)
        {
            db.Countries.Add(country);
            db.SaveChanges();
        }

        public Country Read(int id)
        {
            return db.Countries.FirstOrDefault(t => t.CountryID == id);
        }

        public void Update(Country country)
        {
            var oldCountry = Read(country.CountryID);
            oldCountry.Name = country.Name;
            oldCountry.TotalGDPInMillionUSD = country.TotalGDPInMillionUSD;
            oldCountry.IsOECDMember = country.IsOECDMember;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.Countries.Remove(Read(id));
            db.SaveChanges();
        }

        public IQueryable<Country> ReadAll()
        {
            return db.Countries;
        }
    }
}
