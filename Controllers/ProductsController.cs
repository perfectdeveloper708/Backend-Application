using DemoAPI.Generic;
using DemoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DemoAPI.Controllers
{
    [ApiController]
    
    public class ProductsController : ControllerBase
    {
        private  DemoAPIContext _context;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [Route("api/Product/GetProducts")]
        public Response Get()
        {
            Response resp = new Response();
            try
            {
                using (_context = new DemoAPIContext())
                {
                    var record = _context.Products.ToList();
                    if (record.Count>0)
                    {
                        resp.httpCode = HttpStatusCode.OK;
                        resp.message = "Record found";
                        resp.json = JsonConvert.SerializeObject(record);
                    }
                    else
                    {
                        resp.httpCode = HttpStatusCode.BadRequest;
                        resp.message = "No record found";
                    }
                }
            }
            catch (Exception ex)
            {
                resp.httpCode = HttpStatusCode.BadRequest;
                resp.message = "Something went wrong";
                resp.json = JsonConvert.SerializeObject(new { Error = ex.Message });
            }
            return resp;
        }
        [HttpGet]
        [Route("api/Product/GetProductsById")]
        public Response GetById(int id)
        {
            Response resp = new Response();
            try
            {
                using (_context = new DemoAPIContext())
                {
                    var record = _context.Products.Where(a => a.Id == id).FirstOrDefault();
                    if (!ReferenceEquals(record, null))
                    {
                        resp.httpCode = HttpStatusCode.OK;
                        resp.message = "Record found";
                        resp.json = JsonConvert.SerializeObject(record);
                    }
                    else
                    {
                        resp.httpCode = HttpStatusCode.BadRequest;
                        resp.message = "No record found";
                    }
                }
            }
            catch (Exception ex)
            {
                resp.httpCode = HttpStatusCode.BadRequest;
                resp.message = "Something went wrong";
                resp.json = JsonConvert.SerializeObject(new { Error = ex.Message });
            }
            return resp;
        }
        [HttpPost]
        [Route("api/Product/Post")]
        public Response Post(Product item)
        {
            Response resp = new Response();
            try
            {
                using (_context = new DemoAPIContext())
                {
                    item.CreatedOn = DateTime.Now;
                    item.LastUpdatedOn = DateTime.Now;
                    item.IsDeleted = false;
                    _context.Products.Add(item);
                    _context.SaveChanges();
                    resp.httpCode = HttpStatusCode.OK;
                    resp.message = "Request Posted Successfully";
                }
            }
            catch (Exception ex)
            {
                resp.httpCode = HttpStatusCode.BadRequest;
                resp.message = "Something went wrong";
                resp.json = JsonConvert.SerializeObject(new { Error = ex.Message });
                return resp;
            }
            return resp;
        }
        [HttpPut]
        [Route("api/Product/Update")]
        public Response Put(Product item)
        {
            Response resp = new Response();
            try
            {
                using (_context = new DemoAPIContext())
                {
                    Product record = _context.Products.Where(a => a.Id == item.Id).FirstOrDefault();
                    if (!ReferenceEquals(record, null))
                    {
                        record.Title = item.Title;
                        record.ProductTypeId = item.ProductTypeId;
                        record.ContactPersonId = item.ContactPersonId;
                        record.Description = item.Description;
                        record.LastUpdatedOn = DateTime.Now;
                       // _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _context.SaveChanges();
                        resp.httpCode = HttpStatusCode.OK;
                        resp.message = "Record Updated Successfully";
                    }
                    else
                    {
                        resp.httpCode = HttpStatusCode.BadRequest;
                        resp.message = "No record found";
                    }
                }
            }
            catch (Exception ex)
            {
                resp.httpCode = HttpStatusCode.BadRequest;
                resp.message = "Something went wrong";
                resp.json = JsonConvert.SerializeObject(new { Error = ex.Message });
                return resp;
            }
            return resp;
        }
        [HttpDelete]
        [Route("api/Product/Delete")]
        public Response Delete(int id)
        {
            Response resp = new Response();
            try
            {
                using (_context = new DemoAPIContext())
                {
                    Product record = _context.Products.Where(a => a.Id == id).FirstOrDefault();
                    if (!ReferenceEquals(record, null))
                    {
                        _context.Products.Remove(record);
                        _context.SaveChanges();
                        resp.httpCode = HttpStatusCode.OK;
                        resp.message = "Record Deleted Successfully";
                    }
                    else
                    {
                        resp.httpCode = HttpStatusCode.BadRequest;
                        resp.message = "No record found";
                    }
                }
            }
            catch (Exception ex)
            {
                resp.httpCode = HttpStatusCode.BadRequest;
                resp.message = "Something went wrong";
                resp.json = JsonConvert.SerializeObject(new { Error = ex.Message });
                return resp;
            }
            return resp;
        }
    }
}
