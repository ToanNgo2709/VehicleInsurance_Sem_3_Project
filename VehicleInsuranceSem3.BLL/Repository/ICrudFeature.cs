using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.BLL.Repository
{
    public interface ICrudFeature<Type>
    {
        int Add(Type newItem);
        int Update(int id, Type updateItems);
        int Delete(int id);

        List<Type> GetAll();
        List<Type> GetById(int Id);


    }
}
