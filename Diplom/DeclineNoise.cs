using System;
using System.Collections.Generic;
using System.Linq;

namespace Diplom
{
    class DeclineNoise : IDeclineData
    {
        private Random randOne;
        private Random randTwo;
        private double v1;
        private double v2;
        private double r;
        private double f;
        private SortedDictionary<DateTime, double> noiseData;

        public DeclineNoise()
        {
            randOne = new Random();
            randTwo = new Random();
            noiseData = new SortedDictionary<DateTime, double>();
        }

        public SortedDictionary<DateTime, double> AddDecline(SortedDictionary<DateTime, double> loadData)
        {
            //random noise with normal distribution
            r = 1;
            while (r >= 1)
            {
                v1 = randOne.NextDouble();
                v2 = randTwo.NextDouble();
                r = v1 * v1 + v2 * v2;
            }
            f = Math.Sqrt(-2 * Math.Log(r) / 2);
            var noise = loadData.Values.Last() + v1 * f;

            noiseData.Add(loadData.Keys.Last(), noise);
            return noiseData;
        }
    }
}
