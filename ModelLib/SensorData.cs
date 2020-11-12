using System;

namespace ModelLib
{
    public class SensorData
    {
        public SensorData(int id, string name, int temperature, int cO2)
        {
            Id = id;
            SensorName = name;
            Temperature = temperature;
            CO2 = cO2;
        }

        public SensorData()
        {
        }

        public int Id { get; set; }
        public string SensorName { get; set; }
        public int Temperature { get; set; }
        public int CO2 { get; set; }

        public override string ToString()
        {
            return $"{SensorName};{nameof(Temperature)}:{Temperature},{nameof(CO2)}: {CO2}";
        }
    }
}
