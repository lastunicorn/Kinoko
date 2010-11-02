using System.Collections.Generic;
namespace DustInTheWind.SharpKinoko
{
    internal class Math
    {
        public static double Avarage(double[] values)
        {
            double sum = 0;

            for (int i = 0; i < values.Length; i++)
            {
                sum += values[i];
            }

            return sum / (double)values.Length;
        }

        public static double Avarage(IList<double> values)
        {
            double sum = 0;

            for (int i = 0; i < values.Count; i++)
            {
                sum += values[i];
            }

            return sum / (double)values.Count;
        }

        public static double Avarage(IEnumerable<double> values)
        {
            double sum = 0;
            int count = 0;

            foreach (double value in values)
            {
                sum += value;
                count++;
            }

            return sum / (double)count;
        }
    }
}
