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
        int Update(Type updateItems);
        void Delete(int id);
        List<Type> Search(int page, int row, string keyword);
        List<Type> GetAll();
        List<Type> GetById(int Id);
        Type GetEdit(int id);
        List<Type> Gets(int page, int row);


    }
}
