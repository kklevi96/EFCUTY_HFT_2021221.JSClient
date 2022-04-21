using EFCUTY_HFT_2021221.Models;
using System.Linq;

namespace EFCUTY_HFT_2021221.Repository
{
    public interface ICitizenRepository
    {
        void Create(Citizen citizen);
        void Delete(int id);
        Citizen Read(int id);
        void Update(Citizen citizen);
        IQueryable<Citizen> ReadAll();
    }
}