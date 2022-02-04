using ExcelMapper;
using System;
using System.ComponentModel;

namespace RandomPacketSelection.Model
{
    public class ExcelInputs : INotifyPropertyChanged, IEquatable<ExcelInputs>
    {
        [ExcelColumnName("First Name")]
        public string ClientFirstName { get; set; }
        
        [ExcelColumnName("Last Name")]
        public string ClientLastName { get; set; }
        
        [ExcelColumnName("Employee")]
        public string EmployeeName { get; set; }
        
        [ExcelColumnName("Client Checked")]
        public bool ClientChecked { get; set; } = false;

        // The original excel list used kept the clients' first and last names in different columns.
        // For simple checks in the program, comparing the full name was easier than double checks.
        [ExcelIgnore]
        public string ClientFullName { get { return ClientFirstName + " " + ClientLastName; } }

        // Required for IEquatable<>, used to prevent duplicate employee entries from being created.
        public bool Equals(ExcelInputs other)
        {
            return ClientFullName.Equals(other.ClientFullName);
        }

        // Required for IEquatable<>, used to prevent duplicate employee entries from being created.
        public override int GetHashCode()
        {
            return ClientFullName.GetHashCode() ^ (ClientFullName == null ? 0 : ClientFullName.GetHashCode());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}
