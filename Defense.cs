using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application;
public class Defense
{
    public static class Shield
    {
        public static int Priority { get; set;}
        public static int ValueMax {get; set;}
        public static int CalculatePriority(GameMessage gameMessage)
        {
            var myShip = gameMessage.Ships[gameMessage.CurrentTeamId];
            ValueMax = gameMessage.Constants.Ship.MaxShield;
            double percentShield = (myShip.CurrentShield/ValueMax)*100;
            if(percentShield < 20)
                Priority = 5;
            else if(percentShield < 60)
                Priority = 3;
            else
                Priority = 0;
            return Priority;
        }
    }
    

    public Defense()
    {

    }
}
