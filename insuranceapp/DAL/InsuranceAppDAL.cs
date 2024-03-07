using insuranceapp.Models;
using System;
using System.Data.SqlClient;

namespace insuranceapp.DAL
{
    public class InsuranceAppDAL
    {
        public Response AddPolicy(SqlConnection connection, InsurancePolicy policy)
        {
            Response response = new Response();

            SqlCommand cmd = new SqlCommand("INSERT INTO InsurancePolicy (PolicyHolderFullName, PolicyHolderDateOfBirth, PolicyHolderAddress, PolicyHolderPhoneNumber, Type, Premium, StartDate, EndDate) VALUES (@PolicyHolderFullName, @PolicyHolderDateOfBirth, @PolicyHolderAddress, @PolicyHolderPhoneNumber, @Type, @Premium, @StartDate, @EndDate); SELECT SCOPE_IDENTITY();", connection);

            // Add parameters to avoid SQL injection and improve performance
            cmd.Parameters.AddWithValue("@PolicyHolderFullName", policy.PolicyHolder.FullName);
            cmd.Parameters.AddWithValue("@PolicyHolderDateOfBirth", policy.PolicyHolder.DateOfBirth);
            cmd.Parameters.AddWithValue("@PolicyHolderAddress", policy.PolicyHolder.Address);
            cmd.Parameters.AddWithValue("@PolicyHolderPhoneNumber", policy.PolicyHolder.PhoneNumber);
            cmd.Parameters.AddWithValue("@Type", policy.Type.ToString());
            cmd.Parameters.AddWithValue("@Premium", policy.Premium);
            cmd.Parameters.AddWithValue("@StartDate", policy.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", policy.EndDate);

            connection.Open();
            // Execute the query and get the newly inserted policy number
            int newPolicyNumber = Convert.ToInt32(cmd.ExecuteScalar());
            connection.Close();

            if (newPolicyNumber > 0)
            {
                GetAllPoliciesResponse getAllPolicies = GetAllPolicies(connection);

                response.StatusCode = 200;
                response.StatusMessage = "Policy Added Successfully";
                response.PolicyNumber = newPolicyNumber;
                response.Policies = getAllPolicies.Policies;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Failed to add policy";
            }

            return response;
        }

        public GetAllPoliciesResponse GetAllPolicies(SqlConnection connection)
        {
            GetAllPoliciesResponse response = new GetAllPoliciesResponse();
            List<InsurancePolicy> policies = new List<InsurancePolicy>();

            SqlCommand cmd = new SqlCommand("SELECT * FROM InsurancePolicy", connection);

            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                InsurancePolicy policy = new InsurancePolicy
                {
                    PolicyNumber = reader.GetInt32(reader.GetOrdinal("PolicyNumber")),
                    PolicyHolder = new PolicyHolder
                    {
                        FullName = reader.GetString(reader.GetOrdinal("PolicyHolderFullName")),
                        DateOfBirth = reader.GetDateTime(reader.GetOrdinal("PolicyHolderDateOfBirth")),
                        Address = reader.GetString(reader.GetOrdinal("PolicyHolderAddress")),
                        PhoneNumber = reader.GetString(reader.GetOrdinal("PolicyHolderPhoneNumber"))
                    },
                    Type = (InsuranceType)Enum.Parse(typeof(InsuranceType), reader.GetString(reader.GetOrdinal("Type"))),
                    Premium = (reader.GetOrdinal("Premium")),
                    StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                    EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate"))
                };
                policies.Add(policy);
            }

            reader.Close();
            connection.Close();

            response.StatusCode = 200;
            response.StatusMessage = "Policies Retrieved Successfully";
            response.Policies = policies;

            return response;
        }

        public UpdateInsuranceResponse UpdatePolicy(SqlConnection connection, int policyNumber, InsurancePolicy policy)
        {
            UpdateInsuranceResponse response = new UpdateInsuranceResponse();

            SqlCommand cmd = new SqlCommand("UPDATE InsurancePolicy SET PolicyHolderFullName = @PolicyHolderFullName, PolicyHolderDateOfBirth = @PolicyHolderDateOfBirth, PolicyHolderAddress = @PolicyHolderAddress, PolicyHolderPhoneNumber = @PolicyHolderPhoneNumber, Type = @Type, Premium = @Premium, StartDate = @StartDate, EndDate = @EndDate WHERE PolicyNumber = @PolicyNumber", connection);

            // Add parameters
            cmd.Parameters.AddWithValue("@PolicyNumber", policyNumber);
            cmd.Parameters.AddWithValue("@PolicyHolderFullName", policy.PolicyHolder.FullName);
            cmd.Parameters.AddWithValue("@PolicyHolderDateOfBirth", policy.PolicyHolder.DateOfBirth);
            cmd.Parameters.AddWithValue("@PolicyHolderAddress", policy.PolicyHolder.Address);
            cmd.Parameters.AddWithValue("@PolicyHolderPhoneNumber", policy.PolicyHolder.PhoneNumber);
            cmd.Parameters.AddWithValue("@Type", policy.Type.ToString());
            cmd.Parameters.AddWithValue("@Premium", policy.Premium);
            cmd.Parameters.AddWithValue("@StartDate", policy.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", policy.EndDate);

            connection.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            connection.Close();

            if (rowsAffected > 0)
            {
                // GetPolicyByNumberResponse updatedPolicyResponse = GetPolicyByNumber(connection, policyNumber);

                response.StatusCode = 200;
                response.StatusMessage = "Policy Updated Successfully";
                // response.UpdatedPolicy = updatedPolicyResponse.Policy;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Failed to update policy";
            }

            return response;
        }

        public DeleteInsuranceResponse DeletePolicy(SqlConnection connection, int policyNumber)
        {
            DeleteInsuranceResponse response = new DeleteInsuranceResponse();

            SqlCommand cmd = new SqlCommand("DELETE FROM InsurancePolicy WHERE PolicyNumber = @PolicyNumber", connection);
            cmd.Parameters.AddWithValue("@PolicyNumber", policyNumber);

            connection.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            connection.Close();

            if (rowsAffected > 0)
            {
                GetAllPoliciesResponse getAllPolicies = GetAllPolicies(connection);
                response.StatusCode = 200;
                response.StatusMessage = "Policy Deleted Successfully";

            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Failed to delete policy";
            }

            return response;
        }

        public GetPolicyByNumberResponse GetPolicyByPolicyNumber(SqlConnection connection, int policyNumber)
        {
            GetPolicyByNumberResponse response = new GetPolicyByNumberResponse();

            SqlCommand cmd = new SqlCommand("SELECT * FROM InsurancePolicy WHERE PolicyNumber = @PolicyNumber", connection);
            cmd.Parameters.AddWithValue("@PolicyNumber", policyNumber);

            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                InsurancePolicy policy = new InsurancePolicy
                {
                    PolicyNumber = reader.GetInt32(reader.GetOrdinal("PolicyNumber")),
                    PolicyHolder = new PolicyHolder
                    {
                        FullName = reader.GetString(reader.GetOrdinal("PolicyHolderFullName")),
                        DateOfBirth = reader.GetDateTime(reader.GetOrdinal("PolicyHolderDateOfBirth")),
                        Address = reader.GetString(reader.GetOrdinal("PolicyHolderAddress")),
                        PhoneNumber = reader.GetString(reader.GetOrdinal("PolicyHolderPhoneNumber"))
                    },
                    Type = (InsuranceType)Enum.Parse(typeof(InsuranceType), reader.GetString(reader.GetOrdinal("Type"))),
                    Premium = reader.GetDouble(reader.GetOrdinal("Premium")),
                    StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                    EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate"))
                };
                response.Policy = policy;
                response.StatusCode = 200;
                response.StatusMessage = "Policy Retrieved Successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Policy Not Found";
            }

            reader.Close();
            connection.Close();

            return response;
        }
    }
}
