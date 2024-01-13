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



        public void GetAllHelmStations(GameMessage gameMessage)
        {
            var myShip = gameMessage.Ships[gameMessage.CurrentTeamId]; //information du vaisseau

            for (int i = 0; i < myShip.Stations.Helms.Count(); i++)
            {
                helm.Add(myShip.Stations.Helms[i]);
            }
        }

        public Action RotateToEnemy(RadarStation enemyShip)
        {
            return new ShipLookAtAction(enemyShip.GridPosition);
        }
    }
}
