﻿using SuperFMS.Airspaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFMS.Traffic
{
    public class Route
    {
        public Route() { }

        public Route(Airdrome originAirport, Airdrome destionationAirport, List<Waypoint> waypoints)
        {
            _originAirport = originAirport;
            _destionationAirport = destionationAirport;
            _waypoints = waypoints;
        }

        public Route(Airdrome originAirport, Airdrome destionationAirport)
        {
            _originAirport = originAirport;
            _destionationAirport = destionationAirport;
            _waypoints = new List<Waypoint>();
        }

        private Airdrome _originAirport;
        private Airdrome _destionationAirport;
        private List<Waypoint> _waypoints;

        private Waypoint4D _holdingPoint;// it is surposed to be the prior waypoint of IAF in this phase


        private DateTime _estimatedArrivalTime;
        private DateTime _latestArrivalTime;
        private DateTime _earliestArrivalTime;
        private double _estimatedArrivalTimeInSeconds;
        private double _landingDurationInSeconds;//The time from IAF to the touch down, it depends on the aircraft performance and the STAR
        public Airdrome OriginAirport { get => _originAirport; set => _originAirport = value; }
        public Airdrome DestionationAirport { get => _destionationAirport; set => _destionationAirport = value; }
        public List<Waypoint> Waypoints { get => _waypoints; set => _waypoints = value; }
        public DateTime EstimatedArrivalTime { get => _estimatedArrivalTime; set => _estimatedArrivalTime = value; }
        public DateTime LatestArrivalTime { get => _latestArrivalTime; set => _latestArrivalTime = value; }
        public DateTime EarliestArrivalTime { get => _earliestArrivalTime; set => _earliestArrivalTime = value; }
        public Waypoint4D HoldingPoint { get => _holdingPoint; set => _holdingPoint = value; }
        public double EstimatedArrivalTimeInSeconds { get => _estimatedArrivalTimeInSeconds; set => _estimatedArrivalTimeInSeconds = value; }
        public double LandingDurationInSeconds { get => _landingDurationInSeconds; set => _landingDurationInSeconds = value; }

        public void AddWaypoint(Waypoint waypoint)
        {
            Waypoints.Add(waypoint);
        }
        public void SerializeToJson(string filePath)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);
        }

        public static Route DeserializeFromJson(string filePath)
        {
            string json = System.IO.File.ReadAllText(filePath);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Route>(json);
        }
    }
}