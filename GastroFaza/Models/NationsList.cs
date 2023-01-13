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
