using EFCUTY_HFT_2021221.Logic;
using EFCUTY_HFT_2021221.Models;
using EFCUTY_HFT_2021221.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WorldDb.Test
{
    [TestFixture]
    public class Tester
    {

        SettlementLogic sl;
        CitizenLogic cl;
        CountryLogic cyl;

        [SetUp]
        public void Init()
        {
            var mockCountryRepository =
                new Mock<ICountryRepository>();

            var mockSettlementRepository =
                new Mock<ISettlementRepository>();

            var mockCitizenRepository =
                new Mock<ICitizenRepository>();

            var fakeCountries = new List<Country>()
            {
                new Country()
                {
                    CountryID = 1,
                    Name = "Hamisország",
                    IsOECDMember = false,
                    TotalGDPInMillionUSD = 6999,
                    Settlements = new List<Settlement>()
                {
                    new Settlement()
                    {
                        SettlementID = 1,
                        CountryID = 1,
                        SettlementName = "Az első hamiskás település",
                        HDI = 0.944,
                        Population = 1000000
                    },
                    new Settlement()
                    {
                        SettlementID = 2,
                        CountryID = 1,
                        SettlementName = "A második hamiskás település",
                        HDI = 0.844,
                        Population = 700000
                    }

                }
                },

                new Country
                {
                    CountryID = 2,
                    Name = "Huncutország",
                    IsOECDMember = true,
                    TotalGDPInMillionUSD = 900000,
                    Settlements = new List<Settlement>()
                {
                new Settlement()
                {
                    SettlementID = 3,
                    CountryID = 2,
                    SettlementName = "Az első huncut település",
                    HDI = 0.934,
                    Population = 40000
                },
                new Settlement()
                {
                    SettlementID = 4,
                    CountryID = 2,
                    SettlementName = "A második huncut település",
                    HDI = 0.3,
                    Population = 540000
                },
                new Settlement()
                {
                    HDI = 0.84,
                    SettlementID = 5,
                    Population = 221230,
                    CountryID = 2,
                    SettlementName = "Ártatlan település"
                }

                }
                }
            }.AsQueryable();

            Country fakeCountry1 = new()
            {
                CountryID = 1,
                Name = "Hamisország",
                IsOECDMember = false,
                TotalGDPInMillionUSD = 6999,
                Settlements = new List<Settlement>()
                {
                    new Settlement()
                    {
                        SettlementID = 1,
                        CountryID = 1,
                        SettlementName = "Az első hamiskás település",
                        HDI = 0.944,
                        Population = 1000000
                    },
                    new Settlement()
                    {
                        SettlementID = 2,
                        CountryID = 1,
                        SettlementName = "A második hamiskás település",
                        HDI = 0.844,
                        Population = 700000
                    }

                }
            };

            Country fakeCountry2 = new()
            {
                CountryID = 2,
                Name = "Huncutország",
                IsOECDMember = true,
                TotalGDPInMillionUSD = 900000,
                Settlements = new List<Settlement>()
                {
                new Settlement()
                {
                    SettlementID = 3,
                    CountryID = 2,
                    SettlementName = "Az első huncut település",
                    HDI = 0.934,
                    Population = 40000
                },
                new Settlement()
                {
                    SettlementID = 4,
                    CountryID = 2,
                    SettlementName = "A második huncut település",
                    HDI = 0.3,
                    Population = 540000
                },
                new Settlement()
                {
                    HDI = 0.84,
                    SettlementID = 5,
                    Population = 221230,
                    CountryID = 2,
                    SettlementName = "Ártatlan település"
                }

                }
            };

            Settlement developedSettlement = new()
            {
                HDI = 0.94,
                SettlementID = 1,
                Population = 911220,
                Country = fakeCountry1,
                SettlementName = "Fejlett település"
            };

            Settlement undevelopedSettlement = new()
            {
                HDI = 0.24,
                SettlementID = 2,
                Population = 222220,
                Country = fakeCountry2,
                SettlementName = "Fejletlen település"
            };
            Settlement innocentSettlement = new()
            {
                HDI = 0.84,
                SettlementID = 3,
                Population = 221230,
                SettlementName = "Ártatlan település",
                Citizens = new List<Citizen>()
                {
                    new Citizen()
                    {
                        PersonID = 8,
                        Name = "Nyolcadik Nyafika",
                        BirthDate = new DateTime(1955,05,05),
                        HasCriminalRecord = false,
                        IncomeInUSD = 23423
                    }
                }

            };

            var fakeSettlements = new List<Settlement>()
            {
                new Settlement()
                {
                    SettlementID = 1,
                    Country = fakeCountry1,
                    SettlementName = "Az első fejlett település",
                    HDI = 0.944,
                    Population = 1000000,
                    Citizens = new List<Citizen>()
                    {
                        new Citizen()
                        {
                            PersonID = 4,
                            Name = "Negyedik Nóra",
                            BirthDate = new DateTime(1914, 10, 12),
                            Citizenship = fakeCountry2,
                            Settlement = developedSettlement,
                            HasCriminalRecord = true,
                            IncomeInUSD = 41000
                        }
                    }
                },
                new Settlement()
                {
                    SettlementID = 2,
                    Country = fakeCountry2,
                    SettlementName = "A második fejletlen település",
                    HDI = 0.3,
                    Population = 540000,
                    Citizens = new List<Citizen>()
                    {
                        new Citizen()
                        {
                            PersonID = 1,
                            Name = "Első Előd",
                            BirthDate = new DateTime(1984, 10, 12),
                            Citizenship = fakeCountry1,
                            Settlement = undevelopedSettlement,
                            HasCriminalRecord = false,
                            IncomeInUSD = 50000
                        },
                        new Citizen()
                        {
                            PersonID = 2,
                            Name = "Második Márton",
                            BirthDate = new DateTime(1954, 10, 12),
                            Citizenship = fakeCountry1,
                            Settlement = undevelopedSettlement,
                            HasCriminalRecord = false,
                            IncomeInUSD = 50000
                        },
                        new Citizen()
                        {
                            PersonID = 3,
                            Name = "Harmadik Huba",
                            BirthDate = new DateTime(1984, 10, 12),
                            Citizenship = fakeCountry1,
                            Settlement = undevelopedSettlement,
                            HasCriminalRecord = true,
                            IncomeInUSD = 50000
                        },
                        new Citizen()
                        {
                            PersonID = 1,
                            Name = "Első Előd",
                            BirthDate = new DateTime(1984, 10, 12),
                            Citizenship = fakeCountry1,
                            Settlement = undevelopedSettlement,
                            HasCriminalRecord = false,
                            IncomeInUSD = 50000
                        },
                        new Citizen()
                        {
                            PersonID = 2,
                            Name = "Második Márton",
                            BirthDate = new DateTime(1954, 10, 12),
                            Citizenship = fakeCountry1,
                            Settlement = undevelopedSettlement,
                            HasCriminalRecord = false,
                            IncomeInUSD = 50000
                        },
                        new Citizen()
                        {
                            PersonID = 3,
                            Name = "Harmadik Huba",
                            BirthDate = new DateTime(1984, 10, 12),
                            Citizenship = fakeCountry1,
                            Settlement = undevelopedSettlement,
                            HasCriminalRecord = true,
                            IncomeInUSD = 50000
                        }
                    }
                },
                new Settlement()
                {
                    HDI = 0.84,
                    SettlementID = 3,
                    Population = 221230,
                    SettlementName = "Ártatlan település",
                    Citizens = new List<Citizen>()
                    {
                        new Citizen()
                        {
                            PersonID = 8,
                            Name = "Nyolcadik Nyafika",
                            BirthDate = new DateTime(1955,05,05),
                            HasCriminalRecord = false,
                            IncomeInUSD = 23423
                        }
                    }
                }
            }.AsQueryable();

            var fakeCitizens = new List<Citizen>()
            {
                new Citizen()
                {
                    PersonID = 1,
                    Name = "Első Előd",
                    BirthDate = new DateTime(1984, 10, 12),
                    Citizenship = fakeCountry1,
                    Settlement = undevelopedSettlement,
                    HasCriminalRecord = false,
                    IncomeInUSD = 50000
                },
                new Citizen()
                {
                    PersonID = 2,
                    Name = "Második Márton",
                    BirthDate = new DateTime(1954, 10, 12),
                    Citizenship = fakeCountry1,
                    Settlement = undevelopedSettlement,
                    HasCriminalRecord = false,
                    IncomeInUSD = 50000
                },
                new Citizen()
                {
                    PersonID = 3,
                    Name = "Harmadik Huba",
                    BirthDate = new DateTime(1984, 10, 12),
                    Citizenship = fakeCountry1,
                    Settlement = undevelopedSettlement,
                    HasCriminalRecord = true,
                    IncomeInUSD = 50000
                },
                new Citizen()
                {
                    PersonID = 4,
                    Name = "Negyedik Nóra",
                    BirthDate = new DateTime(1914, 10, 12),
                    Citizenship = fakeCountry2,
                    Settlement = developedSettlement,
                    HasCriminalRecord = true,
                    IncomeInUSD = 41000
                },
                new Citizen()
                {
                    PersonID = 5,
                    Name = "Ötödik Ödön",
                    BirthDate = new DateTime(1984, 03, 12),
                    Citizenship = fakeCountry1,
                    Settlement = undevelopedSettlement,
                    HasCriminalRecord = false,
                    IncomeInUSD = 30700
                },
                new Citizen()
                {
                    PersonID = 6,
                    Name = "Hatodik Heléna",
                    BirthDate = new DateTime(1938, 11, 20),
                    Citizenship = fakeCountry1,
                    Settlement = undevelopedSettlement,
                    HasCriminalRecord = false,
                    IncomeInUSD = 51000
                },
                new Citizen()
                {
                    PersonID = 7,
                    Name = "Hetedik Hedvig",
                    BirthDate = new DateTime(1935, 11, 20),
                    Citizenship = fakeCountry1,
                    Settlement = undevelopedSettlement,
                    HasCriminalRecord = false,
                    IncomeInUSD = 50000
                },
                new Citizen()
                {
                    PersonID = 8,
                    Name = "Nyolcadik Nyafika",
                    BirthDate = new DateTime(1955,05,05),
                    Citizenship = fakeCountry1,
                    Settlement = innocentSettlement,
                    HasCriminalRecord = false,
                    IncomeInUSD = 23423
                }

            }.AsQueryable();

            mockSettlementRepository.Setup((t) => t.ReadAll())
                .Returns(fakeSettlements);

            mockCitizenRepository.Setup((t) => t.ReadAll())
                .Returns(fakeCitizens);

            mockCountryRepository.Setup((t) => t.ReadAll())
                .Returns(fakeCountries);

            sl = new SettlementLogic(mockSettlementRepository.Object);
            cl = new CitizenLogic(mockCitizenRepository.Object);
            cyl = new CountryLogic(mockCountryRepository.Object);

        }

        [Test]
        public void PoorOldPeopleTest()
        {
            var result = cl.PoorOldPeople().ToList();
            var expected = new List<Citizen>()
            {
                new Citizen()
                {
                    PersonID = 6,
                    Name = "Hatodik Heléna",
                    BirthDate = new DateTime(1938, 11, 20),
                    CitizenshipID = 1,
                    SettlementID = 1,
                    HasCriminalRecord = false,
                    IncomeInUSD = 51000
                },
                new Citizen()
                {
                    PersonID = 7,
                    Name = "Hetedik Hedvig",
                    BirthDate = new DateTime(1935, 11, 20),
                    CitizenshipID = 1,
                    SettlementID = 3,
                    HasCriminalRecord = false,
                    IncomeInUSD = 50000
                }
            };


            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public void PopulationTest()
        {
            var expected = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("Hamisország", 1700000),
                new KeyValuePair<string, int>("Huncutország",801230)
            };
            var result = cyl.Population();

            Assert.That(expected, Is.EqualTo(result));

        }

        [Test]
        public void CriminalTest()
        {
            var result = cl.DevelopedCriminals().ToList();

            var expected = new List<Citizen>()
            {
                new Citizen()
                {
                    PersonID = 4,
                    Name = "Negyedik Nóra",
                    BirthDate = new DateTime(1914, 10, 12),
                    CitizenshipID = 2,
                    SettlementID = 3,
                    HasCriminalRecord = true,
                    IncomeInUSD = 41000
                }
            };


            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public void GoodSettlementsTest()
        {
            List<Settlement> expected = new()
            {
                new Settlement()
                {
                    HDI = 0.84,
                    SettlementID = 3,
                    Population = 221230,
                    SettlementName = "Ártatlan település",
                    Citizens = new List<Citizen>()
                    {
                        new Citizen()
                        {
                            PersonID = 8,
                            Name = "Nyolcadik Nyafika",
                            BirthDate = new DateTime(1955,05,05),
                            HasCriminalRecord = false,
                            IncomeInUSD = 23423
                        }
                    }
                }
            };

            var result = sl.GoodSettlements();
            Assert.That(expected, Is.EqualTo(result));
            ;
        }

        [Test]
        public void CreateTooPoorCountryTest()
        {
            Country newcountry = new()
            {
                CountryID = 1,
                IsOECDMember = true,
                Name = "túl szegény új ország",
                TotalGDPInMillionUSD = 10
            };

            try
            {
                cyl.Create(newcountry);
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.Pass(ex.Message);
            }
        }

        [Test]
        public void CreateNormalCountryTest()
        {
            Country newcountry = new()
            {
                CountryID = 1,
                IsOECDMember = false,
                Name = "egy átlagos új ország",
                TotalGDPInMillionUSD = 10000
            };

            try
            {
                cyl.Create(newcountry);
                Assert.Pass();
            }
            catch (ArgumentException ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void OldCitizenCreateTest()
        {
            Citizen deadcitizen = new()
            {
                BirthDate = new DateTime(1828, 01, 01),
                Name = "Halott Ember",
                PersonID = 1
            };

            try
            {
                cl.Create(deadcitizen);
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.Pass(ex.Message);
            }
        }

        [Test]
        public void TypicalCitizenCreateTest()
        {
            Citizen typicalcitizen = new()
            {
                BirthDate = new DateTime(1928, 01, 01),
                Name = "Élő Ember",
                PersonID = 1
            };

            try
            {
                cl.Create(typicalcitizen);
                Assert.Pass();
            }
            catch (ArgumentException ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void TooDevelopedSettlementCreateTest()
        {
            Settlement supersettlement = new()
            {
                SettlementID = 1,
                HDI = 1.01,
                SettlementName = "A szuperváros",
                Population = 1293
            };

            try
            {
                sl.Create(supersettlement);
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.Pass(ex.Message);
            }
        }

        [Test]
        public void UnpopulatedSettlementCreateTest()
        {
            Settlement emptysettlement = new()
            {
                SettlementID = 1,
                HDI = 0.81,
                SettlementName = "A kihalt település",
                Population = 0
            };

            try
            {
                sl.Create(emptysettlement);
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.Pass(ex.Message);
            }
        }

        [Test]
        public void TypicalSettlementCreateTest()
        {
            Settlement emptysettlement = new()
            {
                SettlementID = 1,
                HDI = 0.81,
                SettlementName = "Település",
                Population = 124123
            };

            try
            {
                sl.Create(emptysettlement);
                Assert.Pass();
            }
            catch (ArgumentException ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
