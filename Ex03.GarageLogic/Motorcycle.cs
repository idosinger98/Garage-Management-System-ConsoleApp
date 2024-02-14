using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private const float k_MotorcycleMaxBatteryTime = 2.5f;
        private const float k_MotorcycleMaxFuelQuantity = 6.2f;
        private const eFuelType k_MotorcycleFuelType = eFuelType.Octan98;
        private const int k_MaxWheelsPressure = 31;
        private const int k_NumberOfWheels = 2;
        private readonly eEngineType r_EngineType;
        private eMotorcycleLicenseType m_LicenseType;
        private int m_EngineVolume;

        public Motorcycle(eEngineType i_EngineType)
        {
            m_LicenseType = 0;
            m_EngineVolume = 0;
            r_EngineType = i_EngineType;
            this.m_Engine = BuildEngine();
        }

        private enum eMotorcycleLicenseType
        {
            A = 1,
            A1,
            B1,
            Bb
        }

        public override void GetVehicleParameter(ref List<string> io_VehicleParameters)
        {
            base.GetVehicleParameter(ref io_VehicleParameters);
            io_VehicleParameters.Add("license type");
            io_VehicleParameters.Add("engine volume");
        }

        private void checkLicenseType(string i_LicenseType)
        {
            bool isValid = Enum.TryParse(i_LicenseType, out m_LicenseType);
            int counter = 0;

            if (isValid == false)
            {
                throw new FormatException("invalid input");
            }

            isValid = false;
            foreach (eMotorcycleLicenseType type in Enum.GetValues(typeof(eMotorcycleLicenseType)))
            {
                if (type == m_LicenseType)
                {
                    isValid = true;
                }

                counter++;
            }

            if (isValid == false)
            {
                throw new ValueOutOfRangeException(1, counter, "option");
            }
        }

        private void checkEngineVolume(string i_EngineVolume)
        {
            bool isValid = int.TryParse(i_EngineVolume, out m_EngineVolume);

            if (isValid == false)
            {
                throw new FormatException("invalid input");
            }
        }

        public override void CheckVehicleWheels(string i_AirPressure, float i_MaxWheelsPressure)
        {
            this.CheckWheelsAirPressureAndCreateThem(i_AirPressure, i_MaxWheelsPressure, k_NumberOfWheels);
        }

        public override Engine BuildEngine()
        {
            Engine engine = null;

            if (r_EngineType == eEngineType.Fuel)
            {
                engine = new FuelEngine(k_MotorcycleFuelType, k_MotorcycleMaxFuelQuantity);
            }
            else if (r_EngineType == eEngineType.Electric)
            {
                engine = new ElectricEngine(k_MotorcycleMaxBatteryTime);
            }

            return engine;
        }

        public override string CheckIfEnumParameter(string i_Parameter)
        {
            string[] names = null;

            if (i_Parameter == "license type")
            {
                names = Enum.GetNames(typeof(eMotorcycleLicenseType));
            }

            return GetStringOfEnums(names);
        }

        public override void CheckParameter(string i_ParameterType, string i_Parameter, Garage i_Garage)
        {
            if (i_Parameter == string.Empty)
            {
                throw new FormatException("Error, you must enter a value ");
            }
            else if (i_ParameterType == "license type")
            {
                checkLicenseType(i_Parameter);
            }
            else if (i_ParameterType == "engine volume")
            {
                checkEngineVolume(i_Parameter);
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

        public override string GetFullInformation()
        {
            string electricMotorcycleInformation = string.Format(
@"Motorcycle:
Engine type: {0}
{1}
License type: {2}
Engine volume: {3}",
r_EngineType.ToString(),
this.ToString(),
m_LicenseType,
m_EngineVolume);

            return electricMotorcycleInformation;
        }
    }
}
