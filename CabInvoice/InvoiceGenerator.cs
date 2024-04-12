﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCabInvoice
{
   
    public interface IRide
    {
        double Distance { get; }
        double Duration { get; }
    }

    public interface IInvoiceGenerator
    {
        double CalculateFare(IRide ride); 
        double CalculateTotalFare(IEnumerable<IRide> rides);
    }

    public class Ride(double distance, double duration) : IRide
    {
        public double Distance { get; } = distance;
        public double Duration { get; } = duration;
    }
    public class InvoiceGenerator : IInvoiceGenerator
    {
        private const double CostPerKm = 10.0;
        private const double CostPerMinute = 1.0;
        private const double MinimumFare = 5.0;

        public double CalculateFare(IRide ride) 
        {
            double distanceCost = ride.Distance * CostPerKm;
            double timeCost = ride.Duration * CostPerMinute;
            double totalFare = Math.Max(distanceCost + timeCost, MinimumFare);
            return totalFare;
        }

        public double CalculateTotalFare(IEnumerable<IRide> rides)
        {
            double totalFare = 0;
            foreach (var ride in rides)
            {
                totalFare += CalculateFare(ride);
            }
            return totalFare;
        }

    }
}