using insuranceapp.DAL;
using insuranceapp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace insuranceapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceAppController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public InsuranceAppController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("AddPolicy")]
        public ActionResult<Response> AddPolicy(InsurancePolicy insurancePolicy)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection"));

            InsuranceAppDAL dal = new InsuranceAppDAL();
            Response response = dal.AddPolicy(connection, insurancePolicy);

            return response;
        }

        [HttpPut]
        [Route("UpdatePolicy/{policyNumber}")]
        public ActionResult<UpdateInsuranceResponse> UpdatePolicy(int policyNumber, InsurancePolicy insurancePolicy)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection"));

            InsuranceAppDAL dal = new InsuranceAppDAL();
            UpdateInsuranceResponse response = dal.UpdatePolicy(connection, policyNumber, insurancePolicy);

            return response;
        }

        [HttpDelete]
        [Route("DeletePolicy/{policyNumber}")]
        public ActionResult<DeleteInsuranceResponse> DeletePolicy(int policyNumber)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection"));

            InsuranceAppDAL dal = new InsuranceAppDAL();
            DeleteInsuranceResponse response = dal.DeletePolicy(connection, policyNumber);

            return response;
        }

        [HttpGet]
        [Route("GetPolicy/{policyNumber}")]
        public ActionResult<GetPolicyByNumberResponse> GetPolicy(int policyNumber)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection"));

            InsuranceAppDAL dal = new InsuranceAppDAL();
            GetPolicyByNumberResponse response = dal.GetPolicyByPolicyNumber(connection, policyNumber);

            return response;
        }

        [HttpGet]
        [Route("GetAllPolicies")]
        public ActionResult<GetAllPoliciesResponse> GetAllPolicies()
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection"));

            InsuranceAppDAL dal = new InsuranceAppDAL();
            GetAllPoliciesResponse response = dal.GetAllPolicies(connection);

            return response;
        }
    }
}
