using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EMS2.Models
{
    public class PatientDAL
    {
        static private PatientDAL instance;
        static private readonly object mutex = new object();
        private string connectionString;
        private PatientDAL(string strConn)
        {
            connectionString = strConn;

        }
        static public PatientDAL GetInst(string connectionString)
        {
            lock(mutex)
            {
                if (instance == null)
                {
                    instance = new PatientDAL(connectionString);
                }
            }
            
            return instance;
        }
        public void Create(Patient patient)
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                // Do something here
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        public void Update(Patient patient)
        {

        }
        /*public IEnumerable<Patient> GetAll()
        {

        }
        public Patient Get()
        {

        }*/
        public void Delete(string ID)
        {

        }

    }
}
