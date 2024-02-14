using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const float k_TruckMaxFuelQuantity = 120;
        private const int k_MaxWheelsPressure = 24;
        private const int k_NumberOfWheels = 16;
        private const eFuelType k_TruckFuelType = eFuelType.Soler;
        private readonly eEngineType r_EngineType;
        private bool m_TransportCargoWithCooling;
        private float m_CargoVolume;

        public Truck(eEngineType i_EngineType)
        {
            m_TransportCargoWithCooling = false;
            m_CargoVolume = 0;
            r_EngineType = i_EngineType;
            this.m_Engine = BuildEngine();
        }

        public bool TransportCargoWithCooling
        {
            get { return m_TransportCargoWithCooling; }
            set { m_TransportCargoWithCooling = value; }
        }

        public float CargoVolume
        {
            get { return m_CargoVolume; }
            set { m_CargoVolume = value; }
        }

        public override void GetVehicleParameter(ref List<string> io_VehicleParameters)
        {
            base.GetVehicleParameter(ref io_VehicleParameters);
            io_VehicleParameters.Add("is cargo with cooling true/false");
            io_VehicleParameters.Add("cargo volume");
        }

        private void checkCargoInCooling(string i_CargoInCooling)
        {
            bool isValid = bool.TryParse(i_CargoInCooling, out m_TransportCargoWithCooling);

            if (isValid == false)
            {
                throw new FormatException("invalid input");
            }
        }

        private void checkCargoVolume(string i_CargoVolume)
        {
            bool isValid = float.TryParse(i_CargoVolume, out m_CargoVolume);

            if (isValid == false)
            {
                throw new FormatException("invalid input");
            }
        }

        public override void CheckVehicleWheels(string i_AirPressure, float i_MaxWheelsPressure)
        {
            this.CheckWheelsAirPressureAndCreateThem(i_AirPressure, i_MaxWheelsPressure, k_NumberOfWheels);
        }

        public override void CheckParameter(string i_ParameterType, string i_Parameter, Garage i_Garage)
        {
            if (i_Parameter == string.Empty)
            {
                throw new FormatException("Error, you must enter a value");
            }
            else if (i_ParameterType == "is cargo with cooling true/false")
            {
                checkCargoInCooling(i_Parameter);
            }
            else if (i_ParameterType == "cargo volume")
            {
                checkCargoVolume(i_Parameter);
            }
            else if (i_ParameterType == "wheels air pressure")
            {
                CheckVehicleWheels(i_Parameter, k_MaxWheelsPressure);
            }
            else
            {
                base.CheckParameter(i_ParameterType, i_Parameter, i_Garage);
            }
        }

        public override string CheckIfEnumParameter(string i_Parameter)
        {
            string[] names = null;

            return GetStringOfEnums(names);
        }

        public override Engine BuildEngine()
        {
            Engine engine = null;

            if (r_EngineType == eEngineType.Fuel)
            {
                engine = new FuelEngine(k_TruckFuelType, k_TruckMaxFuelQuantity);
            }

            return engine;
        }

        public override string GetFullInformation()
        {
            string fuelTrackInformation = string.Format(
@"Truck:
Engine type: {0}
{1}
Transport cargo with cooling: {2}
Cargo volume: {3}",
r_EngineType.ToString(),
this.ToString(),
m_TransportCargoWithCooling,
m_CargoVolume);

            return fuelTrackInformation;
        }
    }
}
