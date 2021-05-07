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
    public class UserInfoDAORequest : ICrudFeature<UserinfoViewModel>
    {
        public InsuranceDbContext context = new InsuranceDbContext();   

        public int Add(UserinfoViewModel newItem)
        {
            User_Info user = new User_Info()
            {
                username = newItem.username,
                password = newItem.password,
                authorize_token = newItem.authorizetoken,
                active = newItem.active,

            };
            context.User_Info.Add(user);
            context.SaveChanges();
            return 1;
        }

        public void Delete(int id)
        {
            var q = context.User_Info.Where(d => d.id == id).FirstOrDefault();
            if (q!=null)
            {
                context.User_Info.Remove(q);
                context.SaveChanges();

            }
        }

        public List<UserinfoViewModel> GetAll()
        {
            var q = context.User_Info.Select(d => new UserinfoViewModel { id = d.id, username = d.username, password = d.password, active = d.active, authorizetoken = d.authorize_token }).ToList();
            return q;

        }

        public List<UserinfoViewModel> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public UserinfoViewModel GetEdit(int id)
        {
            var q = context.User_Info.Where(d => d.id == id).Select(d => new UserinfoViewModel { id = d.id, username = d.username, password = d.password, active = d.active, authorizetoken = d.authorize_token }).FirstOrDefault();
            return q;

        }


        public List<UserinfoViewModel> Gets(int page, int row)
        {
            var q = context.User_Info.Select(d => new UserinfoViewModel { id = d.id, username = d.username, password = d.password, active = d.active, authorizetoken = d.authorize_token }).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();
            return q;
        }

        public List<UserinfoViewModel> Search(int page, int row, string keyword)
        {
            var countItem = context.User_Info.Where(d => d.username.ToLower().Contains(keyword.ToLower())).Count();
            var totalPage = countItem / row;
            totalPage += (countItem % row > 0 ? 1 : 0);
            HttpContext Context = HttpContext.Current;
            Context.Session["CounItemUserinfo"] = countItem;
            Context.Session["TotalPage"] = totalPage;
            var q = context.User_Info.Where(d => d.username.ToLower().Contains(keyword.ToLower())).Select(d=>new UserinfoViewModel { id = d.id , username = d.username , password = d.password , active = d.active , authorizetoken =d.authorize_token}).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();
            return q;

        }

        public int Update(UserinfoViewModel updateItems)
        {
            try
            {
                var q = context.User_Info.Where(d => d.id == updateItems.id).FirstOrDefault();
                q.username = updateItems.username;
                q.password = updateItems.password;
                q.active = updateItems.active;
                q.authorize_token = updateItems.authorizetoken;
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
