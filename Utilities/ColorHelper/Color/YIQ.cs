namespace ColorHelper
{
    public class YIQ : IColor
    {
        public double Y { get; set; }
        public double I { get; set; }
        public double Q { get; set; }

        public YIQ(double y, double i, double q)
        {
            this.Y = y;
            this.I = i;
            this.Q = q;
        }

        public override bool Equals(object obj)
        {
            var result = (YIQ)obj;

            return (
                result != null &&
                this.Y == result.Y &&
                this.I == result.I &&
                this.Q == result.Q);
        }

        public override string ToString()
        {
            return $"{this.Y} {this.I} {this.Q}";
        }
    }
}