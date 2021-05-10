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
    public class ModelDAORequest : ICrudFeature<ModelViewModel>
    {
        public InsuranceDbContext context = new InsuranceDbContext();
        public int Add(ModelViewModel newItem)
        {
            Model model = new Model()
            {
                name = newItem.name,
                highest_rate = newItem.rate,
                active = newItem.active

            };
            context.Models.Add(model);
            context.SaveChanges();
            return 1;
        }

        public void Delete(int id)
        {
            var q = context.Models.Where(d => d.id == id).FirstOrDefault();
            if (q!=null)
            {
                context.Models.Remove(q);
                context.SaveChanges();

            }
        }

        public List<ModelViewModel> GetAll()
        {
            var q = context.Models.Select(d => new ModelViewModel { id = d.id, name = d.name, brandid = d.brand_id, rate = d.highest_rate, active = d.active }).ToList();
            return q;
        }

        public List<ModelViewModel> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public ModelViewModel GetEdit(int id)
        {
            var q = context.Models.Where(d => d.id == id).Select(d => new ModelViewModel { id = d.id, name = d.name, rate = d.highest_rate }).FirstOrDefault();
            return q;
        }

        public List<ModelViewModel> Gets(int page, int row)
        {
            var q = context.Models.Select(d => new ModelViewModel { id = d.id, name = d.name, rate = d.highest_rate }).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();
            return q;
        }

        public List<ModelViewModel> Search(int page, int row, string keyword)
        {
            var CountItem = context.Models.Where(d => d.name.ToLower().Contains(keyword.ToLower())).Count();
            var totalPage = CountItem / row;
            totalPage += (CountItem % row > 0 ? 1 : 0);
            HttpContext Context = HttpContext.Current;
            Context.Session["CountItemModel"] = CountItem;
            Context.Session["totalPage"] = totalPage;
            var q = context.Models.Where(d => d.name.ToLower().Contains(keyword.ToLower())).Select(d => new ModelViewModel { id = d.id, name = d.name, brandid = d.brand_id, active = d.active, rate = d.highest_rate }).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();
            return q;
        }


        public int Update( ModelViewModel updateItems)
        {


            try
            {
                var q = context.Models.Where(d => d.id == updateItems.id).FirstOrDefault();
                q.name = updateItems.name;
                q.highest_rate = updateItems.rate;
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
