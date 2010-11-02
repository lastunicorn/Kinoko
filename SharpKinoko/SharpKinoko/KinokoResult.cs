using System;
using System.Collections.Generic;
using System.Text;

namespace DustInTheWind.SharpKinoko
{
    public class KinokoResult
    {
        private List<double> times;

        private double average;
        public double Average
        {
            get { return average; }
        }

        public KinokoResult()
        {
            times = new List<double>();
            average = 0D;
        }

        public void AddValue(double time)
        {
            times.Add(time);
        }

        public void Calculate()
        {
            average = Math.Avarage(times);
        }
    }
}
