namespace FuzzySetsCalc.Models
{
	public class DataPoint
	{
		public DataPoint(double? x, double? y)
		{
			this.X = x;
			this.Y = y;
		}

		public double? X = null;

		public double? Y = null;
	}
}
