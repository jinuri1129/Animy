namespace ColorHelper
{
    public class YUV : IColor
    {
        public double Y { get; set; }
        public double U { get; set; }
        public double V { get; set; }

        public YUV(double y, double u, double v)
        {
            this.Y = y;
            this.U = u;
            this.V = v;
        }

        public override bool Equals(object obj)
        {
            var result = (YUV)obj;

            return (
                result != null &&
                this.Y == result.Y &&
                this.U == result.U &&
                this.V == result.V);
        }

        public override string ToString()
        {
            return $"{this.Y} {this.U} {this.V}";
        }
    }
}