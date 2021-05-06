using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInsuranceSem3.BLL.Repository;
using VehicleInsuranceSem3.BLL.ViewModel;
using VehicleInsuranceSem3.DAL.Model;

namespace VehicleInsuranceSem3.BLL.DAO
{
    public class BrandDAORequest : ICrudFeature<BrandViewModel>
    {

        public InsuranceDbContext context = new InsuranceDbContext();

        public int Add(BrandViewModel newItem)
        {
            Brand newBrand = new Brand()
            {
                name = newItem.Name,
                active = newItem.Active
            };

            context.Brands.Add(newBrand);

            return 1;
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
