namespace SparkAPI.Model
{
    public class VariableResponse
    {
        public string Cmd { get; set; }
        public string Name { get; set; }
        public string Result { get; set; }

        public CoreInfo CoreInfo { get; set; }
    }
}
