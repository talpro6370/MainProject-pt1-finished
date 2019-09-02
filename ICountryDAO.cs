namespace MainProject.DAO
{
    public interface ICountryDAO:IBasicDB<Country>
    {
        long GetCountryId(string countryName);
    }
}