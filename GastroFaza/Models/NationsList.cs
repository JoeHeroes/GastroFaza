namespace GastroFaza.Models
{
    public static class NationsList
    {
        public static List<Nation> GetNations()
        {
            return new List<Nation>()
            {
                new Nation() { Name = "Polska" },
                new Nation() { Name = "Niemcy" }
            };
        }
    }
}
