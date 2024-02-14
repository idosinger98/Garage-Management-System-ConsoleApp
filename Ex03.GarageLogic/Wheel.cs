using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly string r_ManufacturerName;
        private readonly float r_MaxAirPressure;
        private float m_CurrentAirPressure;

        public Wheel(string i_ManufacturerName, float i_AirPressure, float i_MaxAirPressure)
        {
            r_ManufacturerName = i_ManufacturerName;
            m_CurrentAirPressure = i_AirPressure;
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }
        }

        public string ManufacturerName
        {
            get { return r_ManufacturerName; }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
        }

        public void AddPressureToWheel(int i_AmountOfPressureToAdd)
        {
            bool pressureNotInRange = m_CurrentAirPressure + i_AmountOfPressureToAdd > r_MaxAirPressure;

            if (i_AmountOfPressureToAdd < 0) 
            {
                throw new ArgumentException("Error, air pressure to add must be positive");
            }

            if (pressureNotInRange == false)
            {
                m_CurrentAirPressure += i_AmountOfPressureToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(0, r_MaxAirPressure, "Wheels air pressure");
            }
        }

        public void BlowUpToMaxPressure()
        {
            m_CurrentAirPressure = r_MaxAirPressure;
        }

        public override string ToString()
        {
            string wheelsFullInformation = string.Format(
@"Wheels manufacturer: {0} 
Air pressure: {1}",
r_ManufacturerName,
m_CurrentAirPressure);

            return wheelsFullInformation;
        }
    }
}
