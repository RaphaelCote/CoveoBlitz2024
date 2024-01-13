namespace Application;

public class Bot
{
    public const string NAME = "My cool C# bot";

    /// <summary>
    /// This method should be use to initialize some variables you will need throughout the game.
    /// </summary>
    public Bot()
    {
        Console.WriteLine("Initializing your super mega bot!");
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

        // You could find who's not doing anything and try to give them a job?
        var idleCrewmates = myShip.Crew
            .Where(crewmate => crewmate.CurrentStation == null && crewmate.Destination == null)
            .ToList();
        //test
        foreach(var crewmate in idleCrewmates)
        {
            //trouve des stations qui sont disponibles et leurs distance au crewmate sélectionné
            var visitableStations = crewmate.DistanceFromStations.Shields
                .Concat(crewmate.DistanceFromStations.Turrets)
                .Concat(crewmate.DistanceFromStations.Helms)
                .Concat(crewmate.DistanceFromStations.Radars)
                .ToList();

            var stationToMoveTo = visitableStations[Random.Shared.Next(visitableStations.Count)];

            actions.Add(new CrewMoveAction(crewmate.Id, stationToMoveTo.StationPosition));
        }

        // Now crew members at stations should do something!
        var operatedTurretStations = myShip.Stations.Turrets.Where(turretStation => turretStation.Operator != null); //trouve les stations tourelles présentement peuplé
        foreach(var turretStation in operatedTurretStations)
        {
            var switchAction = Random.Shared.Next(3);
            //fait des actions aléatoire
            switch(switchAction)
            {
                case 0:
                    // Charge the turret
                    actions.Add(new TurretChargeAction(turretStation.Id));
                    break;
                case 1:
                    // Aim at the turret itself
                    actions.Add(new TurretLookAtAction(turretStation.Id, new Vector(gameMessage.Constants.World.Width * Random.Shared.NextDouble(), gameMessage.Constants.World.Width * Random.Shared.NextDouble())));
                    break;
                case 2:
                    // Shoot!
                    actions.Add(new TurretShootAction(turretStation.Id));
                    break;
            }
        }

        //permet de trouver un helm avec un crewmate
        var operatedHelmStation = myShip.Stations.Helms.Where(helmStation => helmStation.Operator != null);
        foreach(var helmStation in operatedHelmStation)
        {
            actions.Add(new ShipRotateAction(360 * Random.Shared.NextDouble())); //rotation du vaisseau
        }

        //code qui permet d'utiliser le radar
        var operatedRadarStations = myShip.Stations.Radars.Where(radarStation => radarStation.Operator != null);
        foreach(var radarStation in operatedRadarStations)
        {
            actions.Add(new RadarScanAction(radarStation.Id, otherShipsIds[Random.Shared.Next(otherShipsIds.Count)])); //Radar scan un des vaisseau ennemie sur l'écran de façon aléatoire
        }

        // You can clearly do better than the random actions above. Have fun!!
        return actions;
    }
}
