using Microsoft.AspNetCore.Mvc.Rendering;

namespace GastroFaza.Models
{
    public static class NationsList
    {
        public static List<Nation> GetNations()
        {
            return new List<Nation>()
            {
                new Nation() { Name = "Polska" },
                new Nation() { Name = "Wielka Brytania" },
                new Nation() { Name = "Norwegia" },
                new Nation() { Name = "Szwecja" },
                new Nation() { Name = "Finlandia" },
                new Nation() { Name = "Dania" },
                new Nation() { Name = "Islandia" },
                new Nation() { Name = "Irlandia" },
                new Nation() { Name = "Francja" },
                new Nation() { Name = "Holandia" },
                new Nation() { Name = "Luksemburg" },
                new Nation() { Name = "Hiszpania" },
                new Nation() { Name = "Portugalia" },
                new Nation() { Name = "Andora" },
                new Nation() { Name = "Szwajcaria" },
                new Nation() { Name = "Lichtenstein" },
                new Nation() { Name = "Włochy" },
                new Nation() { Name = "San Marino" },
                new Nation() { Name = "Watykan" },
                new Nation() { Name = "Malta" },
                new Nation() { Name = "Chorwacja" },
                new Nation() { Name = "Słowenia" },
                new Nation() { Name = "Bośnia i Hercegowina" },
                new Nation() { Name = "Serbia" },
                new Nation() { Name = "Czarnogóra" },
                new Nation() { Name = "Kosowo" },
                new Nation() { Name = "Macedonia Północna" },
                new Nation() { Name = "Albania" },
                new Nation() { Name = "Grecja" },
                new Nation() { Name = "Turcja" },
                new Nation() { Name = "Cypr" },
                new Nation() { Name = "Bułgaria" },
                new Nation() { Name = "Węgry" },
                new Nation() { Name = "Mołdawia" },
                new Nation() { Name = "Słowacja" },
                new Nation() { Name = "Czechy" },
                new Nation() { Name = "Litwa" },
                new Nation() { Name = "Łotwa" },
                new Nation() { Name = "Estonia" },
                new Nation() { Name = "Białoruś" },
                new Nation() { Name = "Ukraina" },
                new Nation() { Name = "Rosja" },
                new Nation() { Name = "Monako" },
                new Nation() { Name = "Azerbejdżan" },
                new Nation() { Name = "Gruzja" },
                new Nation() { Name = "Armenia" },
                new Nation() { Name = "Austria" },
                new Nation() { Name = "Rumunia" },
                new Nation() { Name = "Niemcy" }
            };
        }

        public static List<SelectListItem> GetSelectNations()
        {
            var list = new List<SelectListItem>();
            var nationsList = GetNations();

            foreach (var nation in nationsList)
            {
                list.Add(new SelectListItem() { Text = nation.Name, Value = nation.Name });
            }

            return list;
        }
    }
}
