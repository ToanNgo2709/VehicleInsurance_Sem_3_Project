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
using VehicleInsuranceSem3.Utilities.Crypto;

namespace VehicleInsuranceSem3.BLL.DAO
{
    public class CustomerinfoDAORequest : ICrudFeature<CustomerinfoViewModel>
    {
        public InsuranceDbContext context = new InsuranceDbContext();
        public int Add(CustomerinfoViewModel newItem)
        {
            try
            {
                Customer_Info customer = new Customer_Info()
                {

                    name = newItem.name,
                    dob = newItem.dob,
                    address = newItem.address,
                    phone = newItem.phone,
                    email = newItem.email,
                    active = newItem.active,
                    username = newItem.username,
                    password = PasswordSecurity.Encrypt(newItem.password),
                    user_type_id = newItem.user_type_id
                };
                context.Customer_Info.Add(customer);
                context.SaveChanges();

                return 1;
            }
            catch (EntityException ex)
            {
                Debug.Write(ex.Message);
                return 0;
            }


        }
        public Customer_Info searchCustomerById(int id)
        {
            var item = context.Customer_Info.Where(c => c.id == id)
                .FirstOrDefault();
            return item;
        }

        public void Delete(int id)
        {
            var q = context.Customer_Info.Where(d => d.id == id).FirstOrDefault();
            if (q != null)
            {
                context.Customer_Info.Remove(q);
                context.SaveChanges();

            }
        }

        public List<CustomerinfoViewModel> GetAll()
        {
            var q = context.Customer_Info.Select(d => new CustomerinfoViewModel { id = d.id, name = d.name, dob = d.dob, active = (bool)d.active, address = d.address, email = d.email, phone = d.phone, username = d.username }).ToList();
            return q;

        }

        public List<CustomerinfoViewModel> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public CustomerinfoViewModel GetCustomerById(int Id)
        {
            var customer = context.Customer_Info.Where(c => c.id == Id)
                .Select(c => new CustomerinfoViewModel {
                    id = c.id,
                    active = (bool)c.active,
                    address = c.address,
                    dob = c.dob,
                    email = c.email,
                    name = c.name,
                    password = c.password,
                    phone = c.phone,
                    username = c.username,
                    user_type_id = c.user_type_id
                }).FirstOrDefault();
            return customer;
        }

        public CustomerinfoViewModel GetEdit(int id)
        {
            var q = context.Customer_Info.Where(d => d.id == id).Select(d => new CustomerinfoViewModel { id = d.id, name = d.name, address = d.address, dob = d.dob, phone = d.phone, email = d.email, active = (bool)d.active, username = d.username, user_type_id = d.user_type_id, password = d.password }).FirstOrDefault();
            return q;

        }

        public List<CustomerinfoViewModel> Gets(int page, int row)
        {
            var q = context.Customer_Info.Select(d => new CustomerinfoViewModel { id = d.id, active = (bool)d.active, address = d.address, dob = d.dob, email = d.email, name = d.name, phone = d.phone, username = d.username }).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();
            return q;

        }

        public List<CustomerinfoViewModel> Search(int page, int row, string keyword)
        {
            var CountItem = context.Customer_Info.Where(d => d.name.ToLower().Contains(keyword.ToLower()) || d.address.ToLower().Contains(keyword.ToLower()) || d.email.ToLower().Contains(keyword.ToLower()) || d.username.ToLower().Contains(keyword.ToLower())).Count();
            var totalPage = CountItem / row;
            totalPage += (CountItem % row > 0 ? 1 : 0);
            HttpContext Context = HttpContext.Current;
            Context.Session["CountItemCustomerInfo"] = CountItem;
            Context.Session["totalPage"] = totalPage;
            var q = context.Customer_Info.Where(d => d.name.ToLower().Contains(keyword.ToLower()) || d.address.ToLower().Contains(keyword.ToLower()) || d.email.ToLower().Contains(keyword.ToLower()) || d.username.ToLower().Contains(keyword.ToLower())).Select(d => new CustomerinfoViewModel { id = d.id, active = (bool)d.active, address = d.address, dob = d.dob, email = d.email, name = d.name, phone = d.phone, username = d.username }).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();
            return q;
        }

        public CustomerinfoViewModel GetByUsernameAndEmail(string username, string email)
        {
            var q = context.Customer_Info.Where(c => c.username.Equals(username) && c.email.Equals(email))
                .Select(d => new CustomerinfoViewModel { id = d.id, name = d.name, dob = d.dob, active = (bool)d.active, address = d.address, email = d.email, phone = d.phone, username = d.username, password = d.password }).FirstOrDefault();
            return q;
        }

        public int Update(CustomerinfoViewModel updateItems)
        {
            try
            {
                var q = context.Customer_Info.Where(d => d.id == updateItems.id).SingleOrDefault();
                q.name = updateItems.name;
                q.active = updateItems.active;
                q.address = updateItems.address;
                q.dob = updateItems.dob;
                q.email = updateItems.email;
                q.phone = updateItems.phone;
                q.username = updateItems.username;
                q.password = updateItems.password;
                q.user_type_id = updateItems.user_type_id;
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
