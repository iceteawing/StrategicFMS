﻿using StrategicFMSDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFMS.AFAS
{
    public class DecentralizedDecisionAssistanceSystem//TODO: refer to martin's paper
    {
        public DecentralizedDecisionAssistanceSystem() { IsConfirmed = false; IsConfirming = false; }

        private bool _isConfirmed;
        private bool _isConfirming;
        public bool IsConfirmed { get => _isConfirmed; set => _isConfirmed = value; }
        public bool IsConfirming { get => _isConfirming; set => _isConfirming = value; }

        public List<string> LandingSequence = new();
        public void SequenceOperations(List<Aircraft> aircrafts)
        {
            // 使用LINQ根据EstimatedArrivalTime对飞机列表进行排序
            var sortedAircrafts = aircrafts.OrderBy(aircraft => aircraft.AutoPilot.ActiveFlightPlan.EstimatedArrivalTime).ToList();

            foreach (Aircraft aircraft in sortedAircrafts)
            {
                aircraft.Afas.Adas.IsConfirming = true;
                DateTime dt = aircraft.AutoPilot.ActiveFlightPlan.EstimatedArrivalTime;
                LandingSequence.Add(aircraft.AircraftId);
            }
            FlightData flightData = FlightData.GetInstance();
            flightData.LandingSequence = LandingSequence;
        }

        public void SchedulingSessionInitialization()
        {

        }
        public bool ConfirmLock()
        {
            return _isConfirming;
        }
        public bool ConfirmUnlock()
        {
            return _isConfirmed;
        }
        public bool SchedulingSessionTimeout()
        {
            return false;
        }

    }
}
