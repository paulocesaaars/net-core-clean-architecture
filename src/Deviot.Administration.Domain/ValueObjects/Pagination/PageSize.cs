namespace Deviot.Administration.Domain.ValueObjects.Pagination
{
    public class PageSize
    {
        public UInt16 Value { get; private set; }

        public PageSize(int value)
        {
            if (value < 10)
                value = 10;

            if (value > 1000)
                value = 1000;

            Value = (UInt16)value;
        }
    }
}
