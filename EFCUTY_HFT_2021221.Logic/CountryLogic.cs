using EFCUTY_HFT_2021221.Models;
using EFCUTY_HFT_2021221.Repository;
//using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCUTY_HFT_2021221.Logic
{
    public class CountryLogic : ICountryLogic
    {
        ICountryRepository countryRepository;
        //IMessenger messenger;

        public CountryLogic(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
            //this.messenger = messenger;
        }

        public void Create(Country country)
        {
            if (country.TotalGDPInMillionUSD < 100)
            {
                return;
                //throw new ArgumentException("A country just can't be that poor!");
            }

            if (ThisNameExists(country.Name))
            {
                return;
                //messenger.Send("The country with this name already exists!", "LogicInfo");
            }

            if (country.Name == "")
            {
                return;
                //messenger.Send("Country must have a name!", "LogicInfo");
            }

            countryRepository.Create(country);
            //messenger.Send("Country created", "LogicInfo");

        }

        public Country Read(int id)
        {
            return countryRepository.Read(id);
        }

        public void Update(Country country)
        {
            if (country.TotalGDPInMillionUSD < 100)
            {
                return;
                //throw new ArgumentException("A country just can't be that poor!");
            }

            //if (ThisNameExists(country.Name))
            //{
                //return;
                //messenger.Send("The country with this name already exists!", "LogicInfo");
            //}

            if (country.Name == "")
            {
                return;
                //messenger.Send("Country must have a name!", "LogicInfo");
            }


            //messenger.Send("A country just can't be that poor!", "LogicInfo");
            countryRepository.Update(country);


            //if (country.Name == "")
            //{
            //    //messenger.Send("Country must have a name!", "LogicInfo");
            //    return;
            //}
            //if(ThisNameExists(country.Name))
            //{
            //    return;
            //}
            //else
            //{
            //    //countryRepository.Update(country);
            //    //messenger.Send("Country updated", "LogicInfo");
            //}
        }

        public void Delete(int id)
        {
            if (CanBeDeleted(id))
            {
                countryRepository.Delete(id);
                //messenger.Send("Country deleted", "LogicInfo");
            }
            else
            {
                //messenger.Send("Country couldn't be deleted as it had settlement(s) and/or citizen(s)", "LogicInfo");
            }
        }

        public IEnumerable<Country> ReadAll()
        {
            return countryRepository.ReadAll();
        }

        //noncrud 1: which countries have a GDP per capita less than 10000 USD?

        public IEnumerable<KeyValuePair<string, int>> PoorCountries()
        {
            return from x in countryRepository.ReadAll()
                   where (double)x.TotalGDPInMillionUSD / x.Settlements
                                                    .Select(y => y.Population)
                                                    .Sum() < 0.01
                   select new KeyValuePair<string, int>
                   (
                       x.Name,
                       x.TotalGDPInMillionUSD
                   );
        }

        //helper method for noncrud 1
        public static int CountPopulation(Country country)
        {
            return country
                .Settlements
                .Select(x => x.Population)
                .Sum();
        }

        //noncrud 2: list the population of all countries
        public IEnumerable<KeyValuePair<string, int>> Population()
        {
            return from x in countryRepository.ReadAll()
                   select new KeyValuePair<string, int>
                   (
                       x.Name,
                       x.Settlements
                       .Select(y => y.Population)
                       .Sum()
                   );
        }

        //helper method for Create
        public bool ThisNameExists(string name)
        {
            //var a = from x in countryRepository.GetAll()
            //where x.Name.Equals(name)
            //select x;
            //return a.Count() > 0

            return countryRepository.ReadAll()
                .Any(x => x.Name == name);
        }

        //helper method to Delete
        public bool CanBeDeleted(int id)
        {
            Country country = Read(id);
            return country.Citizens.Count == 0 && country.Settlements.Count == 0;
        }
    }
}
