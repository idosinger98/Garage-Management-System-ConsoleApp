using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private const float k_CarMaxBatteryTime = 3.3f;
        private const float k_CarMaxFuelQuantity = 38;
        private const eFuelType k_CarFuelType = eFuelType.Octan95;
        private const int k_MaxWheelsPressure = 29;
        private const int k_NumberOfWheels = 4;
        private readonly eEngineType r_EngineType;
        private eColor m_Color;
        private eNumOfDoors m_NumOfDoors;

        public Car(eEngineType i_EngineType)
        {
            r_EngineType = i_EngineType;
            m_Color = 0;
            m_NumOfDoors = 0;
            this.m_Engine = BuildEngine();
        }

        private enum eNumOfDoors
        {
            Two = 1,
            Three,
            Four,
            Five
        }

        private enum eColor
        {
            Red = 1,
            White,
            Green,
            Blue
        }

        public override void GetVehicleParameter(ref List<string> io_VehicleParameters)
        {
            base.GetVehicleParameter(ref io_VehicleParameters);
            io_VehicleParameters.Add("number of doors");
            io_VehicleParameters.Add("color");
        }

        private void checkNumberOfDoors(string i_NumberOfDoors)
        {
            int counter = 0;
            bool isValid = Enum.TryParse(i_NumberOfDoors, out m_NumOfDoors);

            if (isValid == false)
            {
                throw new FormatException("invalid input");
            }

            isValid = false;
            foreach (eNumOfDoors num in Enum.GetValues(typeof(eNumOfDoors)))
            {
                if (num == m_NumOfDoors)
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

        public override string CheckIfEnumParameter(string i_Parameter)
        {
            string[] names = null;

            if (i_Parameter == "number of doors")
            {
                names = Enum.GetNames(typeof(eNumOfDoors));
            }
            else if (i_Parameter == "color")
            {
                names = Enum.GetNames(typeof(eColor));
            }

            return GetStringOfEnums(names);
        }

        private void checkColor(string i_Color)
        {
            bool isValid = Enum.TryParse(i_Color, out m_Color);
            int counter = 0;

            if (isValid == false)
            {
                throw new FormatException("invalid input");
            }

            isValid = false;
            foreach (eColor color in Enum.GetValues(typeof(eColor)))
            {
                if (color == m_Color)
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

        public override void CheckVehicleWheels(string i_AirPressure, float i_MaxWheelsPressure)
        {
            this.CheckWheelsAirPressureAndCreateThem(i_AirPressure, i_MaxWheelsPressure, k_NumberOfWheels);
        }

        public override void CheckParameter(string i_ParameterType, string i_Parameter, Garage i_Garage)
        {
            if (i_Parameter == string.Empty)
            {
                throw new FormatException("Error, you must enter a value ");
            }
            else if (i_ParameterType == "number of doors")
            {
                checkNumberOfDoors(i_Parameter);
            }
            else if (i_ParameterType == "color")
            {
                checkColor(i_Parameter);
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

        public override Engine BuildEngine()
        {
            Engine engine = null;

            if (r_EngineType == eEngineType.Fuel) 
            {
                engine = new FuelEngine(k_CarFuelType, k_CarMaxFuelQuantity);
            }
            else if(r_EngineType == eEngineType.Electric)
            {
                engine = new ElectricEngine(k_CarMaxBatteryTime);
            }

            return engine;
        }

        public override string GetFullInformation()
        {
            string fuelCarInformation = string.Format(
@"Car:
Engine type: {0}
{1}
Car's Color: {2}
Number of doors: {3}",
r_EngineType.ToString(),
this.ToString(),
m_Color,
m_NumOfDoors);

            return fuelCarInformation;
        }
    }
}
