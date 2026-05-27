using System.ComponentModel.DataAnnotations;

namespace Budget1.Models
{
    public enum RecurrenceEndType
    {
        [Display(Name = "End Date")]
        EndDate = 1,

        [Display(Name = "Until Further Notice")]
        UntilFurtherNotice = 2,

        [Display(Name = "Number of payments")]
        NumberOfOccurrences = 3
    }
}
