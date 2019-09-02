using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.DAO
{
    public interface IBasicDB<T> where T:IPoco
    {
        T Get(int id);
        List<T> GetAll();
        void Add(T t);
        void Remove(T t);
        void Update(T t);
    }
}
