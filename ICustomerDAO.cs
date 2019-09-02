using MainProject.DAO;

namespace MainProject
{
    public interface ICustomerDAO: IBasicDB<Customer>
    {
        Customer GetCustomerByUserame(string name);
    }
}