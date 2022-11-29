using GastroFaza.Models.Enum;

namespace GastroFaza.Models
{
    public class MenuModel
    {
        public string SearchPhrase { get; set; } = null!;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; } = null!;
        public SortDirection SortDirection { get; set; }
    }
}