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

        int NormalTower = 0;
        int EMPTower = 0;
        int FastTower = 0;
        int CannonTower = 0;
        int SniperTower = 0;


        int MaxWeapon = 0;

        public Offense()
        {

        }


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


        public Station MoveStationWeapon()
        {
            Station station = null;

            if(MaxWeapon >= 2)
            {
                return station;
            }
            MaxWeapon++;

            if (NormalTurret.Count() - NormalTower > 0 && NormalTower <= 1)
            {
                NormalTower++;
                station = NormalTurret[0];
            }
            if (EMPTurret.Count() - EMPTower > 0 && EMPTower <= 0)
            {
                EMPTower++;
                station = EMPTurret[0];
            }
            if (FastTurret.Count() - FastTower > 0 && FastTower <= 0)
            {
                FastTower++;
                station = FastTurret[0];
            }
            if (CannonTurret.Count() - CannonTower > 0 && CannonTower <= 0)
            {
                CannonTower++;
                station = CannonTurret[0];
            }
            if (SniperTurret.Count() - SniperTower > 0 && SniperTower <= 0)
            {
                SniperTower++;
                station = SniperTurret[0];
            }

            Console.WriteLine("NewWeapon");


            return station;
        }

    }
}
