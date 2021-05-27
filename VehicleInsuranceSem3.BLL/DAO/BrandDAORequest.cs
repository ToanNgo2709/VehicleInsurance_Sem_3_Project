using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using VehicleInsuranceSem3.BLL.Repository;
using VehicleInsuranceSem3.BLL.ViewModel;
using VehicleInsuranceSem3.DAL.Model;

namespace VehicleInsuranceSem3.BLL.DAO
{
    public class BrandDAORequest :  ICrudFeature<BrandViewModel>
    {

        public InsuranceDbContext context = new InsuranceDbContext();
 
        public int Add(BrandViewModel newItem)
        {
            Brand newBrand = new Brand()
            {
                id = newItem.Id,

                name = newItem.Name,
                active = newItem.Active
            };

            context.Brands.Add(newBrand);
            context.SaveChanges();
            return 1;
        }

        public void Delete(int id)
        {
            var q = context.Brands.Where(d => d.id == id).FirstOrDefault();
            if (q != null)
            {
                context.Brands.Remove(q);
                context.SaveChanges();

            }

           
        }
        public List<BrandViewModel> Search(int page,int row ,string keyword)
        {
            var CountItem = context.Brands.Where(d => d.name.ToLower().Contains(keyword.ToLower())).Count();
            var totalPage = CountItem / row;
            totalPage += (CountItem % row > 0 ? 1 : 0);
            HttpContext Context = HttpContext.Current;
            Context.Session["CountItemBrand"] = CountItem;
            Context.Session["TotalPage"] = totalPage;
            var q = context.Brands.Where(d => d.name.ToLower().Contains(keyword.ToLower())).Select(d => new BrandViewModel { Id = d.id, Name = d.name, Active = (bool)d.active }).OrderBy(d => d.Id).Skip((page - 1) * row).Take(row).ToList();
            return q ;
        }

       

        public List<BrandViewModel> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Update( BrandViewModel updateItems)
        {
            try
            {

                var q = context.Brands.Where(d => d.id == updateItems.Id).FirstOrDefault();
                q.name = updateItems.Name;
                q.active = updateItems.Active;

                context.SaveChanges();
                return 1;


            }
            catch (EntityException ex)
            {

                Debug.WriteLine(ex.Message);
                return 0;
            }
        }

        public BrandViewModel GetEdit(int id)
        {
            var q = context.Brands.Where(d => d.id == id).Select(c => new BrandViewModel { Id = c.id, Name = c.name , Active = (bool)c.active}).FirstOrDefault();
            return q;
        }

        public List<BrandViewModel> Gets(int page, int row)
        {
            var q = context.Brands.Select(d => new BrandViewModel { Id = d.id, Name = d.name ,Active= (bool)d.active}).OrderBy(d => d.Id).Skip((page - 1) * row).Take(row).ToList();
            return q;
        }

        public List<BrandViewModel> GetAll()
        {
            var q =  context.Brands.Select(d => new BrandViewModel { Id = d.id, Name = d.name, Active = (bool)d.active  }).ToList();
            return q;

        }
    }
}
