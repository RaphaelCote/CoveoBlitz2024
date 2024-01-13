using static System.Collections.Specialized.BitVector32;

namespace Application;

public class Bot
{
    public const string NAME = "My cool C# bot";

    Offense offense = new Offense();
    bool doOnce = false;

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
        if(!doOnce)
        {
            doOnce = true;
            offense.GetAllTurretsAvailable(gameMessage);
        }
        var actions = new List<Action>();

        var myShip = gameMessage.Ships[gameMessage.CurrentTeamId];
        var otherShipsIds = gameMessage.ShipsPositions.Keys.Where(shipId => shipId != gameMessage.CurrentTeamId).ToList();

        // You could find who's not doing anything and try to give them a job?
        var idleCrewmates = myShip.Crew
            .Where(crewmate => crewmate.CurrentStation == null && crewmate.Destination == null)
            .ToList();

        Station stationToMoveTo = offense.MoveStationWeapon();

        if( stationToMoveTo != null )
        {
            actions.Add(new CrewMoveAction(idleCrewmates[0].Id, stationToMoveTo.GridPosition));
        }
        


        //code qui permet d'utiliser le radar
        //var operatedRadarStations = myShip.Stations.Radars.Where(radarStation => radarStation.Operator != null);
        //foreach(var radarStation in operatedRadarStations)
        //{
        //actions.Add(new RadarScanAction(radarStation.Id, otherShipsIds[Random.Shared.Next(otherShipsIds.Count)])); //Radar scan un des vaisseau ennemie sur l'écran de façon aléatoire
        //}

        Console.WriteLine("Action count : " + actions.Count);
        return actions;
    }
}
