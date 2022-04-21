using EFCUTY_HFT_2021221.Models;
using System.Collections.Generic;

namespace EFCUTY_HFT_2021221.Logic
{
    public interface ICountryLogic
    {
        void Create(Country country);
        void Delete(int id);
        IEnumerable<Country> ReadAll();
        IEnumerable<KeyValuePair<string, int>> PoorCountries();
        IEnumerable<KeyValuePair<string, int>> Population();
        Country Read(int id);
        bool ThisNameExists(string name);
        void Update(Country country);
    }
}