namespace ProjectTest.Models
{
    public class AccountOpeningModel
    {
        public int CurrentStep { get; set; } = 1;

        // Step 1: Product Selection
        public string SelectedProduct { get; set; }

        // Step 2: Personal Information
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NIDNumber { get; set; }

        // Step 3: Contact Information
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        // Step 4: FATCA Information
        public bool IsUSCitizen { get; set; }
        public string TaxIdentificationNumber { get; set; }

        // Step 5: Nominee Information
        public string NomineeName { get; set; }
        public string NomineeRelationship { get; set; }

        // Step 6: Document Upload
        public IFormFile NIDFrontImage { get; set; }
        public IFormFile NIDBackImage { get; set; }

        // Step 7: Signature & Preview
        public string SignatureData { get; set; } // For digital signature capture
    }
}
