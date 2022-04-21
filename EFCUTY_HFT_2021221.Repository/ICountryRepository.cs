using EFCUTY_HFT_2021221.Models;
using System.Linq;

namespace EFCUTY_HFT_2021221.Repository
{
    public interface ICountryRepository
    {
        void Create(Country country);
        void Delete(int id);
        Country Read(int id);
        void Update(Country country);
        IQueryable<Country> ReadAll();

    }
}