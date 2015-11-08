using LogisticsAPI.DataAccess;
using LogisticsAPI.Models;
using LogisticsAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;

namespace LogisticsAPI.Controllers
{
    public class LocationController : ODataController
    {
        [EnableQuery]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            using (var db = new DBUnitOfWork())
            {
                try
                {
                    List<LocationViewModel> locationViewModelList = new List<LocationViewModel>();
                    var locationList = db.Repository<Location>().GetAll();
                    foreach (var location in locationList)
                    {
                        LocationViewModel locationViewModel = new LocationViewModel();
                        locationViewModel.CopyFrom(location);
                        locationViewModelList.Add(locationViewModel);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, locationViewModelList);
                }
                catch (Exception)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] LocationViewModel locationViewModel)
        {
            using (var db = new DBUnitOfWork())
            {
                try
                {
                    Location location = new Location();
                    location.CopyFrom(locationViewModel,db);
                    db.Repository<Location>().Add(location);
                    return Request.CreateResponse(HttpStatusCode.Created);
                }
                catch (Exception)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
        }
        [HttpPut]
        public HttpResponseMessage Put([FromODataUri] string key, [FromBody] LocationViewModel locationViewModel)
        {
            using (var db = new DBUnitOfWork())
            {
                try
                {
                    Location location = new Location();
                    location.CopyFrom(locationViewModel, db);
                    location.EntityId = new Guid(key);
                    if (db.Repository<Location>().Update(location, location.EntityId) != null)
                        return Request.CreateResponse(HttpStatusCode.OK);
                    else
                        return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                catch (Exception)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
        }

        public HttpResponseMessage Delete([FromODataUri] string key)
        {
            using (var db = new DBUnitOfWork())
            {
                try
                {
                    if (db.Repository<Location>().Delete(new Guid(key)) == true)
                        return Request.CreateResponse(HttpStatusCode.OK);
                    else
                        return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                catch (Exception)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
        }
    }
}
