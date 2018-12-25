using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace P4Papi.Repository.StoredProcedures
{
    public class GetDataStoredProcedures
    {
        private string ConnectionStrings { get; set; }
        private string StoredProcedures { get; set; }

        private string DefaultStoredProcedures = "P4P_POINT_PROCEDURE";
        public GetDataStoredProcedures()
        {
            ConnectionStrings = ConfigurationManager.ConnectionStrings["PfourPSP"].ConnectionString;
            StoredProcedures = DefaultStoredProcedures;
        }

        public GetDataStoredProcedures(string connectionStringName,string storedProcedureName)
        {
            string connectionString = "PfourPSP";
            if (!string.IsNullOrEmpty(connectionStringName))
            {
                connectionString = connectionStringName;
            }
            ConnectionStrings = ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;

            if (!string.IsNullOrEmpty(storedProcedureName))
            {
                StoredProcedures = storedProcedureName;
            }
            else
            {
                StoredProcedures = DefaultStoredProcedures;
            }
        }

        public DataSet GetDataUserPoint(int? departmentId,int? positionId, int? SubdivisionId,int? userId,string ymStart, string ymEnd)
        {
            string procedureType = "REQUEST_POINT_LIST";
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures, con))
                {
                    SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                    adapt.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapt.SelectCommand.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = procedureType;
                    adapt.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = departmentId;
                    adapt.SelectCommand.Parameters.Add("@PositionId", SqlDbType.Int).Value = positionId;
                    adapt.SelectCommand.Parameters.Add("@SubdivisionId", SqlDbType.Int).Value = SubdivisionId;
                    adapt.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                    adapt.SelectCommand.Parameters.Add("@YMStart", SqlDbType.VarChar).Value = ymStart;
                    adapt.SelectCommand.Parameters.Add("@YMEnd", SqlDbType.VarChar).Value = ymEnd;

                    DataSet ds = new DataSet();
                    adapt.Fill(ds);
                    return ds;
                    //DataTable dt = ds.Tables[0];
                }
            }
        }

    }
}