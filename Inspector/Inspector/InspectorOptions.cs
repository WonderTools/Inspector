namespace WonderTools.Inspector
{
    public class InspectorOptions
    {
        public InspectorOptions()
        {
            BaseEndPoint = "";
            IsCorsEnabled = false;
        }
        public string BaseEndPoint { get; set; }
        public bool IsCorsEnabled { get; set; }

    }
}