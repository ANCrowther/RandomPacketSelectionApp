namespace WVSRandomizer.Model
{
    public class ClientModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WVSC { get; set; }
        public bool ClientChecked { get; set; } = false;
        public string FirstLastName { get { return $"{FirstName} {LastName}"; } }
        public string LastFirstName { get { return $"{LastName}, {FirstName}"; } }
    }
}
