using Microsoft.AspNetCore.Mvc.Rendering;

namespace GastroFaza.Models
{
    public static class NationsList
    {
        public static List<Nation> GetNations()
        {
            return new List<Nation>()
            {
                 new Nation() { Name = "Poland" },
                 new Nation() { Name = "Great Britain" },
                 new Nation() { Name = "Norway" },
                 new Nation() { Name = "Sweden" },
                 new Nation() { Name = "Finland" },
                 new Nation() { Name = "Denmark" },
                 new Nation() { Name = "Iceland" },
                 new Nation() { Name = "Ireland" },
                 new Nation() { Name = "France" },
                 new Nation() { Name = "Netherlands" },
                 new Nation() { Name = "Luxembourg" },
                 new Nation() { Name = "Spain" },
                 new Nation() { Name = "Portugal" },
                 new Nation() { Name = "Andorra" },
                 new Nation() { Name = "Switzerland" },
                 new Nation() { Name = "Liechtenstein" },
                 new Nation() { Name = "Italy" },
                 new Nation() { Name = "San Marino" },
                 new Nation() { Name = "Vatican" },
                 new Nation() { Name = "Malta" },
                 new Nation() { Name = "Croatia" },
                 new Nation() { Name = "Slovenia" },
                 new Nation() { Name = "Bosnia and Herzegovina" },
                 new Nation() { Name = "Serbia" },
                 new Nation() { Name = "Montenegro" },
                 new Nation() { Name = "Kosovo" },
                 new Nation() { Name = "North Macedonia" },
                 new Nation() { Name = "Albania" },
                 new Nation() { Name = "Greece" },
                 new Nation() { Name = "Türkiye" },
                 new Nation() { Name = "Cyprus" },
                 new Nation() { Name = "Bulgaria" },
                 new Nation() { Name = "Hungary" },
                 new Nation() { Name = "Moldova" },
                 new Nation() { Name = "Slovakia" },
                 new Nation() { Name = "Czech Republic" },
                 new Nation() { Name = "Lithuania" },
                 new Nation() { Name = "Latvia" },
                 new Nation() { Name = "Estonia" },
                 new Nation() { Name = "Belarus" },
                 new Nation() { Name = "Ukraine" },
                 new Nation() { Name = "Russia" },
                 new Nation() { Name = "Monaco" },
                 new Nation() { Name = "Azerbaijan" },
                 new Nation() { Name = "Georgia" },
                 new Nation() { Name = "Armenia" },
                 new Nation() { Name = "Austria" },
                 new Nation() { Name = "Romania" },
                 new Nation() { Name = "Germany" }
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
