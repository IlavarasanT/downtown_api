using Dapper;
using downtown.Constants;
using downtown.Context;
using downtown.Model;
using downtown.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace downtown.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly SqlService _sqlService;


        //public UserController(SqlService sqlService)
        //{
        //    _sqlService = sqlService;
        //}

        private readonly DapperContext _context;
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
            //var query = "SELECT * FROM Companies";

            using (var connection = _context.CreateConnection())
            {
                var departmentList = await connection.QueryAsync<Department>(SqlQuery.GetDepartment);
                return departmentList.ToList();
            }
        }


    }
}
