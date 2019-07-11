using System;
namespace MyParking.Core.Constants
{
    public static class ParkingConstants
    {
        public static readonly int AvailableCellCar = 20;
        public static readonly int AvailableCellMotorBike = 10;
        public static readonly int DayHours = 24;
        public static readonly int RuleToChargeDay = 9;

        public static readonly int CostForEachHourOfTheCar = 1000;
        public static readonly int CostForEachDayOfTheCar = 8000;

        public static readonly int CostForEachHourOfTheMotorCycle = 500;
        public static readonly int CostForEachDayOfTheMotorCycle = 4000;

        public static readonly int AditionalCost = 2000;

        public static readonly int DisplacmentMotorbike = 500;

        public static readonly string CarType = "Car";
        public static readonly string MotorBikeType = "MotorBike";

        public static readonly string StartWord = "A";
    }
}
