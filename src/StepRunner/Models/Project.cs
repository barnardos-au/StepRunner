namespace StepRunner.Models
{
    public class Project
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public Variables Variables { get; set; }
        public Steps Steps { get; set; }
    }
}
