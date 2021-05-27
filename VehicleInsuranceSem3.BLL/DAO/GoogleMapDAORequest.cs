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
  public  class GoogleMapDAORequest : ICrudFeature<GooglemapViewModel>
    {
        public InsuranceDbContext context = new InsuranceDbContext();

        public int Add(GooglemapViewModel newItem)
        {
            Google_Map google = new Google_Map()
            {
                city_name = newItem.cityname,
                latitude = newItem.latitude,
                longitude = newItem.longtitude,
                description = newItem.description


            };
            context.Google_Map.Add(google);
            context.SaveChanges();
            return 1;

        }

        public void Delete(int id)
        {
            var q = context.Google_Map.Where(d => d.id == id).FirstOrDefault();
            if (q !=null)
            {
                context.Google_Map.Remove(q);
                context.SaveChanges();

            }

        }

        public List<GooglemapViewModel> GetAll()
        {
            var q = context.Google_Map.Select(d => new GooglemapViewModel { id = d.id, cityname = d.city_name, description = d.description, latitude = d.latitude, longtitude = d.longitude }).ToList();
            return q;
        }

        public List<GooglemapViewModel> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public GooglemapViewModel GetEdit(int id)
        {
            var q = context.Google_Map.Where(d => d.id == id).Select(d => new GooglemapViewModel { id = d.id, cityname = d.city_name, description = d.description, latitude = d.latitude, longtitude = d.longitude }).FirstOrDefault();
            return q;
        }

        public List<GooglemapViewModel> Gets(int page, int row)
        {
            var q = context.Google_Map.Select(d => new GooglemapViewModel { id = d.id, cityname = d.city_name, description = d.description, latitude = d.latitude, longtitude = d.longitude }).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();
            return q;
        }

        public List<GooglemapViewModel> Search(int page, int row, string keyword)
        {
            var CountItem = context.Google_Map.Where(d => d.city_name.ToLower().Contains(keyword.ToLower()) || d.description.ToLower().Contains(keyword.ToLower())||d.latitude.ToString().Contains(keyword.ToLower())||d.longitude.ToString().Contains(keyword.ToString())).Count();
            var totalPage = CountItem / row;
            totalPage += (CountItem % row > 0 ? 1 : 0);
            HttpContext Context = HttpContext.Current;
            Context.Session["CountItemGooglemap"] = CountItem;
            Context.Session["totalPage"] = totalPage;
            var q = context.Google_Map.Where(d => d.city_name.ToLower().Contains(keyword.ToLower()) || d.description.ToLower().Contains(keyword.ToLower()) || d.latitude.ToString().Contains(keyword.ToLower()) || d.longitude.ToString().Contains(keyword.ToString())).Select(
                d => new GooglemapViewModel { id = d.id, cityname = d.city_name, description = d.description, latitude = d.latitude, longtitude = d.longitude }
                ).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();
            return q;
        }

        public int Update(GooglemapViewModel updateItems)
        {
            try
            {
                var q = context.Google_Map.Where(d => d.id == updateItems.id).FirstOrDefault();
                q.city_name = updateItems.cityname;
                q.description = updateItems.description;
                q.latitude = updateItems.latitude;
                q.longitude = updateItems.longtitude;
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
