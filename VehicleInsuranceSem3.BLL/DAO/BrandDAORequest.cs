using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInsuranceSem3.BLL.Repository;
using VehicleInsuranceSem3.BLL.ViewModel;

namespace VehicleInsuranceSem3.BLL.DAO
{
    public class BrandDAORequest : ICrudFeature<BrandViewModel>
    {
        public int Add(BrandViewModel newItem)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<BrandViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<BrandViewModel> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public int Update(int id, BrandViewModel updateItems)
        {
            throw new NotImplementedException();
        }
    }
}
