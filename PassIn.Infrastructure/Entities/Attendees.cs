namespace PassIn.Infrastructure.Entities
{
    public class Attendees
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public Guid Event_Id { get; set; }
        public DateTime Created_At { get; set; }
        public CheckIn? CheckIn { get; set; }
    }
}
