using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class CrewMates
    {

        public CrewMates() { }

        /// <summary>
        /// Move a closest idle crew to a station
        /// </summary>
        /// <param name="gameMessage"></param>
        /// <param name="station"></param>
        /// <param name="moveAction"></param>
        /// <returns>
        /// True : Action to move crew created
        /// False : Now idle crew, try liberating a crew from less important task
        /// </returns>
        static public bool MoveClosestCrewToStation(GameMessage gameMessage, Station station, out Action? moveAction)
        {
            var myShip = gameMessage.Ships[gameMessage.CurrentTeamId];

            //All idle crewmates
            List<Crewmate> idleCrewmates = myShip.Crew
            .Where(crewmate => crewmate.CurrentStation == null && crewmate.Destination == null)
            .ToList();

            //Look for closer crew
            if(idleCrewmates.Count > 0)
            {

                //Station stationToGo = myShip.Stations.
                Crewmate closestCrew = idleCrewmates[0];
                int crewmateDistance = -1;
                foreach(var crewmate in idleCrewmates)
                {
                    List<DistanceFromStation> visitableStations = crewmate.DistanceFromStations.Shields
                        .Concat(crewmate.DistanceFromStations.Turrets)
                        .Concat(crewmate.DistanceFromStations.Helms)
                        .Concat(crewmate.DistanceFromStations.Radars)
                        .ToList();

                    int distance = visitableStations
                        .Where(distanceFromStation => distanceFromStation.StationId == station.Id)
                        .FirstOrDefault()
                        .Distance;

                    if(crewmateDistance == -1)
                    {
                        closestCrew = crewmate;
                        crewmateDistance = distance;
                    }
                    else if(distance < crewmateDistance)
                    {
                        closestCrew = crewmate;
                        crewmateDistance = distance;
                    }

                }

                moveAction = new CrewMoveAction(closestCrew.Id, station.GridPosition);

                return true;
            }

            moveAction = null;

            return false;
        }

        /// <summary>
        /// Move a closest idle crew to a closest station of type
        /// </summary>
        /// <param name="gameMessage"></param>
        /// <param name="station"></param>
        /// <param name="moveAction"></param>
        /// <returns>
        /// True : Action to move crew created
        /// False : Now idle crew, try liberating a crew from less important task
        /// </returns>
        static public bool MoveClosestCrewToStationType(GameMessage gameMessage, Station station, out Action? moveAction)
        {
            var myShip = gameMessage.Ships[gameMessage.CurrentTeamId];

            //All idle crewmates
            List<Crewmate> idleCrewmates = myShip.Crew
            .Where(crewmate => crewmate.CurrentStation == null && crewmate.Destination == null)
            .ToList();

            //Look for closer crew
            if(idleCrewmates.Count > 0)
            {

                //Station stationToGo = myShip.Stations.
                Crewmate closestCrew = idleCrewmates[0];
                int crewmateDistance = -1;
                foreach(var crewmate in idleCrewmates)
                {
                    List<DistanceFromStation> visitableStations = crewmate.DistanceFromStations.Shields
                        .Concat(crewmate.DistanceFromStations.Turrets)
                        .Concat(crewmate.DistanceFromStations.Helms)
                        .Concat(crewmate.DistanceFromStations.Radars)
                        .ToList();

                    int distance = visitableStations
                        .Where(distanceFromStation => distanceFromStation.StationId == station.Id)
                        .FirstOrDefault()
                        .Distance;

                    if(crewmateDistance == -1)
                    {
                        closestCrew = crewmate;
                        crewmateDistance = distance;
                    }
                    else if(distance < crewmateDistance)
                    {
                        closestCrew = crewmate;
                        crewmateDistance = distance;
                    }

                }

                moveAction = new CrewMoveAction(closestCrew.Id, station.GridPosition);

                return true;
            }

            moveAction = null;

            return false;
        }
    }
}
