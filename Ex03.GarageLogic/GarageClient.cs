using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class GarageClient
    {
        private readonly string r_OwnersName;
        private readonly string r_OwnersPhoneNumber;
        private eGarageVehicleStatus m_Status;
        private Vehicle m_Vehicel;

        public GarageClient(Vehicle i_Vehicel, string i_OwnersName, string i_OwnersPhoneNumber)
        {
            r_OwnersName = i_OwnersName;
            r_OwnersPhoneNumber = i_OwnersPhoneNumber;
            m_Status = eGarageVehicleStatus.InProgress;
            m_Vehicel = i_Vehicel;
        }

        public eGarageVehicleStatus VehicleStatus
        {
            get { return m_Status; }
            set { m_Status = value; }
        }

        public void BlowUpTiresToMaxPressure()
        {
            m_Vehicel.BlowUpTirePressureToMax();
        }

        public void CheckEnergyToAddInRange(float i_AmountOfEnergy)
        {
            FuelEngine fuelEngine = m_Vehicel.GetEngine as FuelEngine;
            ElectricEngine electricEngine = m_Vehicel.GetEngine as ElectricEngine;
            float maxEnergyToAdd = 0;

            if (fuelEngine != null)
            {
                if(fuelEngine.EnergyLeft + i_AmountOfEnergy > fuelEngine.MaxEnergyCapacity)
                {
                    maxEnergyToAdd = fuelEngine.MaxEnergyCapacity - fuelEngine.EnergyLeft;
                    throw new ValueOutOfRangeException(0, maxEnergyToAdd, "amount of fuel you can add in liters to this vehicle");
                }
            }
            else if(electricEngine != null)
            {
                if (electricEngine.EnergyLeft + (i_AmountOfEnergy / 60) > electricEngine.MaxEnergyCapacity) 
                {
                    maxEnergyToAdd = (electricEngine.MaxEnergyCapacity - electricEngine.EnergyLeft) * 60;
                    throw new ValueOutOfRangeException(0, maxEnergyToAdd, "amount of minutes you can charge this vehicle");
                }
            }
        }

        public bool IsClientVehicleRunsOnFuel()
        {
            bool isFuelVehicle = false;
            FuelEngine fuelEngine = m_Vehicel.GetEngine as FuelEngine;

            if (fuelEngine != null)
            {
                isFuelVehicle = true;
            }

            return isFuelVehicle;
        }

        public void AddFuel(eFuelType i_FuelType, float i_AmountOfFuel)
        {
            FuelEngine fuelEngine = m_Vehicel.GetEngine as FuelEngine;

            if (fuelEngine != null)
            {
                if(fuelEngine.FuelType == i_FuelType)
                {
                    m_Vehicel.AddEnergy(i_AmountOfFuel);
                }
                else
                {
                    throw new ArgumentException("Error, fuel not match the vehicle");
                }
            }
            else
            {
                throw new ArgumentException("Error, vehicle is not run on fuel");
            }
        }

        public void ChargeBattery(float i_AmountOfMinutes)
        {
            ElectricEngine electricEngine = m_Vehicel.GetEngine as ElectricEngine;

            if (electricEngine != null)
            {
                m_Vehicel.AddEnergy(i_AmountOfMinutes / 60);
            }
            else
            {
                throw new ArgumentException("Error , vehicle is not electric");
            }
        }

        public string GetVehicleInformation()
        {
            string clientVehicleInformation = string.Format(
@"Owner's name: {0}
Vehicle status: {1}
{2}",
r_OwnersName,
m_Status,
m_Vehicel.GetFullInformation());

            return clientVehicleInformation;
        }
    }
}
