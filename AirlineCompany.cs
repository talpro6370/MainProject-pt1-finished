using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject
{
   public class AirlineCompany:IPoco,IUser
    {
        public long ID { get; set; }
        public String AIRLINE_NAME { get; set; }
        public string USER_NAME { get; set; }
        public string PASSWORD { get; set; }
        public long COUNTRY_CODE { get; set; }
        public AirlineCompany()
        {

        }
        public AirlineCompany(string aIRLINE_NAME, string uSER_NAME, string pASSWORD, long cOUNTRY_CODE)
        {
            AIRLINE_NAME = aIRLINE_NAME;
            USER_NAME = uSER_NAME;
            PASSWORD = pASSWORD;
            COUNTRY_CODE = cOUNTRY_CODE;
        }

        public override bool Equals(object obj)
        {
            var company = obj as AirlineCompany;
            return company != null &&
                   AIRLINE_NAME == company.AIRLINE_NAME &&
                   USER_NAME == company.USER_NAME &&
                   PASSWORD == company.PASSWORD &&
                   COUNTRY_CODE == company.COUNTRY_CODE;
        }
        public override int GetHashCode()
        {
            return (int)this.ID;
        }
        public static bool operator ==(AirlineCompany a1, AirlineCompany a2)
        {
            if (ReferenceEquals(a1,null)&& ReferenceEquals(a2, null))
            {
                return true;
            }
            if (ReferenceEquals(a1, null) || ReferenceEquals(a2, null))
            {
                return false;
            }
            if (a1.AIRLINE_NAME == a2.AIRLINE_NAME && a1.PASSWORD == a2.PASSWORD && a1.USER_NAME == a2.USER_NAME && a1.COUNTRY_CODE == a2.COUNTRY_CODE)
            {
                return true;
            }
            return false;
        }
        public static bool operator !=(AirlineCompany a1, AirlineCompany a2)
        {
            return !(a1 == a2);
        }
    }
}
