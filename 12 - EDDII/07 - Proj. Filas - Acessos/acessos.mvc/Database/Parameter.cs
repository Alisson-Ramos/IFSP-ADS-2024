namespace acessos.mvc.Model.Database
{
    public class Parameter
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public Parameter(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}
