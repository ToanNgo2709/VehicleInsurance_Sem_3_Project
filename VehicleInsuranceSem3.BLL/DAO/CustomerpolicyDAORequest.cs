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
                customer_id = newItem.customerid,
                vehicle_id = newItem.vehicleid,
                policy_id = newItem.policyid,
                total_payment = newItem.TotalPayment
                
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
            var q = context.Customer_Policy.Select(d => new CustomerpolicyViewModel { id = d.id, active = d.active, createdate = d.create_date, customeraddprove = d.customer_add_prove, policystartdate = d.policy_start_date, policyenddate = d.policy_end_date , TotalPayment = d.total_payment , policyid = (int)d.policy_id , vehicleid = (int)d.vehicle_id , customerid = (int)d.customer_id }).ToList();
            return q;

        }

        public List<CustomerpolicyViewModel> GetLapsePolicy(DateTime today)
        {
            var q = context.Customer_Policy.Where(c => c.policy_end_date < today)
                .Select(d => new CustomerpolicyViewModel { id = d.id, active = d.active, createdate = d.create_date, customeraddprove = d.customer_add_prove, policystartdate = d.policy_start_date, policyenddate = d.policy_end_date, TotalPayment = d.total_payment, policyid = (int)d.policy_id, vehicleid = (int)d.vehicle_id, customerid = (int)d.customer_id }).ToList();
            return q;

        }



        public List<CustomerpolicyViewModel> GetById(int Id)
        {
            var q = context.Customer_Policy.Where(c => c.id == Id).Select(d => new CustomerpolicyViewModel { id = d.id, active = d.active, createdate = d.create_date, customeraddprove = d.customer_add_prove, policystartdate = d.policy_start_date, policyenddate = d.policy_end_date, TotalPayment = d.total_payment, policyid = (int)d.policy_id, vehicleid = (int)d.vehicle_id, customerid = (int)d.customer_id }).ToList();
            return q;
        }

        public CustomerpolicyViewModel GetCustomerPolicyById(int Id)
        {
            var q = context.Customer_Policy.Where(c => c.id == Id).Select(d => new CustomerpolicyViewModel { id = d.id, active = d.active, createdate = d.create_date, customeraddprove = d.customer_add_prove, policystartdate = d.policy_start_date, policyenddate = d.policy_end_date, TotalPayment = d.total_payment, policyid = (int)d.policy_id, vehicleid = (int)d.vehicle_id, customerid = (int)d.customer_id }).FirstOrDefault();
            return q;
        }

        public CustomerpolicyViewModel GetEdit(int id)
        {
            var q = context.Customer_Policy.Where(d => d.id == id).Select(d => new CustomerpolicyViewModel { id = d.id, active = d.active, createdate = d.create_date, customeraddprove = d.customer_add_prove, policystartdate = d.policy_start_date, policyenddate = d.policy_end_date , customerid = (int)d.customer_id , vehicleid = (int)d.vehicle_id , policyid = (int)d.policy_id , TotalPayment = d.total_payment }).FirstOrDefault();
            return q;

        }


        public List<CustomerpolicyViewModel> Gets(int page, int row)
        {
            var q = context.Customer_Policy.Select(d => new CustomerpolicyViewModel { id = d.id, active = d.active, createdate = d.create_date, customeraddprove = d.customer_add_prove, policystartdate = d.policy_start_date, policyenddate = d.policy_end_date , TotalPayment = d.total_payment , policyid = (int)d.policy_id, vehicleid = (int)d.vehicle_id , customerid = (int)d.customer_id}).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();
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
                customeraddprove = d.customer_add_prove, customerid = (int)d.customer_id , vehicleid = (int)d.vehicle_id , policyid = (int)d.policy_id , TotalPayment = d.total_payment

            }).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();

            return q;
        }
        
        public bool checkexits(string username)
        {
            var q = context.Customer_Info.Where(d => d.username == username).ToList();
            if (q == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        
        
        }

        public CustomerinfoViewModel Login(string username)
        {
            var q = context.Customer_Info.Where(d => d.username == username).Select(d => new CustomerinfoViewModel { id = d.id, username = d.username, password= d.password , user_type_id = d.user_type_id}).FirstOrDefault();
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
                q.total_payment = updateItems.TotalPayment;
                q.vehicle_id = updateItems.vehicleid;
                q.policy_id = updateItems.policyid;
                q.customer_id = updateItems.customerid;
                
                
                
                context.SaveChanges();
                return 1;


            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);

                return 0;
            }
            
        }
        public List<CustomerHistoryModelView> GetCustomerPolicyHistory(int customerId)
        {
            var list = context.Customer_Policy
                .Where(c => c.customer_id == customerId)
                .Select(c => new CustomerHistoryModelView
                {
                    CustomerPolicyId = c.id,
                    CreateDate = c.create_date,
                    CustomerId = (int)c.customer_id,
                    EndDate = c.policy_end_date,
                    PolicyName = c.Policy.policy_number,
                    StartDate = c.policy_start_date,
                    TotalPayment = c.total_payment,
                    VehicleName = c.Vehicle_Info.Brand.name + " " + c.Vehicle_Info.Model.name
                }).ToList();
            return list;
        }

        public List<CustomerpolicyViewModel> FilterCustomerPolicyByCreateDate(DateTime startDate, DateTime endDate, int page, int row) 
        {
            var q = context.Customer_Policy
                .Where(c => c.create_date >= startDate && c.create_date <= endDate)
                .Select(d => new CustomerpolicyViewModel { id = d.id, active = d.active, createdate = d.create_date, customeraddprove = d.customer_add_prove, policystartdate = d.policy_start_date, policyenddate = d.policy_end_date, TotalPayment = d.total_payment, policyid = (int)d.policy_id, vehicleid = (int)d.vehicle_id, customerid = (int)d.customer_id }).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();
            return q;
        }
    }
}
