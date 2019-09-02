using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject
{
    public class Customer : IPoco, IUser
    {
        public long ID { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string USER_NAME { get; set; }
        public string PASSWORD { get; set; }
        public string ADDRESS { get; set; }
        public string PHONE_NO { get; set; }
        public string CREDIT_CARD_NUMBER { get; set; }
        public Customer()
        {

        }

        public Customer(string fIRST_NAME, string lAST_NAME, string uSER_NAME, string pASSWORD, string aDDRESS, string pHONE_NO, string cREDIT_CARD_NUMBER)
        {
            FIRST_NAME = fIRST_NAME;
            LAST_NAME = lAST_NAME;
            USER_NAME = uSER_NAME;
            PASSWORD = pASSWORD;
            ADDRESS = aDDRESS;
            PHONE_NO = pHONE_NO;
            CREDIT_CARD_NUMBER = cREDIT_CARD_NUMBER;
        }

        public override bool Equals(object obj)
        {
            var customer = obj as Customer;
            return customer != null &&
                   FIRST_NAME == customer.FIRST_NAME &&
                   LAST_NAME == customer.LAST_NAME &&
                   USER_NAME == customer.USER_NAME &&
                   PASSWORD == customer.PASSWORD &&
                   ADDRESS == customer.ADDRESS &&
                   PHONE_NO == customer.PHONE_NO &&
                   CREDIT_CARD_NUMBER == customer.CREDIT_CARD_NUMBER;
        }

        public override int GetHashCode()
        {
            return (int)this.ID;
        }
        public static bool operator ==(Customer c1, Customer c2)
        {
            if (ReferenceEquals(c1, null) && ReferenceEquals(c2, null))
            {
                return true;
            }
            if (ReferenceEquals(c1, null) || ReferenceEquals(c2, null))
            {
                return false;
            }
            if (c1.FIRST_NAME==c2.FIRST_NAME&&c1.LAST_NAME==c2.LAST_NAME&&c1.USER_NAME==c2.USER_NAME&&c1.PHONE_NO==c2.PHONE_NO&&c1.PASSWORD==c2.PASSWORD)
            {
                return true;
            }
            return false;
        }
        public static bool operator !=(Customer c1, Customer c2)
        {
            return !(c1 == c2);
        }

    }
}
