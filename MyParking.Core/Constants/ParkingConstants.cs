using System;
namespace Parking.Core.Constants
{
    public class ParkingConstants
    {
        public const int AvailableCellCar = 20;
        public const int AvailableCellMotorBike = 10;
        public const int DayHours = 24;
        public const int RuleToChargeDay = 9;

        public static readonly int CostForEachHourOfTheCar = 1000;
        public static readonly int CostForEachDayOfTheCar = 8000;

        public static readonly int CostForEachHourOfTheMotorCycle = 500;
        public static readonly int CostForEachDayOfTheMotorCycle = 4000;

        public const int AditionalCost = 2000;

        public static readonly int DisplacmentMotorbike = 500;

        public const string CarType = "Car";
        public const string MotorBikeType = "MotorBike";

        public const string StartWord = "A";
    }
}
