using System;

namespace WindowsFormsApplication1
{
    static class QuarticClass
    {
        const double PI = 3.1415926535897932;
        const double THIRD = 1.0 / 3;
        const double RESOLUTION = 1.0E-99;

        /*-------------------- Global Function Description Block ----------------------
         *
         *     ***QUARTIC************************************************25.03.98
         *     Solution of a quartic equation
         *     ref.: J. E. Hacke, Amer. Math. Monthly, Vol. 48, 327-328, (1941)
         *     NO WARRANTY, ALWAYS TEST THIS SUBROUTINE AFTER DOWNLOADING
         *     ******************************************************************
         *     dd(0:4)     (i)  vector containing the polynomial coefficients
         *     sol(1:4)    (o)  results, real part
         *     soli(1:4)   (o)  results, imaginary part
         *     Nsol        (o)  number of real solutions 
         *     ==================================================================
         *  	17-Oct-2004 / Raoul Rausch
         *		Conversion from Fortran to C
         *
         *
         *-----------------------------------------------------------------------------
         */
        static public int Quartic(double[] dd, out double[] sol, out double[] soli, out int Nsol)
        {
            double[] AA = new double[4], z = new double[3];
            double a, b, c, d, f, p, q, r, zsol, xK2, xL, xK, sqp, sqm;
            int ncube, i;
            Nsol = 0;
            sol = new double[4];
            soli = new double[4];

            if (Math.Abs(dd[4]) < RESOLUTION)
            {
                Console.WriteLine("\n ERROR: NOT A QUARTIC EQUATION");
                return 0;
            }

            a = dd[4];
            b = dd[3];
            c = dd[2];
            d = dd[1];
            f = dd[0];

            p = (-3.0 * Math.Pow(b, 2) + 8.0 * a * c) / (8.0 * Math.Pow(a, 2));
            q = (Math.Pow(b, 3) - 4.0 * a * b * c + 8.0 * d * Math.Pow(a, 2)) / (8.0 * Math.Pow(a, 3));
            r = (-3.0 * Math.Pow(b, 4) + 16.0 * a * Math.Pow(b, 2) * c - 64.0 * Math.Pow(a, 2) * b * d + 256.0 * Math.Pow(a, 3) * f) / (256.0 * Math.Pow(a, 4));

            // Solve cubic resolvent
            AA[3] = 8.0;
            AA[2] = -4.0 * p;
            AA[1] = -8.0 * r;
            AA[0] = 4.0 * p * r - Math.Pow(q, 2);

            //Console.WriteLine("\n bcubic %.4e\t%.4e\t%.4e\t%.4e ", AA[0], AA[1], AA[2], AA[3]);
            cubic(AA, out z, out ncube);
            //Console.WriteLine("\n acubic %.4e\t%.4e\t%.4e ", z[0], z[1], z[2]);

            zsol = -1.0E+99;
            for (i = 0; i < ncube; i++) zsol = Math.Max(zsol, z[i]);	//Not sure C has Math.Max fct
            z[0] = zsol;
            xK2 = 2.0 * z[0] - p;
            xK = Math.Sqrt(xK2);
            xL = q / (2.0 * xK);
            sqp = xK2 - 4.0 * (z[0] + xL);
            sqm = xK2 - 4.0 * (z[0] - xL);

            for (i = 0; i < 4; i++) soli[i] = 0.0;
            if ((sqp >= 0.0) && (sqm >= 0.0))
            {
                Console.WriteLine("\n case 1 ");
                sol[0] = 0.5 * (xK + Math.Sqrt(sqp));
                sol[1] = 0.5 * (xK - Math.Sqrt(sqp));
                sol[2] = 0.5 * (-xK + Math.Sqrt(sqm));
                sol[3] = 0.5 * (-xK - Math.Sqrt(sqm));
                Nsol = 4;
            }
            else if ((sqp >= 0.0) && (sqm < 0.0))
            {
                Console.WriteLine("\n case 2 ");
                sol[0] = 0.5 * (xK + Math.Sqrt(sqp));
                sol[1] = 0.5 * (xK - Math.Sqrt(sqp));
                sol[2] = -0.5 * xK;
                sol[3] = -0.5 * xK;
                soli[2] = Math.Sqrt(-.25 * sqm);
                soli[3] = -Math.Sqrt(-.25 * sqm);
                Nsol = 2;
            }
            else if ((sqp < 0.0) && (sqm >= 0.0))
            {
                Console.WriteLine("\n case 3 ");
                sol[0] = 0.5 * (-xK + Math.Sqrt(sqm));
                sol[1] = 0.5 * (-xK - Math.Sqrt(sqm));
                sol[2] = 0.5 * xK;
                sol[3] = 0.5 * xK;
                soli[2] = Math.Sqrt(-0.25 * sqp);
                soli[3] = -Math.Sqrt(-0.25 * sqp);
                Nsol = 2;
            }
            else if ((sqp < 0.0) && (sqm < 0.0))
            {
                Console.WriteLine("\n case 4 ");
                sol[0] = -0.5 * xK;
                sol[1] = -0.5 * xK;
                soli[0] = Math.Sqrt(-0.25 * sqm);
                soli[1] = -Math.Sqrt(-0.25 * sqm);
                sol[2] = 0.5 * xK;
                sol[3] = 0.5 * xK;
                soli[2] = Math.Sqrt(-0.25 * sqp);
                soli[3] = -Math.Sqrt(-0.25 * sqp);
                Nsol = 0;
            }

            for (i = 0; i < 4; i++) sol[i] -= b / (4.0 * a);
            return 0;

        }

        /*-------------------- Global Function Description Block ----------------------
         *
         *     ***CUBIC************************************************08.11.1986
         *     Solution of a cubic equation
         *     Equations of lesser degree are solved by the appropriate formulas.
         *     The solutions are arranged in ascending order.
         *     NO WARRANTY, ALWAYS TEST THIS SUBROUTINE AFTER DOWNLOADING
         *     ******************************************************************
         *     A(0:3)      (i)  vector containing the polynomial coefficients
         *     X(1:L)      (o)  results
         *     L           (o)  number of valid solutions (beginning with X(1))
         *     ==================================================================
         *  	17-Oct-2004 / Raoul Rausch
         *		Conversion from Fortran to C
         *
         *
         *-----------------------------------------------------------------------------
         */
        static private int cubic(double[] A, out double[] X, out int L)
        {
            double[] U = new double[3];
            double W, P, Q, DIS, PHI;
            X = new double[3];

            //define cubic root as statement function
            // In C, the function is defined outside of the cubic fct

            // ====determine the degree of the polynomial ====

            if (Math.Abs(A[3]) > RESOLUTION) //cubic equation
            {
                W = A[2] / A[3] * THIRD;
                P = Math.Pow((A[1] / A[3] * THIRD - Math.Pow(W, 2)), 3);
                Q = -0.5 * (2.0 * Math.Pow(W, 3) - (A[1] * W - A[0]) / A[3]);
                DIS = Math.Pow(Q, 2) + P;
                if (DIS < 0.0) //three real solutions!
                {
                    //Confine the argument of ACOS to the interval [-1;1]!
                    PHI = Math.Acos(Math.Min(1.0, Math.Max(-1.0, Q / Math.Sqrt(-P))));
                    P = 2.0 * Math.Pow((-P), (0.5 * THIRD));
                    for (int i = 0; i < 3; i++) U[i] = P * Math.Cos((PHI + 2 * ((double)i) * PI) * THIRD) - W;
                    X[0] = Math.Min(U[0], Math.Min(U[1], U[2]));
                    X[1] = Math.Max(Math.Min(U[0], U[1]), Math.Max(Math.Min(U[0], U[2]), Math.Min(U[1], U[2])));
                    X[2] = Math.Max(U[0], Math.Max(U[1], U[2]));
                    L = 3;
                }
                else
                {
                    // only one real solution!
                    DIS = Math.Sqrt(DIS);
                    X[0] = CubicRoot(Q + DIS) + CubicRoot(Q - DIS) - W;
                    L = 1;
                }
            }
            else if (Math.Abs(A[2]) > RESOLUTION) // quadratic equation
            {
                
                P = 0.5 * A[1] / A[2];
                DIS = Math.Pow(P, 2) - A[0] / A[2];
                if (DIS > 0.0) // 2 real solutions
                {
                    double tmp = Math.Sqrt(DIS);
                    X[0] = -P - tmp;
                    X[1] = -P + tmp;
                    L = 2;
                }
                else  // no real solution
                    L = 0;  
            }
            else if (Math.Abs(A[1]) > RESOLUTION) //linear equation
            {
                X[0] = A[0] / A[1];
                L = 1;
            }
            else  //no equation
                L = 0;
            
            for (int i = 0; i < L; ++i) // perform one step of a newton iteration in order to minimize round-off errors
            {
                X[i] = X[i] - (A[0] + X[i] * (A[1] + X[i] * (A[2] + X[i] * A[3]))) / (A[1] + X[i] * (2.0 * A[2] + X[i] * 3.0 * A[3]));
                //	Console.WriteLine("\n X inside cubic %.15e\n", X[i]);
            }

            return 0;
        }

        static private double CubicRoot(double Z)
        {
            if (Z < 0)
                return -Math.Pow(Math.Abs(Z), THIRD);
            else
                return  Math.Pow(Math.Abs(Z), THIRD);
        }
    }
}
