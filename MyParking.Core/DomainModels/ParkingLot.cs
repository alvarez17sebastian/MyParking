using System;
using System.Collections.Generic;
using MyParking.core.Dto;
using MyParking.Core.Constants;
using MyParking.Core.CustomExceptions;
using MyParking.Core.DependencyInjection;
using MyParking.Core.Helpers;
using MyParking.Core.Repository;

namespace MyParking.Core.DomainModels
{
    public class ParkingLot
    {
        private readonly IVehicleDatabaseRepository vehicleDatabaseRepository;

        public ParkingLot()
        {
            this.vehicleDatabaseRepository = ServiceLocator.Get<IVehicleDatabaseRepository>();
        }

        public bool RegisterCheckInVehicle(VehicleDto vehicleDto)
        {
            CheckAvailableCell(vehicleDto.Type);
            CheckIfCanTheVehicleEnterForLicensePlate(vehicleDto.Plate, vehicleDto.DateOfEntry);
            return vehicleDatabaseRepository.SaveVehicle(vehicleDto);
        }

        public bool RegisterCheckoutVehicle(VehicleDto vehicleDto)
        {
            bool resultDelete = vehicleDatabaseRepository.DeleteVehicle(vehicleDto);
            return resultDelete;
        }

        public bool DeleteVehicle(VehicleDto vehicleDto)
        {
            return vehicleDatabaseRepository.DeleteVehicle(vehicleDto);
        }

        public int CalculatePaymentVehicle(VehicleDto vehicleDto)
        {
            int numberOfHours = CurrentDateManagement.GetHours(vehicleDto.DateOfEntry);
            return CalculateValueToPay(numberOfHours, vehicleDto);
        }

        public List<VehicleDto> GetAllVehicles()
        {
            List<VehicleDto> vehiclesDto = vehicleDatabaseRepository.GetAllVehicles();
            return vehiclesDto;
        }

        private bool CheckIfCanTheVehicleEnterForLicensePlate(string licensePlate, DateTimeOffset dateOfEntry)
        {
            if (VerifyStartWordLicensePlate(licensePlate))
            {
                if (CurrentDateManagement.GetCurrentDay(dateOfEntry) != DateConstants.SundayDay && CurrentDateManagement.GetCurrentDay(dateOfEntry) != DateConstants.MondayDay)
                {
                    throw new ParkingDomainBusinessException(MessageConstants.NotAuthorized);
                }
            }

            return true;
        }

        private bool VerifyStartWordLicensePlate(string licensePlate)
        {
            bool resultOfVerifyLicensePlate = licensePlate.StartsWith(ParkingConstants.StartWord);
            return resultOfVerifyLicensePlate;
        }

        private bool CheckAvailableCell(string type)
        {
            if (type.Equals(ParkingConstants.CarType))
            {
                return CheckAvailabilityCars();
            }
            else if (type.Equals(ParkingConstants.MotorBikeType))
            {
                return CheckAvailabilityMotorBike();
            }
            else
            {
                throw new ParkingDomainBusinessException(MessageConstants.TypeVehicleNoExist);
            }
        }

        public bool CheckAvailabilityCars()
        {
            int numberOfCars = vehicleDatabaseRepository.GetVehiclesForType(ParkingConstants.CarType).Count;
            return numberOfCars <= ParkingConstants.AvailableCellCar;
        }

        public bool CheckAvailabilityMotorBike()
        {
            int numberOfMotorBike = vehicleDatabaseRepository.GetVehiclesForType(ParkingConstants.MotorBikeType).Count;
            return numberOfMotorBike <= ParkingConstants.AvailableCellMotorBike;
        }

        private int CalculateValueToPay(int numberHours, VehicleDto vehicleDto)
        {
            int payment = 0;
            if (ParkingConstants.CarType.Equals(vehicleDto.Type))
            {
                payment = CalculatePayForTimeInHoursOrDays(numberHours, ParkingConstants.CostForEachDayOfTheCar, ParkingConstants.CostForEachHourOfTheCar);
            }
            else if (ParkingConstants.MotorBikeType.Equals(vehicleDto.Type))
            {
                payment = CalculatePayForTimeInHoursOrDays(numberHours, ParkingConstants.CostForEachDayOfTheMotorCycle, ParkingConstants.CostForEachHourOfTheMotorCycle);
                if (CheckDisplacementMotorCycle(vehicleDto.Displacement))
                {
                    payment += ParkingConstants.AditionalCost;
                }
            }
            return payment;
        }

        private int CalculatePayForTimeInHoursOrDays(int numberHours, int costForDay, int costForHour)
        {
            int payment;
            if (numberHours < ParkingConstants.RuleToChargeDay)
            {
                payment = numberHours * costForHour;
            }
            else if (numberHours >= ParkingConstants.RuleToChargeDay && numberHours <= ParkingConstants.DayHours)
            {
                payment = costForDay;
            }
            else
            {
                int days = numberHours / ParkingConstants.DayHours;
                int hours = numberHours % ParkingConstants.DayHours;

                payment = days * costForDay + hours * costForHour;
            }
            return payment;
        }

        private bool CheckDisplacementMotorCycle(int displacement)
        {
            if (displacement > ParkingConstants.DisplacmentMotorbike)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public VehicleDto GetVehicle(string plate)
        {
            return null;
        }
    }
}
