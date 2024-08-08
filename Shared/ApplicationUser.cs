namespace daryon_house_ui.Shared
{
    public class ApplicationUser : ApplicationUserDropdown
    {
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Address { get; set; }
        public string Locality { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmed { get; set; }
    }
}
