using System;

namespace Core
{
    public class Loan
    {
        public int LoanId { get; set; }
        public string BookTitle { get; set; }
        public string MemberName { get; set; }
        public DateTime LoanDate { get; set; }
        public double PenaltyPerDay { get; set; }
        public bool IsReturned { get; set; }

        public Loan(int loanId, string bookTitle, string memberName, DateTime loanDate, double penaltyPerDay, bool isReturned)
        {
            LoanId = loanId;
            BookTitle = bookTitle;
            MemberName = memberName;
            LoanDate = loanDate;
            PenaltyPerDay = penaltyPerDay;
            IsReturned = isReturned;
        }

        public override string ToString()
        {
            return $"[Loan]\n" +
                   $"  Loan ID     : {LoanId}\n" +
                   $"  Book        : {BookTitle}\n" +
                   $"  Member      : {MemberName}\n" +
                   $"  Loan Date   : {LoanDate:dd.MM.yyyy}\n" +
                   $"  Penalty/Day : {PenaltyPerDay:F2} USD\n" +
                   $"  Returned    : {IsReturned}";
        }
    }
}