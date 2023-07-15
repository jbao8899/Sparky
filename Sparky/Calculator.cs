namespace Sparky
{
    public class Calculator
    {
        public List<int> IntRange = new List<int>();
        public int AddIntegers(int a, int b)
        {
            return a + b;
        }

        public double AddDoubles(double a, double b)
        {
            return a + b;
        }


        public bool IsOddNumber(int a)
        {
            return a % 2 != 0;
        }

        public List<int> GetOddRange(int min, int max)
        {
            IntRange.Clear();
            for (int i = min; i <= max; i++)
            {
                if (i % 2 != 0)
                {
                    IntRange.Add(i);
                }
            }
            return IntRange;
        }
    }
}