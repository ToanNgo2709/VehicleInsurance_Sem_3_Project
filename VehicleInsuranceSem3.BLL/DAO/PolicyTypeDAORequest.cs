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

namespace VehicleInsuranceSem3.BLL.ViewModel
{
   public class PolicyTypeDAORequest : ICrudFeature<PolicytypeViewModel>
    {
        public InsuranceDbContext context = new InsuranceDbContext();

        public int Add(PolicytypeViewModel newItem)
        {
            Policy_Type policy = new Policy_Type()
            {
                name = newItem.name,
                active = newItem.active,
                liability_level = newItem.libilityLevel,
                price = newItem.price,
                id = newItem.id
               
            };
            context.Policy_Type.Add(policy);
            context.SaveChanges();
            return 1;

        }

        public void Delete(int id)
        {
            var q = context.Policy_Type.Where(d => d.id == id).FirstOrDefault();
            if (q!=null)
            {
                context.Policy_Type.Remove(q);
                context.SaveChanges();
            }

        }

        public List<PolicytypeViewModel> GetAll()
        {
            var q = context.Policy_Type.Select(d => new PolicytypeViewModel { id = d.id , name = d.name , active =d.active , price = d.price , libilityLevel = d.liability_level }).ToList();
            return q;

        }

        public List<PolicytypeViewModel> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public PolicytypeViewModel GetTypeById(int Id)
        {
            var q = context.Policy_Type.Where(p => p.id == Id)
                .Select(d => new PolicytypeViewModel { id = d.id, name = d.name, active = d.active, price = d.price, libilityLevel = d.liability_level })
                .FirstOrDefault();
            return q;
        }

        public PolicytypeViewModel GetEdit(int id)
        {
            var q = context.Policy_Type.Where(d => d.id == id).Select(d => new PolicytypeViewModel { id = d.id, name = d.name, active = d.active  , price = d.price , libilityLevel = d.liability_level}).FirstOrDefault();
            return q;
        }


        public List<PolicytypeViewModel> Gets(int page, int row)
        {
            var q = context.Policy_Type.Select(d => new PolicytypeViewModel { id = d.id, name = d.name, active = d.active  , libilityLevel = d.liability_level , price = d.price}).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();
            return q;
        }

        public List<PolicytypeViewModel> Search(int page, int row, string keyword)
        {
            var CountItem = context.Policy_Type.Where(d => d.name.ToLower().Contains(keyword.ToLower())|| d.price.ToString().Contains(keyword.ToString())).Count();
            var totalPage = CountItem / row;
            totalPage += (CountItem % row > 0 ? 1 : 0);
            HttpContext Context = HttpContext.Current;
            Context.Session["CountItemPolicyType"] = CountItem;
            Context.Session["totalPage"] = totalPage;
            var q = context.Policy_Type.Where(d => d.name.ToLower().Contains(keyword.ToLower())).Select(d => new PolicytypeViewModel { id = d.id, name = d.name, active = d.active , price = d.price , libilityLevel = d.liability_level }).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();
            return q;

        }

        public int Update(PolicytypeViewModel updateItems)
        {
            try
            {
                var q = context.Policy_Type.Where(d => d.id == updateItems.id).FirstOrDefault();
                q.name = updateItems.name;
                q.active = updateItems.active;
                q.price = updateItems.price;
                q.liability_level = updateItems.libilityLevel;
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
