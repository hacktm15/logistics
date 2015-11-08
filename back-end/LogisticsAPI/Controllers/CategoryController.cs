using LogisticsAPI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using LogisticsAPI.DataAccess;
using System.Web.Http.OData.Query;
using LogisticsAPI.ViewModels;

namespace LogisticsAPI.Controllers
{
    public class CategoryController : ODataController
    {
        [EnableQuery]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            using (var db = new DBUnitOfWork())
            {
                try
                {
                    List<CategoryViewModel> categoryViewModelList = new List<CategoryViewModel>();
                    var categoryList = db.Repository<Category>().GetAll();
                    foreach (var category in categoryList)
                    {
                        CategoryViewModel categoryViewModel = new CategoryViewModel();
                        categoryViewModel.CopyFrom(category);
                        categoryViewModelList.Add(categoryViewModel);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, categoryViewModelList);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] CategoryViewModel categoryViewModel)
        {
            using (var db = new DBUnitOfWork())
            {
                try
                {
                    Category category = new Category();
                    category.CopyFrom(categoryViewModel, db);
                    db.Repository<Category>().Add(category);
                    return Request.CreateResponse(HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
        }

        [HttpPut]
        public HttpResponseMessage Put([FromODataUri] string key, [FromBody] CategoryViewModel categoryViewModel)
        {
            using (var db = new DBUnitOfWork())
            {
                try
                {
                    Category category = new Category();
                    category.CopyFrom(categoryViewModel, db);
                    category.EntityId = new Guid(key);
                    if (db.Repository<Category>().Update(category, category.EntityId) != null)
                        return Request.CreateResponse(HttpStatusCode.OK);
                    else
                        return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromODataUri] string key)
        {
            using (var db = new DBUnitOfWork())
            {
                try
                {
                    if (db.Repository<Category>().Delete(new Guid(key)) == true)
                        return Request.CreateResponse(HttpStatusCode.OK);
                    else
                        return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
        }
    }
}
