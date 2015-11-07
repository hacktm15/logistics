using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using LogisticsAPI.DataAccess;
using LogisticsAPI.Models;
using LogisticsAPI.ViewModels;
using static System.String;

namespace LogisticsAPI.Controllers
{
    public class WarningController : ODataController
    {
        public HttpResponseMessage Get()
        {
            using (var db = new DBUnitOfWork())
            {
                try
                {
                    var warningViewModels = new List<WarningViewModel>();
                    var warningModels = db.Repository<Warning>().GetAll();
                    foreach (var warning in warningModels)
                    {
                        var warningViewModel = new WarningViewModel();
                        warningViewModel.CopyFrom(warning);
                        warningViewModels.Add(warningViewModel);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, warningViewModels); ;
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                }
            }
        }
        [HttpGet]
        public HttpResponseMessage Get([FromODataUri] string key)
        {
            using (var db = new DBUnitOfWork())
            {
                try
                {
                    foreach (var item in db.Repository<Item>().GetAll())
                    {
                        WarningController.UpdateWarningForItem(item);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                }
            }
        }
        public static void UpdateWarningForItem(Item item)
        {
            var warningEnabled = item.Quantity < item.MinQuantity;
            var shouldUpdateExisting = false;
            try
            {
                using (var db = new DBUnitOfWork())
                {
                    var existing = db.Repository<Warning>().Find(x => x.ItemEntityId.Equals(item.EntityId));
                    if (warningEnabled)
                    {
                        if (existing == null)
                        {
                            var warning = new Warning
                            {
                                ItemEntityId = item.EntityId,
                                Message = "Warning"
                            };
                            item.Status = "Warning";
                            db.Repository<Warning>().Add(warning);
                            shouldUpdateExisting = true;
                        }
                    }
                    else
                    {
                        if (existing != null)
                        {
                            item.Status = string.Empty;
                            db.Repository<Warning>().Delete(existing.EntityId);
                            shouldUpdateExisting = true;
                        }
                    }
                }

                if (shouldUpdateExisting)
                {
                    using (var db = new DBUnitOfWork())
                    {
                        db.Repository<Item>().Update(item, item.EntityId);
                    }
                }
            }
            catch (Exception exc)
            {
                
            }
        }

        public static void DeleteWarningForItem(Guid itemEntityId)
        {
            using (var db = new DBUnitOfWork())
            {
                var existing = db.Repository<Warning>().Find(x => x.ItemEntityId.Equals(itemEntityId));
                if (existing != null)
                {
                    db.Repository<Warning>().Delete(existing);
                }
            }
        }
    }
}
