﻿using System;
using System.Collections.Generic;
using DriverPlan.model;
using DriverPlan.viewmodel;

namespace DriverPlan
{
    internal static class TestDataGenerator
    {
        public static List<DriverPlanEntryViewModel> CreateDriverPlanEntries()
        {
            var hGeorgEntry = new DriverInfo
            {
                DeliveryLocation = "Kesternich",
                DeliveryTime = DateTime.Now - TimeSpan.FromHours(3),
                Driver = "Georg",
                Note = "Hinter dem Haus"
            };

            var hPeterEntry = new DriverInfo
            {
                DeliveryLocation = "Strauch",
                DeliveryTime = DateTime.Now,
                Driver = "Peter",
                Note = "Garten"
            };


            return new List<DriverPlanEntryViewModel>
            {
                new DriverPlanEntryViewModel(hGeorgEntry),
                new DriverPlanEntryViewModel(hPeterEntry)
            };
        }

        public static List<DriverInfo> CreateDriverInfo()
        {
            return new List<DriverInfo>
            {
                new DriverInfo
                {
                    DeliveryLocation = "Kesternich",
                    DeliveryTime = DateTime.Now - TimeSpan.FromHours(3),
                    Driver = "Georg",
                    Note = "Hinter dem Haus"
                },
                new DriverInfo
                {
                    DeliveryLocation = "Kesternich",
                    DeliveryTime = DateTime.Now - TimeSpan.FromHours(2),
                    Driver = "Georg",
                    Note = "Hinter dem Haus"
                },
                new DriverInfo
                {
                    DeliveryLocation = "Strauch",
                    DeliveryTime = DateTime.Now,
                    Driver = "Peter",
                    Note = "Garten"
                },
                new DriverInfo
                {
                    DeliveryLocation = "Max Mustermann, Gartenstraße, 52152 Strauch",
                    DeliveryTime = DateTime.Now,
                    Driver = "Peter",
                    Note = "Das hier ist jetzt eine etwas längere Notiz"
                },
                new DriverInfo
                {
                    DeliveryLocation = "Strauch",
                    DeliveryTime = DateTime.Now - TimeSpan.FromDays(2),
                    Driver = "Peter",
                    Note = "Garten"
                }
            };
        }
    }
}