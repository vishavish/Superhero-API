namespace Superhero.Api.Entities
{
    public class Organization
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        List<Hero> Heroes { get; set; }
    }
}
