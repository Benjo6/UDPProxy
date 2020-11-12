using ModelLib;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SensorREST.DBUTil
{
    public class ManagerSensor
    {
        private const string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SensorDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private const string GET_ALL_SQL = "select * from Sensor";
        public IList<SensorData> HentAlle()
        {
            IList<SensorData> sensors = new List<SensorData>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(GET_ALL_SQL, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        sensors.Add(ReadNextSensor(reader));
                    }
                }
            }
            return sensors;

        }
        private const string GET_ONE_SQL = "select * from Sensor where Id = @Id";
        public SensorData HentEn(int id)
        {
            SensorData sensor = new SensorData();
            
            using(SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using(SqlCommand cmd = new SqlCommand(GET_ONE_SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        sensor = ReadNextSensor(reader);
                    }
                }
            }
            return sensor;
        }
        private const string INSERT_SQL = "insert into Sensor(Name, Temperature, CO2) values (@Name, @Temperature, @CO2)";
        public bool OpretSensor(SensorData sensor)
        {
            bool OK = true;

            using(SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using(SqlCommand cmd = new SqlCommand(INSERT_SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", sensor.SensorName);
                    cmd.Parameters.AddWithValue("@Temperature", sensor.Temperature);
                    cmd.Parameters.AddWithValue("@CO2", sensor.CO2);

                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        OK = rows == 1;
                    }
                    catch(Exception ex)
                    {
                        OK = false;
                    }
                }
            }
            return OK;
        }
        private SensorData ReadNextSensor(SqlDataReader reader)
        {
            SensorData sensor = new SensorData();

            sensor.Id = reader.GetInt32(0);
            sensor.SensorName = reader.GetString(1);
            sensor.Temperature = reader.GetInt32(2);
            sensor.CO2 = reader.GetInt32(3);

            return sensor;
        }

    }
}
