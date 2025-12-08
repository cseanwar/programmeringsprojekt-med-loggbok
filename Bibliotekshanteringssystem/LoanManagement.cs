using LibraryManagement.Models;

public class LoanService
{
    private List<Loan> _loans = new List<Loan>();
    private int _nextLoanId = 1;

    public string BorrowBook(string username, Book book)
    {
        if (book.IsBorrowed)
            return "Book is already borrowed.";

        book.IsBorrowed = true;

        var loan = new Loan
        {
            LoanId = _nextLoanId++,
            Username = username,
            BookTitle = book.Title,
            LoanDate = DateTime.Now
        };

        _loans.Add(loan);

        return $"Book '{book.Title}' borrowed successfully.";
    }

    public string ReturnBook(string username, Book book)
    {
        var loan = _loans.FirstOrDefault(l =>
            l.Username == username &&
            l.BookTitle.Equals(book.Title, StringComparison.OrdinalIgnoreCase) &&
            l.ReturnDate == null);

        if (loan == null)
            return "This user has not borrowed this book.";

        loan.ReturnDate = DateTime.Now;
        book.IsBorrowed = false;

        return $"Book '{book.Title}' returned successfully.";
    }
}
