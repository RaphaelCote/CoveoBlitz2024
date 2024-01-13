using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    internal class ShootTarget
    {
        int OffsetTicks = 0;

        public Vector GetInterspetionPosition(Vector gunPosition, Vector meteorPosition, Vector meteorVelocity, double WeaponSpeed)
        {
            Vector vectorFromRunner = new Vector(gunPosition.X - meteorPosition.X, gunPosition.Y - meteorPosition.Y);
            double distanceToRunner = Math.Sqrt(vectorFromRunner.X * vectorFromRunner.X + vectorFromRunner.Y * vectorFromRunner.Y);
            double meteorSpeed = Math.Sqrt(meteorVelocity.X * meteorVelocity.X + meteorVelocity.Y * meteorVelocity.Y);

            double m_timeToInterception = 0;

            // calcul les variable de la quadratique(grosse berta)
            double a = WeaponSpeed * WeaponSpeed - meteorSpeed * meteorSpeed;
            double b = 2 * (vectorFromRunner.X * meteorVelocity.X + vectorFromRunner.Y * meteorVelocity.Y);
            double c = -distanceToRunner * distanceToRunner;


            double t1, t2;
            if (QuadraticSolver(a, b, c, out t1, out t2))
            {

                if (t1 < 0 && t2 < 0)
                {
                    // Both values for t are negative, so the interception would have to have
                    // occured in the past
                    return new Vector(0, 0);
                }

                if (t1 > 0 && t2 > 0) // Both are positive, take the smaller one
                    m_timeToInterception = Math.Min(t1, t2);
                else // One has to be negative, so take the larger one
                    m_timeToInterception = Math.Max(t1, t2);

                //calcul de l'intersection sur la droite de la meteor
                Vector vec = new Vector(meteorPosition.X + (meteorVelocity.X * (m_timeToInterception + OffsetTicks)), meteorPosition.Y + (meteorVelocity.Y * (m_timeToInterception + OffsetTicks)));

                return vec;
            }


            return new Vector(0, 0);
        }


        //Fonction qui calcul la quadratique
        //Les deux valeur sont dans t1 et t2
        //Si la fonction retourne faux la racine est negatif, donc erreur
        private bool QuadraticSolver(double a, double b, double c, out double t1, out double t2)
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
}
