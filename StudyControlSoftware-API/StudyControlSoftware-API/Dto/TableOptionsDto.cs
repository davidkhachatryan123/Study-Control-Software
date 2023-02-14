using System.ComponentModel.DataAnnotations;

namespace StudyControlSoftware_API.Dto
{
    public class TableOptionsDto
    {
        [Required(ErrorMessage = "Sort is required")]
        public string Sort { get; set; } = null!;

        [Required(ErrorMessage = "OrderDirection is required")]
        public string OrderDirection { get; set; } = null!;

        [Required(ErrorMessage = "Page is required")]
        public int Page { get; set; }

        [Required(ErrorMessage = "PageSize is required")]
        public int PageSize { get; set; }
    }
}
