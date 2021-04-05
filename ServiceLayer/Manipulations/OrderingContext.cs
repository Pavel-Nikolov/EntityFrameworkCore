namespace ServiceLayer.Manipulations
{
    public class OrderingContext
    {
        public string PropertyName { get; set; }
        public bool Acssending { get; set; }

        public OrderingContext(string propertyName, bool acssending = true)
        {
            PropertyName = propertyName;
            Acssending = acssending;
        }
    }
}