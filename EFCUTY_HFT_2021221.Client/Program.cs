using ConsoleTools;
using EFCUTY_HFT_2021221.Models;
using System;
using System.Collections.Generic;

namespace EFCUTY_HFT_2021221.Client
{
    class Program
    {
        static RestService rest = new("http://localhost:54726");

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            int wait = 15;
            for (int i = 0; i < wait; i++)
            {
                Console.WriteLine("{0}", wait - i);
                System.Threading.Thread.Sleep(750);
                Console.Clear();
            }

            Console.CursorVisible = true;
            Console.WriteLine("Program loaded, showing menu...");
            System.Threading.Thread.Sleep(200);

            ConsoleMenu submenuCitizens = new ConsoleMenu(args, level: 1)
                .Add("Create a citizen", () =>
                {
                    CreateCitizen();
                    Console.WriteLine("Citizen created successfully!");
                    Console.ReadKey();
                })
                .Add("Read all citizens", () =>
                {
                    Console.WriteLine(ReadAllCitizens());
                    Console.ReadKey();
                })
                .Add("Read a citizen", () =>
                {
                    Console.WriteLine(ReadCitizen());
                    Console.ReadKey();
                })
                .Add("Update a citizen", () =>
                {
                    UpdateCitizen();
                    Console.WriteLine("Citizen updated successfully!");
                    Console.ReadKey();
                })
                .Add("Delete a citizen", () =>
                {
                    DeleteCitizen();
                    Console.WriteLine("Citizen deleted successfully!");
                    Console.ReadKey();
                })
                .Add("Return to main menu", ConsoleMenu.Close);

            ConsoleMenu submenuSettlements = new ConsoleMenu(args, level: 1)
                .Add("Create a settlement", () =>
                {
                    CreateSettlement();
                    Console.WriteLine("Settlement created successfully!");
                    Console.ReadKey();
                })
                .Add("Read all settlements", () =>
                {
                    Console.WriteLine(ReadAllSettlements());
                    Console.ReadKey();
                })
                .Add("Read a settlement", () =>
                {
                    Console.WriteLine(ReadSettlement());
                    Console.ReadKey();
                })
                .Add("Update a settlement", () =>
                {
                    UpdateSettlement();
                    Console.WriteLine("Settlement updated successfully!");
                    Console.ReadKey();
                })
                .Add("Delete a settlement", () =>
                {
                    DeleteSettlement();
                    Console.WriteLine("If there were no citizens in the settlement, the settlement has been deleted successfully!");
                    Console.ReadKey();
                })
                .Add("Return to main menu", ConsoleMenu.Close);

            ConsoleMenu submenuCountries = new ConsoleMenu(args, level: 1)
                .Add("Create a country", () =>
                {
                    CreateCountry();
                    Console.WriteLine("Country created successfully!");
                    Console.ReadKey();
                })
                .Add("Read all countries", () =>
                {
                    Console.WriteLine(ReadAllCountries());
                    Console.ReadKey();
                })
                .Add("Read a country", () =>
                {
                    Console.WriteLine(ReadCountry());
                    Console.ReadKey();
                })
                .Add("Update a country", () =>
                {
                    UpdateCountry();
                    Console.WriteLine("Country updated successfully!");
                    Console.ReadKey();
                })
                .Add("Delete a country", () =>
                {
                    DeleteCountry();
                    Console.WriteLine("If there were no citizens or settlements in the country, the country has been deleted successfully!");
                    Console.ReadKey();
                })
                .Add("Return to main menu", ConsoleMenu.Close);

            ConsoleMenu submenuNoncrud = new ConsoleMenu(args, level: 1)
                .Add("Citizens having a criminal record who live in a settlement with a HDI at least 0.9", () =>
                {
                    Console.WriteLine(string.Join("\n", rest.Get<Citizen>("citizenstat/developedcriminals")));
                    Console.ReadKey();
                })
                .Add("Citizens who were born before 1940.01.01 and live in a country which is not a member of OECD", () =>
                {
                    Console.WriteLine(string.Join("\n", rest.Get<Citizen>("citizenstat/pooroldpeople")));
                    Console.ReadKey();
                })
                .Add("Population of all countries", () =>
                {
                    Console.WriteLine(string.Join("\n", rest.Get<KeyValuePair<string, int>>("countrystat/population")));
                    Console.ReadKey();
                })
                .Add("Countries having less than $10000 GDP per capita (you will see the full GDP of the countries,\n\tto see the population, query the population of all countries", () =>
                {
                    Console.WriteLine(string.Join("\n", rest.Get<KeyValuePair<string, int>>("countrystat/poorcountries")));
                    Console.ReadKey();
                })
                .Add("Average HDI of all countries", () =>
                {
                    Console.WriteLine(string.Join("\n", rest.Get<KeyValuePair<string, double>>("settlementstat/avghdibycountries")));
                    Console.ReadKey();
                })
                .Add("Settlements which have no people with a criminal record", () =>
                {
                    Console.WriteLine(string.Join("\n", rest.Get<Settlement>("settlementstat/goodsettlements")));
                    Console.ReadKey();
                })
                .Add("Return to main menu", ConsoleMenu.Close);

            ConsoleMenu menu = new ConsoleMenu(args, level: 0)
                .Add("Methods for Citizens", submenuCitizens.Show)
                .Add("Methods for Settlements", submenuSettlements.Show)
                .Add("Methods for Countries", submenuCountries.Show)
                .Add("Noncrud queries", submenuNoncrud.Show)
                .Add("Exit", () => Environment.Exit(0));
            menu.Show();
        }

        public static string ReadAllSettlements()
        {
            var settlements = rest.Get<Settlement>("settlement");
            return string.Join("\n", settlements);
        }

        public static string ReadAllCountries()
        {
            var countries = rest.Get<Country>("country");
            return string.Join("\n", countries);
        }

        public static string ReadAllCitizens()
        {
            var citizens = rest.Get<Citizen>("citizen");
            return string.Join("\n", citizens);
        }

        public static void CreateCitizen()
        {
            Console.Write("Name of citizen: ");
            string citizenName = Console.ReadLine();
            Console.Write("Birth date of citizen (enter in YYYY.MM.DD format): ");
            string[] birthDate = Console.ReadLine().Split('.');
            int year = int.Parse(birthDate[0]);
            int month = int.Parse(birthDate[1]);
            int day = int.Parse(birthDate[2]);
            Console.Write("Income of citizen: ");
            int income = int.Parse(Console.ReadLine());
            Console.Write("Does he/she have a criminal record?\nIf yes, type yes, if no, type no. ");
            string criminalRecord = Console.ReadLine();
            if (criminalRecord != "yes" && criminalRecord != "no")
            {
                throw new ArgumentException("Only yes or no answers are allowed");
            }
            Console.Write("Country ID of citizen: ");
            int countryID = int.Parse(Console.ReadLine());
            Console.Write("Settlement ID of citizen: ");
            int settlementID = int.Parse(Console.ReadLine());
            Citizen citizen = new()
            {
                Name = citizenName,
                BirthDate = new DateTime(year, month, day),
                HasCriminalRecord = criminalRecord == "yes",
                CitizenshipID = countryID,
                SettlementID = settlementID,
                IncomeInUSD = income
            };
            rest.Post(citizen, "citizen");
        }

        public static void CreateSettlement()
        {
            Console.Write("Name of settlement: ");
            string name = Console.ReadLine();
            Console.Write("Country ID of settlement: ");
            int cID = int.Parse(Console.ReadLine());
            Console.Write("Population of settlement: ");
            int population = int.Parse(Console.ReadLine());
            Console.Write("HDI (0<=HDI<=1) of settlement: ");
            double HDI = double.Parse(Console.ReadLine());
            Settlement settlement = new()
            {
                SettlementName = name,
                CountryID = cID,
                HDI = HDI,
                Population = population
            };
            rest.Post(settlement, "settlement");
        }

        public static void CreateCountry()
        {
            Console.Write("Name of country: ");
            string name = Console.ReadLine();
            Console.Write("Yearly GDP of the country in million USD: ");
            int GDP = int.Parse(Console.ReadLine());
            Console.Write("Is the country a member of OECD? If yes, type yes, if no, type no. ");
            string OECDMember = Console.ReadLine();
            if (OECDMember != "yes" && OECDMember != "no")
            {
                throw new ArgumentException("Only yes or no answers are allowed");
            }

            Country country = new()
            {
                IsOECDMember = OECDMember == "yes",
                Name = name,
                TotalGDPInMillionUSD = GDP
            };
            rest.Post(country, "country");
        }

        public static Citizen ReadCitizen()
        {
            Console.Write("ID of citizen whose details you are interested in: ");
            int id = int.Parse(Console.ReadLine());
            return rest.Get<Citizen>(id, "citizen");
        }

        public static Settlement ReadSettlement()
        {
            Console.Write("ID of settlement whose details you are interested in: ");
            int id = int.Parse(Console.ReadLine());
            return rest.Get<Settlement>(id, "settlement");
        }

        public static Country ReadCountry()
        {
            Console.Write("ID of country whose details you are interested in: ");
            int id = int.Parse(Console.ReadLine());
            return rest.Get<Country>(id, "country");
        }

        public static void UpdateCitizen()
        {
            Console.Write("ID of citizen you would like to update: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("New name of citizen: ");
            string citizenName = Console.ReadLine();
            Console.Write("New income of citizen: ");
            int income = int.Parse(Console.ReadLine());
            Console.Write("Does he/she have now a criminal record?\nIf yes, type yes, if no, type no. ");
            string criminalRecord = Console.ReadLine();
            if (criminalRecord != "yes" && criminalRecord != "no")
            {
                throw new ArgumentException("Only yes or no answers are allowed");
            }
            Console.Write("New country ID of citizen: ");
            int countryID = int.Parse(Console.ReadLine());
            Console.Write("New settlement ID of citizen: ");
            int settlementID = int.Parse(Console.ReadLine());
            Citizen citizen = new()
            {
                PersonID = id,
                Name = citizenName,
                HasCriminalRecord = criminalRecord == "yes",
                CitizenshipID = countryID,
                SettlementID = settlementID,
                IncomeInUSD = income
            };
            rest.Put(citizen, "citizen");
        }
        public static void UpdateSettlement()
        {
            Console.Write("Id of settlement you would like to update: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("New name of settlement: ");
            string name = Console.ReadLine();
            Console.Write("New country ID of settlement: ");
            int cID = int.Parse(Console.ReadLine());
            Console.Write("New population of settlement: ");
            int population = int.Parse(Console.ReadLine());
            Console.Write("New HDI (0<=HDI<=1) of settlement: ");
            double HDI = double.Parse(Console.ReadLine());
            Settlement settlement = new()
            {
                SettlementID = id,
                SettlementName = name,
                CountryID = cID,
                HDI = HDI,
                Population = population
            };
            rest.Put(settlement, "settlement");
        }

        public static void UpdateCountry()
        {
            Console.Write("ID of country you would like to update: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("New name of country: ");
            string name = Console.ReadLine();
            Console.Write("New yearly GDP of the country in million USD: ");
            int GDP = int.Parse(Console.ReadLine());
            Console.Write("Is the country now a member of OECD? If yes, type yes, if no, type no. ");
            string OECDMember = Console.ReadLine();
            if (OECDMember != "yes" && OECDMember != "no")
            {
                throw new ArgumentException("Only yes or no answers are allowed");
            }

            Country country = new()
            {
                CountryID = id,
                IsOECDMember = OECDMember == "yes",
                Name = name,
                TotalGDPInMillionUSD = GDP
            };
            rest.Put(country, "country");
        }

        public static void DeleteCitizen()
        {
            Console.Write("Id of citizen you would like to delete: ");
            int id = int.Parse(Console.ReadLine());
            rest.Delete(id, "citizen");
        }

        public static void DeleteSettlement()
        {
            Console.Write("Id of settlement you would like to delete: ");
            int id = int.Parse(Console.ReadLine());
            rest.Delete(id, "settlement");
        }

        public static void DeleteCountry()
        {
            Console.Write("Id of country you would like to delete: ");
            int id = int.Parse(Console.ReadLine());
            rest.Delete(id, "country");
        }
    }
}
