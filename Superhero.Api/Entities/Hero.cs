namespace Superhero.Api.Entities
{
    public class Hero
    {
        public Guid Id { get; set; }
        public string? HeroName { get; set; }
        public string? Superpower { get; set; }
        public int PowerLevel { get; set; }
        public Organization? Organization { get; set; }
        public Guid? OrganizationId { get; set; }

        public Hero() { }

        public override string ToString()
        {
            return $"{HeroName} has {Superpower} abilities. Power Level: {PowerLevel}";
        }
    }
}
