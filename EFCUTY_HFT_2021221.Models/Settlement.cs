using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EFCUTY_HFT_2021221.Models
{
    [Table("Settlements")]
    public class Settlement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SettlementID { get; set; }

        public string SettlementName { get; set; }

        public int Population { get; set; }

        public double HDI { get; set; }

        [NotMapped]
        //[JsonIgnore]
        public virtual Country Country { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Citizen> Citizens { get; set; }

        [ForeignKey(nameof(Country))]
        public int CountryID { get; set; }

        //public override int GetHashCode()
        //{
        //    return this.HDI.GetHashCode() * this.SettlementName.GetHashCode() + this.HDI.GetHashCode() * this.Population.GetHashCode();
        //}

        //public override bool Equals(object obj)
        //{
        //    return this.GetHashCode().Equals(obj.GetHashCode());
        //}

        //public override string ToString()
        //{
        //    return "---- DETAILS ----\n\tName, Country: " + SettlementName + ", " + Country.Name + "\n\tID:" + SettlementID + "\n\tPopulation: " + Population + "\n\tHDI: " + HDI + "\n---- DETAILS ----";
        //}
    }
}
