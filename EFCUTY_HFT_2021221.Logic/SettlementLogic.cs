using EFCUTY_HFT_2021221.Models;
using EFCUTY_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCUTY_HFT_2021221.Logic
{
    public class SettlementLogic : ISettlementLogic
    {
        ISettlementRepository settlementRepository;

        public SettlementLogic(ISettlementRepository settlementRepository)
        {
            this.settlementRepository = settlementRepository;
        }

        public void Create(Settlement settlement)
        {
            if (settlement.HDI < 0 || settlement.HDI > 1)
            {
                throw new ArgumentException("HDI must be between 0 and 1!");
            }

            if (settlement.Population < 1)
            {
                throw new ArgumentException("There must be at least one person who lives in the settlement!");
            }

            if (settlement.SettlementName == "")
            {
                throw new ArgumentException("Settlement must have a name!");
            }

            settlementRepository.Create(settlement);
        }

        public Settlement Read(int id)
        {
            return settlementRepository.Read(id);
        }

        public void Update(Settlement settlement)
        {
            if (settlement.HDI < 0 || settlement.HDI > 1)
            {
                throw new ArgumentException("HDI must be between 0 and 1!");
            }

            if (settlement.Population < 1)
            {
                throw new ArgumentException("There must be at least one person who lives in the settlement!");
            }

            if (settlement.SettlementName == "")
            {
                throw new ArgumentException("Settlement must have a name!");
            }

            settlementRepository.Update(settlement);
        }

        public void Delete(int id)
        {
            if (CanBeDeleted(id))
            {
                settlementRepository.Delete(id);
            }
        }

        public IEnumerable<Settlement> ReadAll()
        {
            return settlementRepository.ReadAll();
        }


        //noncrud 5: list all settlements which have no people with a criminal record
        public IEnumerable<Settlement> GoodSettlements()
        {
            return from x in settlementRepository.ReadAll()
                   where x.Citizens.All(y => !y.HasCriminalRecord)
                   select x;
        }

        //noncrud plus: what is the average HDI of the settlements in the countries
        public IEnumerable<KeyValuePair<string, double>> AvgHDIByCountries()
        {
            return from x in settlementRepository.ReadAll()
                   group x by x.Country.Name into g
                   select new KeyValuePair<string, double>
                   (g.Key, g.Average(t => t.HDI));
        }

        //helper method for delete
        public bool CanBeDeleted(int id)
        {
            Settlement settlement = Read(id);
            return settlement.Citizens.Count == 0;
        }
    }
}
