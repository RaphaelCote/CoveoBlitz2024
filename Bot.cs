using static System.Collections.Specialized.BitVector32;

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

        var otherShipsIds = gameMessage.ShipsPositions.Where(ship => ship.Key != gameMessage.CurrentTeamId).ToList(); //infor vaisseau ennemi, est-ce que on a la position en partant?

        List<Action> helmActions = RotateShip.RotateShipToEnemy(gameMessage);

        foreach(var action in helmActions)
        {
            actions.Add(action);
        }

        // Now crew members at stations should do something!
        //var operatedTurretStations = myShip.Stations.Turrets.Where(turretStation => turretStation.Operator != null); //trouve les stations tourelles présentement peuplé
        //foreach(var turretStation in operatedTurretStations)
        //{
        //    var switchAction = Random.Shared.Next(3);
        //    //fait des actions aléatoire
        //    switch(switchAction)
        //    {
        //        case 0:
        //            // Charge the turret
        //            actions.Add(new TurretChargeAction(turretStation.Id));
        //            break;
        //        case 1:
        //            // Aim at the turret itself
        //            actions.Add(new TurretLookAtAction(turretStation.Id, new Vector(gameMessage.Constants.World.Width * Random.Shared.NextDouble(), gameMessage.Constants.World.Width * Random.Shared.NextDouble())));
        //            break;
        //        case 2:
        //            // Shoot!
        //            actions.Add(new TurretShootAction(turretStation.Id));
        //            break;
        //    }
        //}

        //code qui permet d'utiliser le radar
        //var operatedRadarStations = myShip.Stations.Radars.Where(radarStation => radarStation.Operator != null);
        //foreach(var radarStation in operatedRadarStations)
        //{
        //actions.Add(new RadarScanAction(radarStation.Id, otherShipsIds[Random.Shared.Next(otherShipsIds.Count)])); //Radar scan un des vaisseau ennemie sur l'écran de façon aléatoire
        //}

        //ShootMeteor shootMeteor = new ShootMeteor();

        //shootMeteor.ShowInfo(gameMessage);
        // You can clearly do better than the random actions above. Have fun!!
        //foreach(var action in actions)
        //{
        //    Console.WriteLine(action.Type);
        //}
        Console.WriteLine(actions.Count);
        return actions;
    }
}
