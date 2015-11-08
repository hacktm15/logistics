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
using LogisticsAPI.ViewModels;
using LogisticsAPI.Authorization;

namespace LogisticsAPI.Controllers
{
    public class ItemController : ODataController
    {
        [EnableQuery]
        [HttpGet]
        [LDAPAuthorize(Roles = new[] {Role.Read})]
        public HttpResponseMessage GetItem()
        {
            using (var db = new DBUnitOfWork())
            {
                try
                {
                    List<ItemViewModel> itemViewModelList = new List<ItemViewModel>();
                    var itemList = db.Repository<Item>().GetAll();
                    foreach (var item in itemList)
                    {
                        ItemViewModel itemViewModel = new ItemViewModel();
                        itemViewModel.CopyFrom(item);
                        itemViewModelList.Add(itemViewModel);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, itemViewModelList);
                }
                catch (Exception)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
        }


        [HttpPost]
        [LDAPAuthorize(Roles = new[] { Role.Write })]
        public HttpResponseMessage Post([FromBody] ItemViewModel itemViewModel)
        {
            using (var db = new DBUnitOfWork())
            {
                try
                {
                    Item item = new Item();
                    item.CopyFrom(itemViewModel,db);
                    db.Repository<Item>().Add(item);
                    WarningController.UpdateWarningForItem(item);
                    return Request.CreateResponse(HttpStatusCode.Created);
                }
                catch (Exception)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
        }
        [HttpPut]
        [LDAPAuthorize(Roles = new[] { Role.Write })]
        public HttpResponseMessage Put([FromODataUri] string key, [FromBody] ItemViewModel itemViewModel)
        {
            using (var db = new DBUnitOfWork())
            {
                try
                {
                    Item item = new Item();
                    item.CopyFrom(itemViewModel, db);
                    item.EntityId = new Guid(key);
                    if (db.Repository<Item>().Update(item, item.EntityId) != null)
                    {
                        WarningController.UpdateWarningForItem(item);
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                        return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                catch (Exception)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
        }

        [LDAPAuthorize(Roles = new[] { Role.Write })]
        public HttpResponseMessage Delete([FromODataUri] string key)
        {
            using (var db = new DBUnitOfWork())
            {
                try
                {
                    var itemEntityId = new Guid(key);
                    if (db.Repository<Item>().Delete(itemEntityId) == true)
                    {
                        WarningController.DeleteWarningForItem(itemEntityId);
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
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
