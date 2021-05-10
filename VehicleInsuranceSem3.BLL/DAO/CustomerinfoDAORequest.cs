﻿using System;
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
    public class CustomerinfoDAORequest : ICrudFeature<CustomerinfoViewModel>
    {
        public InsuranceDbContext context = new InsuranceDbContext();
        public int Add(CustomerinfoViewModel newItem)
        {
            Customer_Info customer = new Customer_Info()
            {
                name = newItem.name,
                dob = newItem.dob,
                address = newItem.address,
                phone = newItem.phone,
                email = newItem.email,
                active = newItem.active,
            };
            context.Customer_Info.Add(customer);
            context.SaveChanges();
            return 1;
        }

        public void Delete(int id)
        {
            var q = context.Customer_Info.Where(d => d.id == id).FirstOrDefault();
            if (q!=null)
            {
                context.Customer_Info.Remove(q);
                context.SaveChanges();

            }
        }

        public List<CustomerinfoViewModel> GetAll()
        {
            var q = context.Customer_Info.Select(d => new CustomerinfoViewModel { id = d.id, name = d.name, dob = d.dob, active = d.active, address = d.address, email = d.email, phone = d.phone }).ToList();
            return q;

        }

        public List<CustomerinfoViewModel> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public CustomerinfoViewModel GetEdit(int id)
        {
            var q = context.Customer_Info.Where(d => d.id == id).Select(d => new CustomerinfoViewModel { id = d.id, name = d.name, address = d.address, dob = d.dob, phone = d.phone, email = d.email, active = d.active }).FirstOrDefault();
            return q;

        }

        public List<CustomerinfoViewModel> Gets(int page, int row)
        {
            var q = context.Customer_Info.Select(d => new CustomerinfoViewModel { id = d.id, active = d.active, address = d.address, dob = d.dob, email = d.email, name = d.name, phone = d.phone }).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();
            return q;

        }

        public List<CustomerinfoViewModel> Search(int page, int row, string keyword)
        {
            var CountItem = context.Customer_Info.Where(d => d.name.ToLower().Contains(keyword.ToLower()) || d.address.ToLower().Contains(keyword.ToLower()) || d.email.ToLower().Contains(keyword.ToLower())).Count();
            var totalPage = CountItem / row;
            totalPage += (CountItem % row > 0 ? 1 : 0);
            HttpContext Context = HttpContext.Current;
            Context.Session["CountItemCustomerInfo"] = CountItem;
            Context.Session["totalPage"] = totalPage;
            var q = context.Customer_Info.Where(d => d.name.ToLower().Contains(keyword.ToLower()) || d.address.ToLower().Contains(keyword.ToLower()) || d.email.ToLower().Contains(keyword.ToLower())).Select(d => new CustomerinfoViewModel
            {
                id = d.id,
                name = d.name,
                active = d.active,
                address = d.address,
                dob = d.dob,
                email = d.email,
                phone = d.phone
            }).Skip((page-1)*row).Take(row).ToList();
            return q;
        }

        public int Update(CustomerinfoViewModel updateItems)
        {
            try
            {
                var q = context.Customer_Info.Where(d => d.id == updateItems.id).FirstOrDefault();
                q.name = updateItems.name;
                q.active = updateItems.active;
                q.address = updateItems.address;
                    q.dob = updateItems.dob;
                q.email = updateItems.email;
                q.phone = updateItems.phone;
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