namespace ServiceLayer.Manipulations
{
    public class FiltrationContext
    {
        public string PropertyName { get; set; }
        public object Value { get; set; }

        public FiltrationContext(string propertyName, object value)
        {
            PropertyName = propertyName;
            Value = value;
        }
    }
}