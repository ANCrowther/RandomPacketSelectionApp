using System.ComponentModel;

namespace RandomPacketSelection.Model
{
    public class Client : INotifyPropertyChanged
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // PacketSelector class needs to determine total client count and to ignore
        // clients already selected for the random packet selection.
        public bool IsChecked { get; set; }

        // FullName is used to prevent redundent clients being added to the client list.
        public string FullName 
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public Client() { }

        public Client(string fName, string lName, bool isChecked = false)
        {
            FirstName = fName;
            LastName = lName;
            IsChecked = isChecked;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}
