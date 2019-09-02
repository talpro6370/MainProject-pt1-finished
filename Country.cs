using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject
{
    public class Country : IPoco
    {
        public long ID { get; set; }
        public string COUNTRY_NAME { get; set; }
        public Country()
        {

        }

        public Country(string cOUNTRY_NAME)
        {
            COUNTRY_NAME = cOUNTRY_NAME;
        }

        public override bool Equals(object obj)
        {
            var countries = obj as Country;
            return countries != null &&
                   COUNTRY_NAME == countries.COUNTRY_NAME;
        }

        public override int GetHashCode()
        {
            return (int)this.ID;
        }
        public static bool operator ==(Country c1, Country c2)
        {
            if (ReferenceEquals(c1, null) && ReferenceEquals(c2, null))
            {
                return true;
            }
            if (ReferenceEquals(c1, null) || ReferenceEquals(c2, null))
            {
                return false;
            }
            if (c1.COUNTRY_NAME==c2.COUNTRY_NAME)
            {
                return true;
            }
            return false;
        }
        public static bool operator !=(Country c1, Country c2)
        {
            return !(c1 == c2);
        }
    }
}
