namespace StepRunner.Models
{
    public class Step
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Variables Inputs { get; set; }
        public bool IsDisabled { get; set; }
    }
}