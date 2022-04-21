using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace EFCUTY_HFT_2021221.Models
{
    [Table("Countries")]
    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryID { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Settlement> Settlements { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Citizen> Citizens { get; set; }

        public string Name { get; set; }

        public int TotalGDPInMillionUSD { get; set; }

        public bool IsOECDMember { get; set; }

        public Country()
        {
            Settlements = new HashSet<Settlement>();
            Citizens = new HashSet<Citizen>();
        }

        //public override int GetHashCode()
        //{
        //    return this.Name.GetHashCode() * this.CountryID.GetHashCode() + this.TotalGDPInMillionUSD.GetHashCode();
        //}
        /*
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Country p = (Country)obj;
                return (p.Name.Equals(this.Name));
            }
        }
        */
        //public override string ToString()
        //{
        //    return "---- DETAILS ----\n\tName: " + Name + "\n\tID: " + CountryID + "\n\tTotal yearly GDP in million USD: " + TotalGDPInMillionUSD + "\n\tLogical value of it being an OECD member: " + IsOECDMember + "\n---- DETAILS ----";
        //}
    }
}
