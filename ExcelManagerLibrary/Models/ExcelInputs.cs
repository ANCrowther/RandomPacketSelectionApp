using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using ExcelManagerLibrary.Helpers;

namespace ExcelManagerLibrary.Models
{
    internal class ExcelInputs : INotifyPropertyChanged, IEquatable<ExcelInputs>
    {
        [ExcelColumnName(@"Male/Female")]
        public string Sex { get; set; }
        
        [ExcelColumnName("Race")]
        public string Race { get; set; }
        
        [ExcelColumnName(@"IE/CI")]
        public string IE_CI { get; set; }
        
        [ExcelColumnName("Primary Disability")]
        public string PrimaryDisability { get; set; }
        
        [ExcelColumnName("STATUS")]
        public string Status { get; set; }
        
        [ExcelColumnName("FILE")]
        public string File { get; set; }
        
        [ExcelColumnName("DVR Status")]
        public string DVR_Status { get; set; }

        [ExcelColumnName("Monitoring Plan Start Date")]
        public DateTime MonitoringPlan_StartDate { get; set; }

        [ExcelColumnName("LAST NAME")]
        public string LastName { get; set; }

        [ExcelColumnName("FIRST NAME")]
        public string FirstName { get; set; }

        [ExcelColumnName("WVSC")]
        public string WVSC { get; set; }

        [ExcelColumnName("FILE TRANSFER")]
        public string FileTransfer { get; set; }

        [ExcelColumnName("Acuity")]
        public string Acuity { get; set; }

        [ExcelColumnName("Additional Staff")]
        public string AdditionalStaff { get; set; }

        [ExcelColumnName("Funding Increase Exp. Date")]
        public DateTime FundingIncrease_ExpDate { get; set; }

        [ExcelColumnName("ETR Hours")]
        public double ETRHours { get; set; }

        [ExcelColumnName("Target Hours")]
        public double TargetHours { get; set; }

        [ExcelColumnName("Up to Hours")]
        public double UpToHours { get; set; }

        [ExcelColumnName("-Job Coach Hours")]
        public double JobCoachHours { get; set; }

        [ExcelColumnName("Job Coach")]
        public string Entry { get; set; }

        [ExcelColumnName("ENTRY")]
        public string Coaching { get; set; }

        [ExcelColumnName("Coaching")]
        public string JobStart { get; set; }

        [ExcelColumnName("Job Start")]
        public string JobPlace { get; set; }

        [ExcelColumnName("Job Place")]
        public decimal CurrentWage { get; set; }

        [ExcelColumnName("Current Wages")]
        public double HoursPerWeek { get; set; }

        [ExcelColumnName("Hours per Week")]
        public DateTime JobLoss { get; set; }

        [ExcelColumnName("Job Loss Reason")]
        public string JobLossReason { get; set; }

        [ExcelColumnName("CLOSURE")]
        public DateTime Closure { get; set; }

        [ExcelColumnName("CLOSURE REASON")]
        public string ClosureReason { get; set; }


        [ExcelIgnore]
        public bool ClientChecked { get; set; } = false;

        [ExcelIgnore]
        public string FirstLastName { get { return $"{FirstName} {LastName}"; } }

        [ExcelIgnore]
        public string LastFirstName { get { return $"{LastName}, {FirstName}"; } }

        public bool Equals(ExcelInputs other)
        {
            return FirstLastName.Equals(other.FirstLastName);
        }

        public override int GetHashCode()
        {
            return FirstLastName.GetHashCode() ^ (FirstLastName == null ? 0 : FirstLastName.GetHashCode());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}
