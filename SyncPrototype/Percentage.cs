
namespace SyncPrototype
{
    public class Percentage
    {
        private readonly ushort percentage;
        public Percentage(ushort value)
        {
            percentage = value;
            if (percentage > 100)
                percentage = 100;
        }

        public static implicit operator int(Percentage percentage)
        {
            return percentage.percentage;
        }

        public static implicit operator Percentage(ushort value)
        {
            return new Percentage(value);
        }

        public override string ToString()
        {
            return percentage.ToString() + "%";
        }
    }
}
