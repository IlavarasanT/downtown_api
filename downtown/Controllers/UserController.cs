using Dapper;
using downtown.Constants;
using downtown.Context;
using downtown.Model;
using downtown.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

namespace downtown.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DapperContext _context;

        //private readonly SqlService _sqlservice;
        //public UserController(SqlService sqlservice)
        //{
        //    _sqlservice = sqlservice;

        //}

        public UserController(DapperContext context)
        {
            _context = context;

        }

        //[HttpGet]
        //public async Task<Department> GetDepartment()
        //{
        //    //var departmentlist = new List<Department>();
        //   var departmentlist = await _sqlService.GetSingleExecuteQueryasync<Department>(SqlQuery.GetDepartment);
        //    return departmentlist;
        //}

        [HttpGet]
        [Route("getdept")]
        public async Task<IEnumerable<Department>> GetCompanies()
        {

            using (var connection = _context.CreateConnection())
            {
                var departmentList = await connection.QueryAsync<Department>(SqlQuery.GetDepartment);
                return departmentList.ToList();
            }
        }
        [HttpGet]
        [Route("getregister")]
        public async Task<IEnumerable<Register>> GetRegister()
        {

            using (var connection = _context.CreateConnection())
            {
                var registerList = await connection.QueryAsync<Register>(SqlQuery.GetRegisterDetails);
                return registerList.ToList();
            }
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IEnumerable<Register>> SaveRegisterDetails(Register registerDetails)
        {

            using (var connection = _context.CreateConnection())
            {
                var response = await connection.QueryAsync<Register>(SqlQuery.SaveRegisterDetails, new { 
                    FirstName=registerDetails.FirstName, 
                    LastName=registerDetails.LastName,
                    Email=registerDetails.Email,
                    Password=registerDetails.Password,
                    Address=registerDetails.Address,
                    PhoneNumber=registerDetails.PhoneNumber
                });
                return response;
            }
        }

        [HttpPost]
        [Route("saveproduct")]
        public async Task<IEnumerable<ProductDetails>> SaveRegisterDetails(ProductDetails productDetails)
        {

            using (var connection = _context.CreateConnection())
            {
                var response = await connection.QueryAsync<ProductDetails>(SqlQuery.SaveProductDetails, new
                {
                    ProductName = productDetails.ProductName,
                    ProductDesc = productDetails.ProductDesc,
                    ProductPrice = productDetails.ProductPrice,
                    ProductQuantity = productDetails.ProductQuantity,
                    ProductImage = productDetails.ProductImage,
                });
                return response;
            }
        }
        [HttpGet]
        [Route("getproduct")]
        public async Task<IEnumerable<ProductDetails>> GetProduct()
        {
            var productDetailsList = new ProductDetails();
            using (var connection = _context.CreateConnection())
            {
                var registerList = await connection.QueryAsync<ProductDetails>(SqlQuery.GetProductDetails);
                return registerList.ToList();
            }
            //using (var connection = _context.CreateConnection())
            //{
            //    productDetailsList = await connection.QueryAsync<ProductDetails>(SqlQuery.GetProductDetails);
            //}
        }
        //[HttpGet]
        //[Route("checkuserisvalid/{email}")]
        //[HttpPost]
        //[Route("checkuserisvalid")]
        //public async Task<IEnumerable<registerinfo>> CheckEmailIsValid(registerinfo registerinfo)
        //{

        //    using (var connection = _context.CreateConnection())
        //    {
        //        var registerDetail = await connection.QueryAsync<registerinfo>(SqlQuery.GetEmailAddressAndPassword, new { Email= registerinfo.Email });
        //        return registerDetail;
        //    }
        //}
        //[HttpGet]
        //[Route("checkemailexist")]
        //public async Task<IEnumerable<string>> CheckEmailIsExist(string email)
        //{

        //    using (var connection = _context.CreateConnection())
        //    {
        //        var registerDetail = await connection.QueryAsync<string>(SqlQuery.GetEmailAddress, new { Email = email });
        //        return registerDetail;
        //    }
        //}

        #region  check the user is exists
        [HttpGet]
        [Route("checkuser")]
        public async Task<registerinfo> CheckEmailIsValid(string email)
        {
            var response = new registerinfo();


            if (email != null && email != "")
            {
                using (var connection = _context.CreateConnection())
                {
                    response = await connection.QueryFirstOrDefaultAsync<registerinfo>(SqlQuery.GetEmailAddressAndPassword, new { Email = email });
                    return (response);

                }

            }

            else
            {
                return (response);
            }
        }

        #endregion

        #region  check the email address is already exists or not

        [HttpGet]
        [Route("getemail")]
        public async Task<registerinfo> CheckEmailIsExist(string email)
        {
            var response = new registerinfo();


            if (email != null && email != "")
            {
                using (var connection = _context.CreateConnection())
                {
                    response = await connection.QueryFirstOrDefaultAsync<registerinfo>(SqlQuery.GetEmailAddress, new { Email = email });
                    return (response);

                }

            }

            else
            {
                return (response);
            }
        }
        #endregion

        //[HttpPost]
        //[Route("uploadfile")]
        //public FileModel UploadFile(FileModel filemodel)
        //{
        //    var reponse = new FileModel();
        //    string path = Path.Combine(@"D:\\myimage", filemodel.filename);
        //    using (Stream stream = new FileStream(path, FileMode.Create))
        //    {
        //        //filemodel.file.CopyTo(stream);
        //    }

        //    return reponse;
       // }
        //[HttpPost]
        //[Route("uploadfile")]
        //public  FileModel UploadFile(FileModel fileModel)
        //{

        //    var response  = new FileModel();
        //    string path = Path.Combine(@"D:\\myimage", fileModel.filename);
        //    using (Stream stream = new FileStream(path, FileMode.Create))
        //    {
        //        //filemodel.file.CopyTo(stream);
        //    }

        //        return response;
            
        //}


        [HttpPost]
        [Route("uploadfil")]
        public string UploadFil()
        {
            string reponse = "";

            var httpRequest = HttpContext.Request;
            var postedFile = httpRequest.Form.Files[0];
            string filename = postedFile.FileName;
            string paths = Path.Combine(@"D:\\myimage", filename);
            using (Stream stream = new FileStream(paths, FileMode.Create))
            {
                postedFile.CopyTo(stream);
            }
            return reponse;
        }

       
    }
}
