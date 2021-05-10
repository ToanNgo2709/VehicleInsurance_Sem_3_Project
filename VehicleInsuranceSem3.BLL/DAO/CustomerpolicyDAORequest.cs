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
   public class CustomerpolicyDAORequest : ICrudFeature<CustomerpolicyViewModel>
    {
        public InsuranceDbContext context = new InsuranceDbContext();

        public int Add(CustomerpolicyViewModel newItem)
        {
            Customer_Policy customer = new Customer_Policy()
            {
                policy_start_date = newItem.policystartdate,
                policy_end_date = newItem.policyenddate,
                create_date = newItem.createdate,
                customer_add_prove = newItem.customeraddprove,
                active = newItem.active,
            };
            context.Customer_Policy.Add(customer);
            context.SaveChanges();
            return 1;
                

        }

        public void Delete(int id)
        {
            var q = context.Customer_Policy.Where(d => d.id == id).FirstOrDefault();
            if (q!=null)
            {
                context.Customer_Policy.Remove(q);
                context.SaveChanges();
            }
        }

        public List<CustomerpolicyViewModel> GetAll()
        {
            var q = context.Customer_Policy.Select(d => new CustomerpolicyViewModel { id = d.id, active = d.active, createdate = d.create_date, customeraddprove = d.customer_add_prove, policystartdate = d.policy_start_date, policyenddate = d.policy_end_date }).ToList();
            return q;

        }


        public List<CustomerpolicyViewModel> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public CustomerpolicyViewModel GetEdit(int id)
        {
            var q = context.Customer_Policy.Where(d => d.id == id).Select(d => new CustomerpolicyViewModel { id = d.id, active = d.active, createdate = d.create_date, customeraddprove = d.customer_add_prove, policystartdate = d.policy_start_date, policyenddate = d.policy_end_date }).FirstOrDefault();
            return q;

        }


        public List<CustomerpolicyViewModel> Gets(int page, int row)
        {
            var q = context.Customer_Policy.Select(d => new CustomerpolicyViewModel { id = d.id, active = d.active, createdate = d.create_date, customeraddprove = d.customer_add_prove, policystartdate = d.policy_start_date, policyenddate = d.policy_end_date }).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();
            return q;
        }

        public List<CustomerpolicyViewModel> Search(int page, int row, string keyword)
        {
            var CountItem = context.Customer_Policy.Where(d => d.customer_add_prove.ToLower().Contains(keyword.ToLower()) || d.create_date.ToString().Contains(keyword.ToString()) || d.policy_start_date.ToString().Contains(keyword.ToString()) || d.policy_end_date.ToString().Contains(keyword.ToString())).Count();
            var totalPage = CountItem / row;
            totalPage += (CountItem % row > 0 ? 1 : 0);
            HttpContext Context = HttpContext.Current;
            Context.Session["CountItemCustomerPolicy"] = CountItem;
            Context.Session["totalPage"] = totalPage;
            var q = context.Customer_Policy.Where(d => d.customer_add_prove.ToLower().Contains(keyword.ToLower()) || d.create_date.ToString().Contains(keyword.ToString()) || d.policy_start_date.ToString().Contains(keyword.ToString()) || d.policy_end_date.ToString().Contains(keyword.ToString())).Select(d => new CustomerpolicyViewModel
            {
                id = d.id,
                active = d.active,
                createdate = d.create_date,
                policystartdate = d.policy_start_date,
                policyenddate = d.policy_end_date,
                customeraddprove = d.customer_add_prove

            }).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();

            return q;
        }

        public int Update(CustomerpolicyViewModel updateItems)
        {
            try
            {
                var q = context.Customer_Policy.Where(d => d.id == updateItems.id).FirstOrDefault();
                q.create_date = updateItems.createdate;
                q.active = updateItems.active;
                q.policy_start_date = updateItems.policystartdate;
                q.policy_end_date = updateItems.policyenddate;
                q.customer_add_prove = updateItems.customeraddprove;
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
