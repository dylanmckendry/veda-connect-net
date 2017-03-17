using System;

namespace VedaConnect
{
    public class Individual
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string[] OtherNames { get; set; }
        public string FamilyName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public string LicenseNumber { get; set; }

        public Address[] Addresses { get; set; }

        public Employer[] Employers{ get; set; }
    }
}