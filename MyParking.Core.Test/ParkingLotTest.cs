using System;
using MyParking.core.Dto;
using MyParking.core.Repository.Mock;
using MyParking.Core.Constants;
using MyParking.Core.CustomExceptions;
using MyParking.Core.DependencyInjection;
using MyParking.Core.DomainModels;
using MyParking.Core.Repository;
using Xunit;

namespace MyParking.Core.Test
{
    public class ParkingLotTest
    {
        public ParkingLotTest()
        {
            ServiceLocator.SetupKernel();
        }

        private IVehicleDatabaseRepository repository = new MockRealmVehicleDatabaseManager();

        [Fact]
        public void RegisterVehicle_WithLicensePlateNotStartWithChar_A_RegisterSucecessful_Test()
        {
            //Arrange
            string licensePlate = "BGP 922";
            string typeVehicle = "Car";
            int displacement = 1600;
            DateTime dateOfEntry = DateTime.Now;
            ParkingLot parking = ServiceLocator.Get<ParkingLot>();
            VehicleDto vehicle = new VehicleDto(licensePlate, typeVehicle, displacement, dateOfEntry);

            //Act
            bool registerResponse = parking.RegisterCheckInVehicle(vehicle);

            //Assert
            Assert.True(registerResponse);

        }

        [Fact]
        public void RegisterVehicle_WithTuesdayDayAndLicensePlateStartWithChar_A_RegisterFail_Test()
        {
            //Arrange
            string licensePlate = "AGP 922";
            string typeVehicle = "Car";
            int displacement = 1600;
            string strDateOfEntry = "04-09-2019 09:00 AM";
            DateTime dateOfEntry = Convert.ToDateTime(strDateOfEntry);
            ParkingLot parking = ServiceLocator.Get<ParkingLot>();
            VehicleDto vehicle = new VehicleDto(licensePlate, typeVehicle, displacement, dateOfEntry);

            //Act & Assert
            Assert.Throws<ParkingDomainBusinessException>(() => parking.RegisterCheckInVehicle(vehicle));
        }


        [Fact]
        public void RegisterVechicle_WithAvailableCarCells_RegisterSuccessful_Test()
        {
            //Arrange
            string licensePlate = "BGP 922";
            string typeVehicle = "Car";
            int displacement = 1600;
            DateTime dateOfEntry = DateTime.Now;
            ParkingLot parking = ServiceLocator.Get<ParkingLot>();
            VehicleDto vehicle = new VehicleDto(licensePlate, typeVehicle, displacement, dateOfEntry);

            //Act
            bool registerResponse = parking.RegisterCheckInVehicle(vehicle);

            //Assert
            Assert.True(registerResponse);

        }

        [Fact]
        public void RegisterVechicle_WithAvailableMotorCyclleCells_RegisterSuccessful_Test()
        {
            //Arrange
            string licensePlate = "BGP 92E";
            string typeVehicle = ParkingConstants.MotorBikeType;
            int displacement = 600;
            DateTime dateOfEntry = DateTime.Now;
            ParkingLot parking = ServiceLocator.Get<ParkingLot>();
            VehicleDto vehicle = new VehicleDto(licensePlate, typeVehicle, displacement, dateOfEntry);

            //Act
            parking.RegisterCheckInVehicle(vehicle);
            //Assert
            Assert.True(parking.CheckAvailabilityMotorBike());
        }

        [Fact]
        public void CalculateVehiclePayment_WithCarType_ReturnPayment_Test()
        {
            //Arange
            string licensePlate = "BGP 922";
            string typeVehicle = "Car";
            int displacement = 1600;
            DateTime dateOfEntry = DateTime.Now.AddHours(-27);
            ParkingLot parking = ServiceLocator.Get<ParkingLot>();
            VehicleDto vehicle = new VehicleDto(licensePlate, typeVehicle, displacement, dateOfEntry);
            int expected_Payment = 11000;

            //Act
            int payment = parking.CalculatePaymentVehicle(vehicle);

            //Assert
            Assert.Equal(expected_Payment, payment);
        }


        [Fact]
        public void CalculateVehiclePayment_WithMotorCycleType_ReturnPayment_Test()
        {
            //Arange
            string licensePlate = "BGP 92E";
            string typeVehicle = ParkingConstants.MotorBikeType;
            int displacement = 700;
            DateTime dateOfEntry = DateTime.Now.AddHours(-10);
            ParkingLot parking = ServiceLocator.Get<ParkingLot>();
            VehicleDto vehicle = new VehicleDto(licensePlate, typeVehicle, displacement, dateOfEntry);
            int expected_Payment = 6000;

            //Act
            int payment = parking.CalculatePaymentVehicle(vehicle);

            //Assert
            Assert.Equal(expected_Payment, payment);
        }

        [Fact]
        public void CheckOut_WithCarType_CheckoutSuccessful_Test()
        {
            //Arrange
            string licensePlate = "BGP 922";
            string typeVehicle = "Car";
            int displacement = 1600;
            DateTime dateOfEntry = DateTime.Now;
            ParkingLot parking = ServiceLocator.Get<ParkingLot>();
            VehicleDto vehicle = new VehicleDto(licensePlate, typeVehicle, displacement, dateOfEntry);
            parking.RegisterCheckInVehicle(vehicle);

            //Act
            bool registerCheckOutResponse = parking.RegisterCheckoutVehicle(vehicle);

            //Assert
            Assert.True(registerCheckOutResponse);
        }

        [Fact]
        public void CheckOut_WithMotorCyleType_CheckoutSuccessful_Test()
        {
            //Arrange
            string licensePlate = "BGP 92E";
            string typeVehicle = ParkingConstants.MotorBikeType;
            int displacement = 650;
            DateTime dateOfEntry = DateTime.Now;
            ParkingLot parking = ServiceLocator.Get<ParkingLot>();
            VehicleDto vehicle = new VehicleDto(licensePlate, typeVehicle, displacement, dateOfEntry);
            parking.RegisterCheckInVehicle(vehicle);


            //Act
            bool registerCheckOutResponse = parking.RegisterCheckoutVehicle(vehicle);

            //Assert
            Assert.True(registerCheckOutResponse);
        }
    }
}
