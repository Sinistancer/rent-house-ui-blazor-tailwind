namespace daryon_house_ui.Client.Model
{
    public class ApplicationUserRequest
    {
        public string Name { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Address { get; set; }
        public string Locality { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmed { get; set; }
    }
}
