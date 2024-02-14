using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleBuilder
    {
        public enum eVehiclesInSystem
        {
            FuelCar = 1,
            ElectricCar,
            FuelMotorcycle,
            ElectricMotorcycle,
            FuelTruck
        }

        public static Vehicle CreateNewVehicle(eVehiclesInSystem i_NewVehicle)
        {
            Vehicle newVehicle = null;

            switch(i_NewVehicle)
            {
                case eVehiclesInSystem.FuelCar:
                    newVehicle = new Car(eEngineType.Fuel);
                    break;
                case eVehiclesInSystem.ElectricCar:
                    newVehicle = new Car(eEngineType.Electric);
                    break;
                case eVehiclesInSystem.FuelMotorcycle:
                    newVehicle = new Motorcycle(eEngineType.Fuel);
                    break;
                case eVehiclesInSystem.ElectricMotorcycle:
                    newVehicle = new Motorcycle(eEngineType.Electric);
                    break;
                case eVehiclesInSystem.FuelTruck:
                    newVehicle = new Truck(eEngineType.Fuel);
                    break;
                default:
                    throw new ArgumentException("Error, vehicle not support in system");
            }

            return newVehicle;
        }
    }
}
