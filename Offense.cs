using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class Offense
    {
        List<TurretStation> NormalTurret = new List<TurretStation>();
        List<TurretStation> EMPTurret = new List<TurretStation>();
        List<TurretStation> FastTurret = new List<TurretStation>();
        List<TurretStation> CannonTurret = new List<TurretStation>();
        List<TurretStation> SniperTurret = new List<TurretStation>();

        public void GetAllTurretsAvailable(GameMessage gameMessage)
        {
            var myShip = gameMessage.Ships[gameMessage.CurrentTeamId]; //information du vaisseau

            for (int i = 0; i < myShip.Stations.Turrets.Count(); i++)
            {
                Console.WriteLine(myShip.Stations.Turrets[i].TurretType);

                if (myShip.Stations.Turrets[i].TurretType == TurretType.Normal)
                {
                    NormalTurret.Add(myShip.Stations.Turrets[i]);
                }
                if (myShip.Stations.Turrets[i].TurretType == TurretType.EMP)
                {
                    EMPTurret.Add(myShip.Stations.Turrets[i]);
                }
                if (myShip.Stations.Turrets[i].TurretType == TurretType.Fast)
                {
                    FastTurret.Add(myShip.Stations.Turrets[i]);
                }
                if (myShip.Stations.Turrets[i].TurretType == TurretType.Cannon)
                {
                    CannonTurret.Add(myShip.Stations.Turrets[i]);
                }
                if (myShip.Stations.Turrets[i].TurretType == TurretType.Sniper)
                {
                    SniperTurret.Add(myShip.Stations.Turrets[i]);
                }
            }
        }

    }
}
