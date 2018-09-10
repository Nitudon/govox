using System;


namespace UdonLib.Commons
{
    public class MersenneTwister
    {
        #region[Private Parameter]
        private const Int16 N = 624;
        private const Int16 M = 397;
        private const UInt32 MATRIX_A = (UInt32)0x9908b0df;   /* constant vector a */
        private const UInt32 UPPER_MASK = (UInt32)0x80000000; /* most significant w-r bits */
        private const UInt32 LOWER_MASK = (UInt32)0x7fffffff; /* least significant r bits */
        private static UInt32[] mt; /* the array for the state vector  */
        private static UInt16 mti; /* mti==N+1 means mt[N] is not initialized */
        private static UInt32[] mag01;
        #endregion

        #region "Constructor"

        public MersenneTwister(UInt32 s)
        {
            MT();
            init_genrand(s);
        }

        public MersenneTwister()
        {
            MT();

            // auto generate seed for .NET
            UInt32[] seed_key = new UInt32[6];
            Byte[] rnseed = new Byte[8];

            seed_key[0] = (UInt32)System.DateTime.Now.Millisecond;
            seed_key[1] = (UInt32)System.DateTime.Now.Second;
            seed_key[2] = (UInt32)System.DateTime.Now.DayOfYear;
            seed_key[3] = (UInt32)System.DateTime.Now.Year;
            ;
            System.Security.Cryptography.RandomNumberGenerator rn
                = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rn.GetNonZeroBytes(rnseed);

            seed_key[4] = ((UInt32)rnseed[0] << 24) | ((UInt32)rnseed[1] << 16)
                | ((UInt32)rnseed[2] << 8) | ((UInt32)rnseed[3]);
            seed_key[5] = ((UInt32)rnseed[4] << 24) | ((UInt32)rnseed[5] << 16)
                | ((UInt32)rnseed[6] << 8) | ((UInt32)rnseed[7]);

            init_by_array(seed_key);

            rn = null;
            seed_key = null;
            rnseed = null;
        }

        public MersenneTwister(UInt32[] init_key)
        {
            MT();

            init_by_array(init_key);
        }

        private void MT()
        {
            mt = new UInt32[N];

            mag01 = new UInt32[] { 0, MATRIX_A };
            /* mag01[x] = x * MATRIX_A  for x=0,1 */

            mti = N + 1;
        }

        #endregion

        #region[Generate]
        private static void init_genrand(UInt32 s)
        {
            mt[0] = s;

            for (mti = 1; mti < N; mti++)
            {
                mt[mti] =
                    ((UInt32)1812433253 * (mt[mti - 1] ^ (mt[mti - 1] >> 30)) + mti);
                /* See Knuth TAOCP Vol2. 3rd Ed. P.106 for multiplier. */
                /* In the previous versions, MSBs of the seed affect   */
                /* only MSBs of the array mt[].                        */
                /* 2002/01/09 modified by Makoto Matsumoto             */
            }
        }

        private void init_by_array(UInt32[] init_key)
        {
            UInt32 i, j;
            Int32 k;
            Int32 key_length = init_key.Length;

            init_genrand(19650218);
            i = 1; j = 0;
            k = (N > key_length ? N : key_length);

            for (; k > 0; k--)
            {
                mt[i] = (mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 30)) * (UInt32)1664525))
                    + init_key[j] + (UInt32)j; /* non linear */
                i++; j++;
                if (i >= N) { mt[0] = mt[N - 1]; i = 1; }
                if (j >= key_length) j = 0;
            }
            for (k = N - 1; k > 0; k--)
            {
                mt[i] = (mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 30)) * (UInt32)1566083941))
                    - (UInt32)i; /* non linear */
                i++;
                if (i >= N) { mt[0] = mt[N - 1]; i = 1; }
            }

            mt[0] = 0x80000000; /* MSB is 1; assuring non-zero initial array */
        }

        private static UInt32 genrand_Int32()
        {
            UInt32 y;

            if (mti >= N)
            { /* generate N words at one time */
                Int16 kk;

                if (mti == N + 1)   /* if init_genrand() has not been called, */
                    init_genrand(5489); /* a default initial seed is used */

                for (kk = 0; kk < N - M; kk++)
                {
                    y = ((mt[kk] & UPPER_MASK) | (mt[kk + 1] & LOWER_MASK)) >> 1;
                    mt[kk] = mt[kk + M] ^ mag01[mt[kk + 1] & 1] ^ y;
                }
                for (; kk < N - 1; kk++)
                {
                    y = ((mt[kk] & UPPER_MASK) | (mt[kk + 1] & LOWER_MASK)) >> 1;
                    mt[kk] = mt[kk + (M - N)] ^ mag01[mt[kk + 1] & 1] ^ y;
                }
                y = ((mt[N - 1] & UPPER_MASK) | (mt[0] & LOWER_MASK)) >> 1;
                mt[N - 1] = mt[M - 1] ^ mag01[mt[0] & 1] ^ y;

                mti = 0;
            }

            y = mt[mti++];

            /* Tempering */
            y ^= (y >> 11);
            y ^= (y << 7) & 0x9d2c5680;
            y ^= (y << 15) & 0xefc60000;
            y ^= (y >> 18);

            return y;
        }

        public double genrand_real()
        {
            return genrand_Int32() * ((double)1.0 / 4294967295.0);
        }
        #endregion
    }
        #region[StaticMethod]
    public class RandMT {

        private static MersenneTwister MT;

        /// <summary>
        /// [0-1]double
        /// </summary>
        /// <returns></returns>
        /// 
        public static double GenerateRate()
        {
            if(MT == null)
            {
                MT = new MersenneTwister();
            }
            return MT.genrand_real();
        }
        /// <summary>
        /// [0-num]double
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static double GenerateNext(double num)
        {
            if (MT == null)
            {
                MT = new MersenneTwister();
            }
            return MT.genrand_real() * num;
        }

        public static double GenerateRange(double start,double end)
        {
            if (MT == null)
            {
                MT = new MersenneTwister();
            }
            return start + MT.genrand_real() * (end - start);
        }

        public static float GenerateNext(float num)
        {
            if (MT == null)
            {
                MT = new MersenneTwister();
            }
            return (float)MT.genrand_real() * num;
        }

        public static float GenerateRange(float start, float end)
        {
            if (MT == null)
            {
                MT = new MersenneTwister();
            }
            return (float)(start + MT.genrand_real() * (end - start));
        }

        public static int GenerateNext(int num)
        {
            if (MT == null)
            {
                MT = new MersenneTwister();
            }
            return (int)(MT.genrand_real() * (double)num);
        }

        public static int GenerateRange(int start, int end)
        {
            if (MT == null)
            {
                MT = new MersenneTwister();
            }
            return (int)(start + MT.genrand_real() * (double)(end - start));
        }

        #endregion
    }
}
