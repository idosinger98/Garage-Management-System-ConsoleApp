using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected string m_Model;
        protected string m_LicenseNumber;
        protected float m_WheelsAirPressure;
        protected string m_WheelsManufacturerName;
        protected Engine m_Engine;
        protected int m_NumberOfWheels;
        protected List<Wheel> m_Wheels;

        protected Vehicle()
        {
            m_Model = string.Empty;
            m_LicenseNumber = string.Empty;
            m_WheelsManufacturerName = string.Empty;
            m_Wheels = null;
            m_NumberOfWheels = 0;
            m_WheelsAirPressure = 0;
            m_Engine = null;
        }

        public List<Wheel> Wheels
        {
            get { return m_Wheels; }
            set { m_Wheels = value; }
        }

        public string Model
        {
            get { return m_Model; }
        }

        public float WheelsAirPressure
        {
            get { return m_WheelsAirPressure; }
            set { m_WheelsAirPressure = value; }
        }

        public string LicenseNumber
        {
            get { return m_LicenseNumber; }
        }

        public int NumberOfWheels
        {
            get { return m_NumberOfWheels; }
        }

        public void BlowUpTirePressureToMax()
        {
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.BlowUpToMaxPressure();
            }
        }

        public Engine GetEngine
        {
            get { return m_Engine; }
            set { m_Engine = value; }
        }

        public bool CheckWheelsRange(float i_MaxAirPressure, float i_WheelsAirPressure)
        {
            bool inRange = false;

            if (i_WheelsAirPressure <= i_MaxAirPressure && i_WheelsAirPressure >= 0) 
            {
                inRange = true;
            }
            else
            {
                throw new ValueOutOfRangeException(0, i_MaxAirPressure, "wheels air pressure");
            }

            return inRange;
        }

        public virtual void GetVehicleParameter(ref List<string> io_VehicleParameters)
        {
            io_VehicleParameters.Add("model");
            io_VehicleParameters.Add("license number");
            io_VehicleParameters.Add("amount of energy left");
            io_VehicleParameters.Add("wheels manufacturer name");
            io_VehicleParameters.Add("wheels air pressure");
        }

        public void AddEnergy(float i_AmountOfEnergy)
        {
            m_Engine.AddEnergy(i_AmountOfEnergy);
        }

        public void CheckWheelsAirPressureAndCreateThem(string i_AirPressure, float i_MaxWheelsPressure, int i_NumberOfWheels)
        {
            float wheelsAirPressure = 0;
            bool isValid = float.TryParse(i_AirPressure, out wheelsAirPressure);

            if (isValid == false)
            {
                throw new FormatException("invalid input");
            }
            else if (CheckWheelsRange(i_MaxWheelsPressure, wheelsAirPressure))
            {
                m_WheelsAirPressure = wheelsAirPressure;
                createVehicleWheels(i_NumberOfWheels, i_MaxWheelsPressure);
            }
        }

        private void createVehicleWheels(int i_NumberOfWheels, float i_MaxWheelsPressure)
        {
            m_Wheels = new List<Wheel>();

            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                m_Wheels.Add(new Wheel(m_WheelsManufacturerName, m_WheelsAirPressure, i_MaxWheelsPressure));
            }

            m_NumberOfWheels = i_NumberOfWheels;
        }

        public string GetStringOfEnums(string[] i_Enums)
        {
            string stringOfEnums = string.Empty;
            int counter = 1;

            if (i_Enums != null)
            {
                foreach (string name in i_Enums)
                {
                    stringOfEnums += counter.ToString() + ". " + separateWords(name) + Environment.NewLine;
                    counter++;
                }
            }

            return stringOfEnums;
        }

        private string separateWords(string i_Line)
        {
            StringBuilder newLine = new StringBuilder();
            int i = 0;

            while (i < i_Line.Length)
            {
                if (char.IsUpper(i_Line[i]) == true)
                {
                    newLine.Append(' ');
                }

                newLine.Append(i_Line[i]);
                i++;
            }

            return newLine.ToString();
        }

        public abstract void CheckVehicleWheels(string i_AirPressure, float i_MaxWheelsPressure);

        public abstract string CheckIfEnumParameter(string i_Parameter);

        public abstract Engine BuildEngine();

        private void checkModel(string i_Model)
        {
            for(int i = 0; i < i_Model.Length; i++)
            {
                if(char.IsLetterOrDigit(i_Model[i]) == false && i_Model[i] != ' ')
                {
                    throw new FormatException("Model should contain only letters and number");
                }
            }

            m_Model = i_Model;
        }

        private void checkLicenseNumber(string i_LicenseNumber, Garage i_Garage)
        {
            for (int i = 0; i < i_LicenseNumber.Length; i++)
            {
                if (char.IsLetterOrDigit(i_LicenseNumber[i]) == false)
                {
                    throw new FormatException("license number should contain only letters and number");
                }
            }

            if(i_Garage.IsVehicleInGarage(i_LicenseNumber) == true)
            {
                throw new ArgumentException("Error, this vehicle is already exist in the garage");
            }

            m_LicenseNumber = i_LicenseNumber;
        }

        private void checkWheelsManufacturerName(string i_ManufacturerName)
        {
            for (int i = 0; i < i_ManufacturerName.Length; i++)
            {
                if (char.IsLetter(i_ManufacturerName[i]) == false)
                {
                    throw new FormatException("Wheels manufacturer name should contain only letters");
                }
            }

            m_WheelsManufacturerName = i_ManufacturerName;
        }

        public virtual void CheckParameter(string i_ParameterType, string i_Parameter, Garage i_Garage)
        {
            if (i_Parameter == string.Empty)
            {
                throw new FormatException("Error, you must enter a value");
            }
            else if (i_ParameterType == "model")
            {
                checkModel(i_Parameter);
            }
            else if (i_ParameterType == "license number")
            {
                checkLicenseNumber(i_Parameter, i_Garage);
            }
            else if (i_ParameterType == "wheels manufacturer name")
            {
                checkWheelsManufacturerName(i_Parameter);
            }
            else if(i_ParameterType == "amount of energy left")
            {
                checkEnergyTimeLeft(i_Parameter);
            }
        }

        private void checkEnergyTimeLeft(string i_EnergyTimeLeft)
        {
            float energyLeft = 0;
            bool isValid = float.TryParse(i_EnergyTimeLeft, out energyLeft);

            if (isValid == false)
            {
                throw new FormatException("input must be float type");
            }
            else if (energyLeft > m_Engine.MaxEnergyCapacity || energyLeft < 0)
            {
                throw new ValueOutOfRangeException(0, m_Engine.MaxEnergyCapacity, "Energy");
            }

            m_Engine.EnergyLeft = energyLeft;
        }

        public abstract string GetFullInformation();

        public override string ToString()
        {
            string fullVehicleInformation = string.Format(
@"License number: {0} 
Model name: {1} 
Number of wheels: {2} 
{3}
Energy remaining: {4}%
Max energy capacity: {5}",
m_LicenseNumber,
m_Model,
m_NumberOfWheels,
m_Wheels[0].ToString(),
m_Engine.PercentageOfEnergyLeft,
m_Engine.MaxEnergyCapacity);

            return fullVehicleInformation;
        }
    }
}
