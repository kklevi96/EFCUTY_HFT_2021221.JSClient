using EFCUTY_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using System;


namespace EFCUTY_HFT_2021221.Data
{
    public class WorldDbContext : DbContext
    {
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Settlement> Settlements { get; set; }
        public virtual DbSet<Citizen> Citizens { get; set; }

        public WorldDbContext()
        {
            try
            {
                this.Database.EnsureCreated();
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine("Fura exception, restartold a progit, általában jó. {0}", ex.Message);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                string conn =
                    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\World.mdf;Integrated Security=True";
                builder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(conn);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Settlement>(entity =>
            {
                entity
                    .HasOne(settlement => settlement.Country)
                    .WithMany(country => country.Settlements)
                    .HasForeignKey(settlement => settlement.CountryID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Citizen>(entity =>
            {
                entity
                    .HasOne(citizen => citizen.Citizenship)
                    .WithMany(country => country.Citizens)
                    .HasForeignKey(citizen => citizen.CitizenshipID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity
                    .HasOne(citizen => citizen.Settlement)
                    .WithMany(settlement => settlement.Citizens)
                    .HasForeignKey(citizen => citizen.SettlementID)
                    .OnDelete(DeleteBehavior.Restrict);

            });

            Country Hungary = new() //9.75M
            {
                CountryID = 1,
                Name = "Hungary",
                IsOECDMember = true,
                TotalGDPInMillionUSD = 155000
            };
            Country Slovakia = new() //5.459M
            {
                CountryID = 2,
                Name = "Slovakia",
                IsOECDMember = true,
                TotalGDPInMillionUSD = 104600
            };
            Country Cameroon = new() //26.55M
            {
                CountryID = 3,
                Name = "Cameroon",
                IsOECDMember = false,
                TotalGDPInMillionUSD = 39800
            };
            Country NewZealand = new() //5.084M
            {
                CountryID = 4,
                Name = "New Zealand",
                IsOECDMember = true,
                TotalGDPInMillionUSD = 212500
            };

            Settlement Budapest = new()
            {
                SettlementID = 1,
                SettlementName = "Budapest",
                CountryID = Hungary.CountryID,
                HDI = 0.925,
                Population = 1756000
            };
            Settlement Debrecen = new()
            {
                SettlementID = 2,
                SettlementName = "Debrecen",
                CountryID = Hungary.CountryID,
                HDI = 0.893,
                Population = 202520
            };
            Settlement Miskolc = new()
            {
                SettlementID = 3,
                SettlementName = "Miskolc",
                CountryID = Hungary.CountryID,
                HDI = 0.833,
                Population = 157639
            };
            Settlement OtherHungarianSettlements = new()
            {
                SettlementID = 4,
                SettlementName = "Other Hungarian Settlements",
                CountryID = Hungary.CountryID,
                HDI = 0.899,
                Population = 7567000
            };
            Settlement Bratislava = new()
            {
                SettlementID = 5,
                SettlementName = "Bratislava",
                CountryID = Slovakia.CountryID,
                HDI = 0.935,
                Population = 424428
            };
            Settlement Kosice = new()
            {
                SettlementID = 6,
                SettlementName = "Košice",
                CountryID = Slovakia.CountryID,
                HDI = 0.873,
                Population = 239171
            };
            Settlement OtherSlovakSettlements = new()
            {
                SettlementID = 7,
                SettlementName = "Other Slovak Settlements",
                CountryID = Slovakia.CountryID,
                HDI = 0.912,
                Population = 4800000
            };
            Settlement Yaounde = new()
            {
                SettlementID = 8,
                SettlementName = "Yaoundé",
                CountryID = Cameroon.CountryID,
                HDI = 0.703,
                Population = 2766000
            };
            Settlement Bamenda = new()
            {
                SettlementID = 9,
                SettlementName = "Bamenda",
                CountryID = Cameroon.CountryID,
                HDI = 0.594,
                Population = 348766
            };
            Settlement OtherCameroonianSettlements = new()
            {
                SettlementID = 10,
                SettlementName = "Other Cameroonian Settlements",
                CountryID = Cameroon.CountryID,
                HDI = 0.545,
                Population = 22900000
            };
            Settlement Wellington = new()
            {
                SettlementID = 11,
                SettlementName = "Wellington",
                CountryID = NewZealand.CountryID,
                HDI = 0.965,
                Population = 215900
            };
            Settlement Auckland = new()
            {
                SettlementID = 12,
                SettlementName = "Auckland",
                CountryID = NewZealand.CountryID,
                HDI = 0.971,
                Population = 1463000
            };
            Settlement Christchurch = new()
            {
                SettlementID = 13,
                SettlementName = "Christchurch",
                CountryID = NewZealand.CountryID,
                HDI = 0.934,
                Population = 380600
            };
            Settlement OtherNewZealandSettlements = new()
            {
                SettlementID = 14,
                SettlementName = "Other Settlements in New Zealand",
                CountryID = NewZealand.CountryID,
                HDI = 0.931,
                Population = 3024500
            };

            Citizen A01 = new()
            {
                PersonID = 1,
                BirthDate = new DateTime(1996, 05, 22),
                CitizenshipID = Hungary.CountryID,
                SettlementID = Budapest.SettlementID,
                HasCriminalRecord = false,
                IncomeInUSD = 2800,
                Name = "Levente Kiss"
            };
            Citizen A02 = new()
            {
                PersonID = 2,
                BirthDate = new DateTime(1976, 01, 22),
                CitizenshipID = Hungary.CountryID,
                SettlementID = Budapest.SettlementID,
                HasCriminalRecord = false,
                IncomeInUSD = 3100,
                Name = "Béla Kovács"
            };

            Citizen A03 = new()
            {
                PersonID = 3,
                BirthDate = new DateTime(1946, 05, 20),
                CitizenshipID = Hungary.CountryID,
                SettlementID = Debrecen.SettlementID,
                HasCriminalRecord = true,
                IncomeInUSD = 2700,
                Name = "Rajmund Nagy"
            };

            Citizen A04 = new()
            {
                PersonID = 4,
                BirthDate = new DateTime(1946, 02, 22),
                CitizenshipID = Hungary.CountryID,
                SettlementID = Debrecen.SettlementID,
                HasCriminalRecord = false,
                IncomeInUSD = 3000,
                Name = "Lajos Takács"
            };

            Citizen A05 = new()
            {
                PersonID = 5,
                BirthDate = new DateTime(1916, 05, 22),
                CitizenshipID = Hungary.CountryID,
                SettlementID = Miskolc.SettlementID,
                HasCriminalRecord = false,
                IncomeInUSD = 1900,
                Name = "János Nagy"
            };

            Citizen A06 = new()
            {
                PersonID = 6,
                BirthDate = new DateTime(1972, 02, 03),
                CitizenshipID = Slovakia.CountryID,
                SettlementID = Miskolc.SettlementID,
                HasCriminalRecord = true,
                IncomeInUSD = 8700,
                Name = "Eva Maďárova"
            };

            Citizen A07 = new()
            {
                PersonID = 7,
                BirthDate = new DateTime(1936, 05, 22),
                CitizenshipID = Hungary.CountryID,
                SettlementID = OtherHungarianSettlements.SettlementID,
                HasCriminalRecord = true,
                IncomeInUSD = 2100,
                Name = "Éva Papp"
            };

            Citizen A08 = new()
            {
                PersonID = 8,
                BirthDate = new DateTime(1996, 02, 22),
                CitizenshipID = Hungary.CountryID,
                SettlementID = OtherHungarianSettlements.SettlementID,
                HasCriminalRecord = false,
                IncomeInUSD = 1800,
                Name = "Béla Takács"
            };

            Citizen A09 = new()
            {
                PersonID = 9,
                BirthDate = new DateTime(1936, 05, 22),
                CitizenshipID = Slovakia.CountryID,
                SettlementID = Bratislava.SettlementID,
                HasCriminalRecord = false,
                IncomeInUSD = 4200,
                Name = "Ján Čapkovič"
            };

            Citizen A10 = new()
            {
                PersonID = 10,
                BirthDate = new DateTime(1976, 05, 22),
                CitizenshipID = Slovakia.CountryID,
                SettlementID = Bratislava.SettlementID,
                HasCriminalRecord = false,
                IncomeInUSD = 3800,
                Name = "Jozef Dancák"
            };

            Citizen A11 = new()
            {
                PersonID = 11,
                BirthDate = new DateTime(1926, 05, 22),
                CitizenshipID = Slovakia.CountryID,
                SettlementID = Kosice.SettlementID,
                HasCriminalRecord = true,
                IncomeInUSD = 3500,
                Name = "Štefan Boška"
            };

            Citizen A12 = new()
            {
                PersonID = 12,
                BirthDate = new DateTime(1929, 05, 22),
                CitizenshipID = Hungary.CountryID,
                SettlementID = Kosice.SettlementID,
                HasCriminalRecord = false,
                IncomeInUSD = 7700,
                Name = "Peter Forgách"
            };

            Citizen A13 = new()
            {
                PersonID = 13,
                BirthDate = new DateTime(1954, 03, 23),
                CitizenshipID = Slovakia.CountryID,
                SettlementID = OtherSlovakSettlements.SettlementID,
                HasCriminalRecord = false,
                IncomeInUSD = 4000,
                Name = "Michal Bartovič"
            };

            Citizen A14 = new()
            {
                PersonID = 14,
                BirthDate = new DateTime(1987, 04, 30),
                CitizenshipID = Slovakia.CountryID,
                SettlementID = OtherSlovakSettlements.SettlementID,
                HasCriminalRecord = true,
                IncomeInUSD = 3700,
                Name = "Pavol Kozáček"
            };

            Citizen A15 = new()
            {
                PersonID = 15,
                BirthDate = new DateTime(1988, 08, 28),
                CitizenshipID = Cameroon.CountryID,
                SettlementID = Yaounde.SettlementID,
                HasCriminalRecord = false,
                IncomeInUSD = 200,
                Name = "František Hamadou"
            };

            Citizen A16 = new()
            {
                PersonID = 16,
                BirthDate = new DateTime(1977, 07, 17),
                CitizenshipID = Cameroon.CountryID,
                SettlementID = Yaounde.SettlementID,
                HasCriminalRecord = true,
                IncomeInUSD = 220,
                Name = "Joseph Bouba"
            };

            Citizen A17 = new()
            {
                PersonID = 17,
                BirthDate = new DateTime(1944, 03, 01),
                CitizenshipID = Cameroon.CountryID,
                SettlementID = Bamenda.SettlementID,
                HasCriminalRecord = false,
                IncomeInUSD = 210,
                Name = "Emmanuel Oumarou"
            };

            Citizen A18 = new()
            {
                PersonID = 18,
                BirthDate = new DateTime(1936, 01, 23),
                CitizenshipID = Cameroon.CountryID,
                SettlementID = Bamenda.SettlementID,
                HasCriminalRecord = true,
                IncomeInUSD = 180,
                Name = "Pierre Moussa"
            };

            Citizen A19 = new()
            {
                PersonID = 19,
                BirthDate = new DateTime(1916, 02, 27),
                CitizenshipID = Cameroon.CountryID,
                SettlementID = OtherCameroonianSettlements.SettlementID,
                HasCriminalRecord = false,
                IncomeInUSD = 190,
                Name = "Paul Mohamadou"
            };

            Citizen A20 = new()
            {
                PersonID = 20,
                BirthDate = new DateTime(1996, 05, 29),
                CitizenshipID = Cameroon.CountryID,
                SettlementID = OtherCameroonianSettlements.SettlementID,
                HasCriminalRecord = false,
                IncomeInUSD = 230,
                Name = "Andre Mahamat"
            };

            Citizen A21 = new()
            {
                PersonID = 21,
                BirthDate = new DateTime(1946, 02, 12),
                CitizenshipID = NewZealand.CountryID,
                SettlementID = Wellington.SettlementID,
                HasCriminalRecord = true,
                IncomeInUSD = 12000,
                Name = "Laura Peterson"
            };

            Citizen A22 = new()
            {
                PersonID = 22,
                BirthDate = new DateTime(1999, 07, 20),
                CitizenshipID = NewZealand.CountryID,
                SettlementID = Wellington.SettlementID,
                HasCriminalRecord = false,
                IncomeInUSD = 20000,
                Name = "Michael Dawnson"
            };

            Citizen A23 = new()
            {
                PersonID = 23,
                BirthDate = new DateTime(2000, 01, 01),
                CitizenshipID = NewZealand.CountryID,
                SettlementID = Auckland.SettlementID,
                HasCriminalRecord = false,
                IncomeInUSD = 17000,
                Name = "Dennis Jackson"
            };

            Citizen A24 = new()
            {
                PersonID = 24,
                BirthDate = new DateTime(1967, 02, 17),
                CitizenshipID = NewZealand.CountryID,
                SettlementID = Auckland.SettlementID,
                HasCriminalRecord = false,
                IncomeInUSD = 15000,
                Name = "Ashley McAdams"
            };

            Citizen A25 = new()
            {
                PersonID = 25,
                BirthDate = new DateTime(1951, 01, 07),
                CitizenshipID = NewZealand.CountryID,
                SettlementID = Auckland.SettlementID,
                HasCriminalRecord = false,
                IncomeInUSD = 23000,
                Name = "Rachel Brown"
            };

            Citizen A26 = new()
            {
                PersonID = 26,
                BirthDate = new DateTime(1944, 02, 27),
                CitizenshipID = NewZealand.CountryID,
                SettlementID = Christchurch.SettlementID,
                HasCriminalRecord = true,
                IncomeInUSD = 20900,
                Name = "Joyce Lauren"
            };

            Citizen A27 = new()
            {
                PersonID = 27,
                BirthDate = new DateTime(1956, 01, 22),
                CitizenshipID = NewZealand.CountryID,
                SettlementID = Christchurch.SettlementID,
                HasCriminalRecord = false,
                IncomeInUSD = 40000,
                Name = "Karen Lewis"
            };

            Citizen A28 = new()
            {
                PersonID = 28,
                BirthDate = new DateTime(1993, 03, 15),
                CitizenshipID = NewZealand.CountryID,
                SettlementID = OtherNewZealandSettlements.SettlementID,
                HasCriminalRecord = false,
                IncomeInUSD = 33000,
                Name = "Rita Gardner"
            };

            Citizen A29 = new()
            {
                PersonID = 29,
                BirthDate = new DateTime(1987, 03, 21),
                CitizenshipID = NewZealand.CountryID,
                SettlementID = OtherNewZealandSettlements.SettlementID,
                HasCriminalRecord = false,
                IncomeInUSD = 29000,
                Name = "Jonas Silverman"
            };


            builder.Entity<Country>().HasData(Hungary, Slovakia, Cameroon, NewZealand);

            builder.Entity<Settlement>().HasData(Budapest, Debrecen, Miskolc, OtherHungarianSettlements, Bratislava, Kosice, OtherSlovakSettlements, Yaounde,
                Bamenda, OtherCameroonianSettlements, Wellington, Auckland, Christchurch, OtherNewZealandSettlements);

            builder.Entity<Citizen>().HasData(A01, A02, A03, A04, A05, A06, A07, A08, A09, A10, A11, A12, A13, A14, A15, A16, A17, A18, A19, A20, A21, A22, A23, A24, A25, A26, A27, A28, A29);

        }
    }
}
