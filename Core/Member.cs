using System;

namespace Core
{
    public class Member
    {
        public string FullName { get; set; }
        public int MemberId { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public double FineAmount { get; set; }
        public bool IsActive { get; set; }

        public Member(string fullName, int memberId, string email, DateTime registrationDate, double fineAmount, bool isActive)
        {
            FullName = fullName;
            MemberId = memberId;
            Email = email;
            RegistrationDate = registrationDate;
            FineAmount = fineAmount;
            IsActive = isActive;
        }

        public override string ToString()
        {
            return $"[Member]\n" +
                   $"  Full Name   : {FullName}\n" +
                   $"  Member ID   : {MemberId}\n" +
                   $"  Email       : {Email}\n" +
                   $"  Registered  : {RegistrationDate:dd.MM.yyyy}\n" +
                   $"  Fine Amount : {FineAmount:F2} USD\n" +
                   $"  Active      : {IsActive}";
        }
    }
}