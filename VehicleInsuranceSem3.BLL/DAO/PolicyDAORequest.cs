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
   public class PolicyDAORequest : ICrudFeature<PolicyViewModel>
    {
        public InsuranceDbContext context = new InsuranceDbContext();

        public int Add(PolicyViewModel newItem)
        {
            Policy policy = new Policy()
            {
                policy_date = newItem.policydate,
                policy_number = newItem.policynumber,
                policy_duration = newItem.policyduration,
                active = newItem.active
            };
            context.Policies.Add(policy);
            context.SaveChanges();
            return 1;
        }

        public void Delete(int id)
        {
            var q = context.Policies.Where(d => d.id == id).FirstOrDefault();
            if (q!= null)
            {
                context.Policies.Remove(q);
                context.SaveChanges();
            }

        }

        public List<PolicyViewModel> GetAll()
        {
            var q = context.Policies.Select(d => new PolicyViewModel { id = d.id, active = d.active, policydate = d.policy_date, policyduration = d.policy_duration, policynumber = d.policy_number }).ToList();
            return q;

        }

        public List<PolicyViewModel> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public PolicyViewModel GetEdit(int id)
        {
            var q = context.Policies.Where(d => d.id == id).Select(d => new PolicyViewModel { id = d.id, active = d.active, policydate = d.policy_date, policyduration = d.policy_duration, policynumber = d.policy_number }).FirstOrDefault();
            return q;
                

        }

        public List<PolicyViewModel> Gets(int page, int row)
        {
            var q = context.Policies.Select(d => new PolicyViewModel { id = d.id, active = d.active, policydate = d.policy_date, policyduration = d.policy_duration, policynumber = d.policy_number }).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();
            return q;
        }

        public List<PolicyViewModel> Search(int page, int row, string keyword)
        {
            var CountItem = context.Policies.Where(d => d.policy_date.ToString().Contains(keyword.ToString()) ||d.policy_number.ToString().Contains(keyword.ToString()) ).Count();
            var totalPage = CountItem / row;
            totalPage += (CountItem % row > 0 ? 1 : 0);
            HttpContext Context = HttpContext.Current;
            Context.Session["CountItemPolicy"] = CountItem;
            Context.Session["totalPage"] = totalPage;
            var q = context.Policies.Where(d => d.policy_date.ToString().Contains(keyword.ToString()) || d.policy_number.ToString().Contains(keyword.ToString())).Select(d => new PolicyViewModel { id = d.id, active = d.active, policydate = d.policy_date, policyduration = d.policy_duration, policynumber = d.policy_number }).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();
            return q;

        }

        public int Update(PolicyViewModel updateItems)
        {
            try
            {
                var q = context.Policies.Where(d => d.id == updateItems.id).FirstOrDefault();
                q.active = updateItems.active;
                q.policy_date = updateItems.policydate;
                q.policy_duration = updateItems.policyduration;
                q.policy_number = updateItems.policynumber;
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
