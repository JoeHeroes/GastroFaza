namespace GastroFaza.Models
{
    public class NationsList
    {
        public List<Nation> GetNations()
        {
            return new List<Nation>()
            {
                new Nation() { Name = "Polska" },
                new Nation() { Name = "Niemcy" }
            };
        }
    }
}
