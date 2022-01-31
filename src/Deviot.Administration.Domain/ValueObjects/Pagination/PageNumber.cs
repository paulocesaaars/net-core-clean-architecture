namespace Deviot.Administration.Domain.ValueObjects.Pagination
{
    public class PageNumber
    {
        public UInt16 Value { get; private set; }

        public PageNumber(int value)
        {
            if(value < 0)
                value = 0;

            Value = (UInt16)value;
        }
    }
}
