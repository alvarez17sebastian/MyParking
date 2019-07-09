using System;
using System.Collections.Generic;
using MyParking.Core.DependencyInjection;
using Parking.core.Dto;
using Parking.Core.Constants;
using Parking.Core.CustomExceptions;
using Parking.Core.Helpers;
using Parking.Core.Repository;

namespace Parking.Core.DomainModels
{
    public class ParkingLot
    {
        private IVehicleDatabaseRepository vehicleDatabaseRepository;

        public ParkingLot()
        {
            //this.vehicleDatabaseRepository = vehicleDatabaseRepository;
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

        public int CalculatePaymentVehicle(VehicleDto vehicleDto)
        {
            int numberOfHours = DateHelper.GetHours(vehicleDto.DateOfEntry);
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
                if (DateHelper.GetCurrentDay(dateOfEntry) != DateConstants.SundayDay && DateHelper.GetCurrentDay(dateOfEntry) != DateConstants.MondayDay)
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

        private bool CheckAvailabilityCars()
        {
            int numberOfCars = vehicleDatabaseRepository.GetVehiclesForType(ParkingConstants.CarType).Count;
            return numberOfCars <= ParkingConstants.AvailableCellCar;
        }

        private bool CheckAvailabilityMotorBike()
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
