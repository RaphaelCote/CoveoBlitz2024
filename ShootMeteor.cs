﻿using System;
namespace Application;

public class ShootMeteor
{
	public ShootMeteor()
	{


	}

	public void ShowInfo(GameMessage gameMessage)
	{
        var myShip = gameMessage.Ships[gameMessage.CurrentTeamId]; //information du vaisseau


		for(int i = 0; i < myShip.Stations.Turrets.Count(); i++)
		{
			Console.WriteLine(myShip.Stations.Turrets[i].TurretType);

			if(myShip.Stations.Turrets[i].TurretType == TurretType.Normal)
			{
                Console.WriteLine(myShip.Stations.Turrets[i].Cooldown);

            }
        }
        
	}

}
