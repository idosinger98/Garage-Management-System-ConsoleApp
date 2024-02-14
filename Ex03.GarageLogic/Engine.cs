using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        private readonly float r_MaxEnergyCapacity;
        protected float m_EnergyLeft;

        protected Engine(float i_MaxEnergyCapacity)
        {
            m_EnergyLeft = 0;
            r_MaxEnergyCapacity = i_MaxEnergyCapacity;
        }

        public float EnergyLeft
        {
            get { return m_EnergyLeft; }
            set { m_EnergyLeft = value; }
        }

        public float PercentageOfEnergyLeft
        {
            get 
            {
                return m_EnergyLeft / r_MaxEnergyCapacity * 100;
            }
        }

        public float MaxEnergyCapacity
        {
            get { return r_MaxEnergyCapacity; }
        }

        public float AddEnergy(float i_AmountToAdd)
        {
            bool isAmountInRange = i_AmountToAdd + m_EnergyLeft <= r_MaxEnergyCapacity;

            if (i_AmountToAdd < 0)
            {
                throw new ArgumentException("Error, amount of energy must be positive");
            }

            if (isAmountInRange == true)
            {
                m_EnergyLeft += i_AmountToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(0, r_MaxEnergyCapacity, "energy capacity");
            }

            return m_EnergyLeft / r_MaxEnergyCapacity * 100;
        }
    }
}
