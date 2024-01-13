using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class RotateShip
    {
        List<HelmStation> helm = new List<HelmStation>();


        static public List<Action> RotateShipToEnemy(GameMessage gameMessage)
        {
            var actions = new List<Action>();
            var myShip = gameMessage.Ships[gameMessage.CurrentTeamId]; //information du vaisseau
            Vector myShipPos = myShip.WorldPosition;

            var otherShips = gameMessage.ShipsPositions.Where(ship => ship.Key != gameMessage.CurrentTeamId).ToList();

            Vector closestPos = otherShips[0].Value;
            foreach(var ship in otherShips)
            {

                if(distanceBetween(myShipPos, ship.Value) < distanceBetween(myShipPos, closestPos))
                {
                    closestPos = ship.Value;
                }
            }

            //find if looking at enemy ship
            double targetAngle = Math.Asin((closestPos.Y - myShipPos.Y) / distanceBetween(myShipPos, closestPos));
            if(myShip.OrientationDegrees != targetAngle)
            {
                Station helmStation = myShip.Stations.Helms.FirstOrDefault();

                Action? moveAction = CrewMates.MoveClosestCrewToStation(gameMessage, helmStation);

                if(moveAction != null)
                {
                    actions.Add(moveAction);
                }
            }

            //permet de trouver un helm avec un crewmate
            var operatedHelmStation = myShip.Stations.Helms.Where(helmStation => helmStation.Operator != null).ToList();
            if(operatedHelmStation.Count != 0)
            {
                //return new ShipLookAtAction();
                actions.Add(new ShipLookAtAction(closestPos));
            }


            return actions;

        }

        public void GetAllHelmStations(GameMessage gameMessage)
        {
            var myShip = gameMessage.Ships[gameMessage.CurrentTeamId]; //information du vaisseau

            for(int i = 0; i < myShip.Stations.Helms.Count(); i++)
            {
                helm.Add(myShip.Stations.Helms[i]);
            }
        }

        public Action RotateToEnemy(RadarStation enemyShip)
        {
            return new ShipLookAtAction(enemyShip.GridPosition);
        }

        static private double distanceBetween(Vector vector1, Vector vector2)
        {
            return Math.Sqrt(Math.Pow(vector1.X - vector2.X, 2) + Math.Pow(vector1.Y - vector2.Y, 2));
        }
    }
}
