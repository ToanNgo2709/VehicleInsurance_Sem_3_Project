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
    public class UserTypeDAORequest : ICrudFeature<UsertypeViewModel>
    {
        public InsuranceDbContext context = new InsuranceDbContext();


        public int Add(UsertypeViewModel newItem)
        {
            User_Type user = new User_Type()
            {
                id = newItem.id,
                name = newItem.name,
                active = newItem.active,
            };
            context.User_Type.Add(user);
            context.SaveChanges();
            return 1;
        }

        public void Delete(int id)
        {
            var q = context.User_Type.Where(d => d.id == id).FirstOrDefault();
            if (q!=null)
            {
                context.User_Type.Remove(q);
                context.SaveChanges();

            }

        }

        public List<UsertypeViewModel> GetAll()
        {
            var q = context.User_Type.Select(d => new UsertypeViewModel { id = d.id, name = d.name, active = d.active }).ToList();
            return q;
        }

        public List<UsertypeViewModel> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public UsertypeViewModel GetEdit(int id)
        {
            var q = context.User_Type.Where(d => d.id == id).Select(d => new UsertypeViewModel { id = d.id, name = d.name, active = d.active }).FirstOrDefault();
            return q;
                
        }

        public List<UsertypeViewModel> Gets(int page, int row)
        {
            var q = context.User_Type.Select(d => new UsertypeViewModel { id = d.id, name = d.name, active = d.active }).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();
            return q;
        }

        public List<UsertypeViewModel> Search(int page, int row, string keyword)
        {
            var CountItem = context.User_Type.Where(d => d.name.ToLower().Contains(keyword.ToLower())).Count();
            var totalPage = CountItem / row;
            totalPage += (CountItem % row > 0 ? 1 : 0);
            HttpContext Context = HttpContext.Current;
            Context.Session["CountItemUserType"] = CountItem;
            Context.Session["totalPage"] = totalPage;
            var q = context.User_Type.Where(d => d.name.ToLower().Contains(keyword.ToLower())).Select(d => new UsertypeViewModel { id = d.id, name = d.name, active = d.active }).OrderBy(d => d.id).Skip((page - 1) * row).ToList();
            return q;

        }

        public int Update(UsertypeViewModel updateItems)
        {
            try
            {
                var q = context.User_Type.Where(d => d.id == updateItems.id).FirstOrDefault();
             

                q.name = updateItems.name;
                q.active = updateItems.active;
                context.SaveChanges();
                return 1;
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
                return 0;
            }

        }
    }
}
