﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiClient.Model
{
    public class Citizen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonID { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public bool HasCriminalRecord { get; set; }

        public int IncomeInUSD { get; set; }

        [NotMapped]
        //[JsonIgnore]
        public virtual Country Citizenship { get; set; }

        [ForeignKey(nameof(Country))]
        public int CitizenshipID { get; set; }

        //[JsonIgnore]
        public virtual Settlement Settlement { get; set; }

        [ForeignKey(nameof(Settlement))]
        public int SettlementID { get; set; }

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode();
        //}

        ////this is needed for unit tests
        //public override bool Equals(object obj)
        //{
        //    return this.PersonID == PersonID;
        //}
        //public override string ToString()
        //{
        //    return "---- DETAILS ----\n\tName: " + Name + "\n\tID: " + PersonID + "\n\tBorn " + BirthDate + "\n\tHas a citizenship of " + Citizenship.Name + "\n\tLives in " + Settlement.SettlementName +
        //        "\n\tIncome is " + IncomeInUSD + " USD" + "\n\tCriminal record: " + HasCriminalRecord + "\n---- DETAILS ----";
        //    //return "";
        //}
    }
}
