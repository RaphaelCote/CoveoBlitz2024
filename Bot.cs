using System;
using System.Linq;
namespace Application;

public class Bot
{
    public const string NAME = "TEST BOT";

    /// <summary>
    /// This method should be use to initialize some variables you will need throughout the game.
    /// </summary>
    public Bot()
    {
        Console.WriteLine("Initializing your bot!");
    }

    /// <summary>
    /// Here is where the magic happens, for now the moves are random. I bet you can do better ;)
    /// </summary>
    public IEnumerable<Action> GetNextMoves(GameMessage gameMessage)
    {
        var actions = new List<Action>();// liste d'action

        var myShip = gameMessage.Ships[gameMessage.CurrentTeamId]; //information du vaisseau
        //Console.Write("Ship ID:");
        //Console.WriteLine(myShip);
        var otherShipsIds = gameMessage.ShipsPositions.Keys.Where(shipId => shipId != gameMessage.CurrentTeamId).ToList(); //infor vaisseau ennemi, est-ce que on a la position en partant?
        Vector myShipPos = myShip.WorldPosition;
        var otherShips = gameMessage.ShipsPositions.Where(ship => ship.Key != gameMessage.CurrentTeamId).ToList();
        Vector closestPos = otherShips[0].Value;

        // You could find who's not doing anything and try to give them a job?
        var Crewmates = myShip.Crew
            .ToList();

        var ValueMax = gameMessage.Constants.Ship.MaxShield;
        double percentShield = (myShip.CurrentShield / ValueMax) * 100;

        /*
        Code pour le premier crewmate qui est le chef
        */
        var Chef = Crewmates[0];
        if (percentShield < 50)
        {
            var distanceShield = Chef.DistanceFromStations.Shields;
            var stationToMoveTo = distanceShield[Chef.DistanceFromStations.Shields.Count() - 1];
            actions.Add(new CrewMoveAction(Chef.Id, stationToMoveTo.StationPosition));
        }
        else if ((gameMessage.CurrentTickNumber%200) < 50)
        {
            var distanceRadar = Chef.DistanceFromStations.Radars;
            var stationToMoveTo = distanceRadar[Chef.DistanceFromStations.Radars.Count()-1];
            actions.Add(new CrewMoveAction(Chef.Id, stationToMoveTo.StationPosition));
        }
        else if((gameMessage.CurrentTickNumber%200) < 120)
        {
            var distanceHelm = Chef.DistanceFromStations.Helms;
            var stationToMoveTo = distanceHelm[Chef.DistanceFromStations.Helms.Count()-1];
            actions.Add(new CrewMoveAction(Chef.Id, stationToMoveTo.StationPosition));
        }
        else
        {
            var distanceTurret = Chef.DistanceFromStations.Turrets;
            var stationToMoveTo = distanceTurret[Chef.DistanceFromStations.Turrets.Count()-1];
            actions.Add(new CrewMoveAction(Chef.Id, stationToMoveTo.StationPosition));
        }

        /*
        Crewmate qui gere un fusil et le bouclier
        */
        var GunShield = Crewmates[1]; 
        
        if(percentShield < 70)
        {
            var distanceShield = GunShield.DistanceFromStations.Shields;
            var stationToMoveTo = distanceShield[GunShield.DistanceFromStations.Shields.Count()-1];
            actions.Add(new CrewMoveAction(GunShield.Id, stationToMoveTo.StationPosition));
        }
        else
        {
            //if(GunShield.CurrentStation.ToLower().Contains("shi") || GunShield.CurrentStation == null)
            //{
                var distanceTurret = GunShield.DistanceFromStations.Turrets;
                var stationToMoveTo = distanceTurret[GunShield.DistanceFromStations.Turrets.Count()-2];
                actions.Add(new CrewMoveAction(GunShield.Id, stationToMoveTo.StationPosition));
            //}
        }

        /*
        PEW PEW crew
        */
        var Gun1 = Crewmates[2];
        if (percentShield < 50)
        {
            var distanceShield = Gun1.DistanceFromStations.Shields;
            var stationToMoveTo = distanceShield[Gun1.DistanceFromStations.Shields.Count() - 1];
            actions.Add(new CrewMoveAction(Gun1.Id, stationToMoveTo.StationPosition));
        }
        else if (Gun1.CurrentStation == null)
        {
            var distanceTurretGun1 = Gun1.DistanceFromStations.Turrets;
            var stationToMoveToGun1  = distanceTurretGun1 [Gun1.DistanceFromStations.Turrets.Count()-3];
            actions.Add(new CrewMoveAction(Gun1.Id, stationToMoveToGun1.StationPosition));
        }
        var Gun2 = Crewmates[3];
        if (percentShield < 50)
        {
            var distanceShield = Gun2.DistanceFromStations.Shields;
            var stationToMoveTo = distanceShield[Gun1.DistanceFromStations.Shields.Count() - 1];
            actions.Add(new CrewMoveAction(Gun1.Id, stationToMoveTo.StationPosition));
        }
        else if (Gun2.CurrentStation == null)
        {
            var distanceTurretGun2 = Gun2.DistanceFromStations.Turrets;
            var stationToMoveToGun2  = distanceTurretGun2 [Gun2.DistanceFromStations.Turrets.Count()-4];
            actions.Add(new CrewMoveAction(Gun2.Id, stationToMoveToGun2.StationPosition));
        }
        
        //permet de trouver un helm avec un crewmate
        var operatedHelmStation = myShip.Stations.Helms.Where(helmStation => helmStation.Operator != null);
        foreach(var helmStation in operatedHelmStation)
        {
            foreach(var ship in otherShips)
            {

                if(distanceBetween(myShipPos, ship.Value) < distanceBetween(myShipPos, closestPos))
                {
                    closestPos = ship.Value;
                }
            }
            actions.Add(new ShipLookAtAction(closestPos));
        }

        //code qui permet d'utiliser le radar
        var operatedRadarStations = myShip.Stations.Radars.Where(radarStation => radarStation.Operator != null);
        foreach(var radarStation in operatedRadarStations)
        {
            actions.Add(new RadarScanAction(radarStation.Id, otherShipsIds[Random.Shared.Next(otherShipsIds.Count)])); //Radar scan un des vaisseau ennemie sur l'écran de façon aléatoire
        }

        // Now crew members at stations should do something!
        var operatedTurretStations = myShip.Stations.Turrets.Where(turretStation => turretStation.Operator != null); //trouve les stations tourelles présentement peuplé
        foreach(var turretStation in operatedTurretStations)
        {
            var switchAction = Random.Shared.Next(3);
            foreach(var ship in otherShips)
            {

                if(distanceBetween(myShipPos, ship.Value) < distanceBetween(myShipPos, closestPos))
                {
                    closestPos = ship.Value;
                }
            }
            //fait des actions aléatoire
            switch(switchAction)
            {
                case 0:
                    // Charge the turret
                    actions.Add(new TurretChargeAction(turretStation.Id));
                    break;
                case 1:
                    // Aim at the turret itself
                    actions.Add(new TurretLookAtAction(turretStation.Id, closestPos));
                    break;
                case 2:
                    // Shoot!
                    actions.Add(new TurretShootAction(turretStation.Id));
                    break;
            }
        }

        

        // You can clearly do better than the random actions above. Have fun!!
        return actions;
    }
    static private double distanceBetween(Vector vector1, Vector vector2)
        {
            return Math.Sqrt(Math.Pow(vector1.X - vector2.X, 2) + Math.Pow(vector1.Y - vector2.Y, 2));
        }
}
