namespace LibraryManagement.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        public string Username { get; set; }
        public string BookTitle { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}