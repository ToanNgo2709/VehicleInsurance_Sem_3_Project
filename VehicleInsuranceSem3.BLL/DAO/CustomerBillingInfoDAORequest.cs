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
    public class CustomerBillingInfoDAORequest : ICrudFeature<CustomerbillinginfoViewModel>
    {
        public InsuranceDbContext context = new InsuranceDbContext();

        public int Add(CustomerbillinginfoViewModel newItem)
        {
            Customer_Billing_Info customer = new Customer_Billing_Info()
            {
                customer_add_prove = newItem.customeraddprove,
                bill_number = newItem.bill_number,
                create_date = newItem.createdate,
                amount = newItem.amount,
                active = newItem.active, customer_policy_id = newItem.customerpolicyid,
               id = newItem.id
            };
            context.Customer_Billing_Info.Add(customer);
            context.SaveChanges();
            return 1;
                
        }

        public void Delete(int id)
        {
            var q = context.Customer_Billing_Info.Where(d => d.id == id).FirstOrDefault();
            if (q !=null)
            {
                context.Customer_Billing_Info.Remove(q);
                context.SaveChanges();
            }

        }

        public List<CustomerbillinginfoViewModel> GetAll()
        {
            var q = context.Customer_Billing_Info.Select(d => new CustomerbillinginfoViewModel { customerpolicyid = (int)d.customer_policy_id,id = d.id, active = d.active, amount = d.amount, bill_number = d.bill_number, createdate = d.create_date, customeraddprove = d.customer_add_prove }).ToList();
            return q;
                

        }

        public List<CustomerbillinginfoViewModel> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public CustomerbillinginfoViewModel GetBillById(int id)
        {
            var q = context.Customer_Billing_Info
                .Where(c => c.id == id )
                .Select(d => new CustomerbillinginfoViewModel { customerpolicyid = (int)d.customer_policy_id, id = d.id, active = d.active, amount = d.amount, bill_number = d.bill_number, createdate = d.create_date, customeraddprove = d.customer_add_prove }).FirstOrDefault();
            return q;
        }

        public CustomerbillinginfoViewModel GetEdit(int id)
        {
            var q = context.Customer_Billing_Info.Where(d => d.id == id).Select(d => new CustomerbillinginfoViewModel { customerpolicyid = (int)d.customer_policy_id, id = d.id, active = d.active, amount = d.amount, bill_number = d.bill_number, createdate = d.create_date, customeraddprove = d.customer_add_prove }).FirstOrDefault();
            return q;
                
        }

        public List<CustomerbillinginfoViewModel> Gets(int page, int row)
        {
            var q = context.Customer_Billing_Info.Select(d => new CustomerbillinginfoViewModel {  customerpolicyid = (int)d.customer_policy_id,id = d.id, active = d.active, amount = d.amount, bill_number = d.bill_number, createdate = d.create_date, customeraddprove = d.customer_add_prove }).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();
            return q;
        }

        public List<CustomerbillinginfoViewModel> Search(int page, int row, string keyword)
        {
            var CountItem = context.Customer_Billing_Info.Where(d => d.customer_add_prove.ToLower().Contains(keyword.ToLower()) ||d.create_date.ToString().Contains(keyword.ToString())||d.bill_number.ToString().Contains(keyword.ToString())||d.amount.ToString().Contains(keyword.ToString())).Count();
            var totalPage = CountItem / row;
            totalPage += (CountItem % row > 0 ? 1 : 0);
            HttpContext Context = HttpContext.Current;
            Context.Session["CoutItemCustomerBiilinfo"] = CountItem;
            Context.Session["totaPage"] = totalPage;
            var q = context.Customer_Billing_Info.Where(d => d.customer_add_prove.ToLower().Contains(keyword.ToLower()) || d.create_date.ToString().Contains(keyword.ToString()) || d.bill_number.ToString().Contains(keyword.ToString()) || d.amount.ToString().Contains(keyword.ToString())).Select(d => new CustomerbillinginfoViewModel
            {

                id = d.id,
                active = d.active,
                amount = d.amount,
                bill_number = d.bill_number,
                createdate = d.create_date,
                customeraddprove = d.customer_add_prove,
                 customerpolicyid = (int)d.customer_policy_id
            }).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();

            return q;
        }

        public int Update(CustomerbillinginfoViewModel updateItems)
        {
            try
            {
                var q = context.Customer_Billing_Info.Where(d => d.id == updateItems.id).FirstOrDefault();
                q.active = updateItems.active;
                q.amount = updateItems.amount;
                q.bill_number = updateItems.bill_number;
                q.create_date = updateItems.createdate;
                q.customer_add_prove = updateItems.customeraddprove;
                q.customer_policy_id = updateItems.customerpolicyid;
           
                context.SaveChanges();
                return 1;
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);

                return 0;
            }
        }

        public List<CustomerbillinginfoViewModel> FilterByDate(DateTime start, DateTime end, int page, int row)
        {
            var q = context.Customer_Billing_Info
                .Where(c => c.create_date >= start && c.create_date <= end)
                .Select(d => new CustomerbillinginfoViewModel { customerpolicyid = (int)d.customer_policy_id, id = d.id, active = d.active, amount = d.amount, bill_number = d.bill_number, createdate = d.create_date, customeraddprove = d.customer_add_prove }).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();
            return q;
        }

        public List<CustomerbillinginfoViewModel> CheckCustomerPolicyExist(int customerPolicyId)
        {
            var q = context.Customer_Billing_Info
                .Where(c => c.customer_policy_id == customerPolicyId)
                .Select(d => new CustomerbillinginfoViewModel { customerpolicyid = (int)d.customer_policy_id, id = d.id, active = d.active, amount = d.amount, bill_number = d.bill_number, createdate = d.create_date, customeraddprove = d.customer_add_prove }).ToList();
            return q;
        }
    }
}
