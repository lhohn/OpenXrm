namespace OpenXrm.Core.Core.Abstractions
{
    public class Entity
    {
        public Guid Id { get; set; }
        public string LastChanger { get; set; }
        public DateTime LastChanged { get; set; }
    }
}
