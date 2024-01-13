using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    /*
    public static class ShootIncoming
    {
        const double Rad2Deg = 180.0 / Math.PI;
        const double Deg2Rad = Math.PI / 180.0;
        public static int Priority { get; set;}
        public static int CalculatePriority(GameMessage gameMessage)
        {
            var myShip = gameMessage.Ships[gameMessage.CurrentTeamId];
            var debrisList = gameMessage.Debris;
            var WeaponSpeed = gameMessage.Constants.Ship.Stations.TurretInfos[0].RocketSpeed;
            foreach(var meteor in debrisList)
            {
                var positionShip = gameMessage.ShipsPositions[gameMessage.CurrentTeamId];
                Vector vectorFromRunner = new Vector(positionShip.X - meteor.Position.X, positionShip.Y - meteor.Position.Y);
                double distanceToRunner = Math.Sqrt(vectorFromRunner.X * vectorFromRunner.X + vectorFromRunner.Y * vectorFromRunner.Y);
                double meteorSpeed = Math.Sqrt(meteor.Velocity.X * meteor.Velocity.X + meteor.Velocity.Y * meteor.Velocity.Y);
                double m_timeToInterception = 0;

                // calcul les variable de la quadratique(grosse berta)
                double a = WeaponSpeed * WeaponSpeed - meteorSpeed * meteorSpeed;
                double b = 2 * (vectorFromRunner.X * meteorVelocity.X + vectorFromRunner.Y * meteorVelocity.Y);
                double c = -distanceToRunner * distanceToRunner;
            }
            
            return Priority;
        }
        //Fonction qui calcul la quadratique
        //Les deux valeur sont dans t1 et t2
        //Si la fonction retourne faux la racine est negatif, donc erreur
        private static bool QuadraticSolver(double a, double b, double c, out double t1, out double t2)
        {
            t1 = 0; t2 = 0;

            double preRoot = (b * b) - (4 * a * c);

            if (preRoot < 0)
            {
                return false;
            }
            else
            {
                preRoot = Math.Sqrt(preRoot);
                t1 = (-b + preRoot) / (2 * a);
                t2 = (-b - preRoot) / (2 * a);
                return true;
            }
        }
    }
    */
    public Defense()
    {

    }
}
