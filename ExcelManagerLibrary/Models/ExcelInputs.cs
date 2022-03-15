using System;
using System.ComponentModel;
using ExcelManagerLibrary.Helpers;

namespace ExcelManagerLibrary.Models
{
    public class ExcelInputs : INotifyPropertyChanged, IEquatable<ExcelInputs>
    {
        [ExcelColumnName(@"Male/Female")]
        public string Sex { get; set; } = string.Empty;
        
        [ExcelColumnName("Race")]
        public string Race { get; set; } = string.Empty;

        [ExcelColumnName(@"IE/CI")]
        public string IE_CI { get; set; } = string.Empty;

        [ExcelColumnName("Primary Disability")]
        public string PrimaryDisability { get; set; } = string.Empty;

        [ExcelColumnName("STATUS")] // 5
        public string Status { get; set; } = string.Empty;

        [ExcelColumnName("FILE")]
        public string File { get; set; } = string.Empty;

        [ExcelColumnName("DVR Status")]
        public string DVR_Status { get; set; } = string.Empty;

        [ExcelColumnName("Monitoring Plan Start Date")]
        public DateTime MonitoringPlan_StartDate { get; set; } = DateTime.MinValue;

        [ExcelColumnName("LAST NAME")]
        public string LastName { get; set; } = string.Empty;

        [ExcelColumnName("FIRST NAME")] // 10
        public string FirstName { get; set; } = string.Empty;

        [ExcelColumnName("WVSC")]
        public string WVSC { get; set; } = string.Empty;

        [ExcelColumnName("FILE TRANSFER")]
        public string FileTransfer { get; set; } = string.Empty;

        [ExcelColumnName("Acuity")]
        public string Acuity { get; set; } = string.Empty;

        [ExcelColumnName("Additional Staff")]
        public string AdditionalStaff { get; set; } = string.Empty;

        [ExcelColumnName("Funding Increase Exp. Date")] // 15
        public DateTime? FundingIncrease_ExpDate { get; set; } = DateTime.MinValue;

        [ExcelColumnName("ETR Hours")]
        public double? ETRHours { get; set; } = 0.0;

        [ExcelColumnName("Target Hours")]
        public double? TargetHours { get; set; } = 0.0;

        [ExcelColumnName("Up to Hours")]
        public double? UpToHours { get; set; } = 0.0;

        [ExcelColumnName("-Job Coach Hours")]
        public double? JobCoachHours { get; set; } = 0.0;

        [ExcelColumnName("Job Coach")] // 20
        public string JobCouach { get; set; } = string.Empty;

        [ExcelColumnName("ENTRY")]
        public string Entry { get; set; } = string.Empty;

        [ExcelColumnName("Coaching")]
        public string Coaching { get; set; } = string.Empty;

        [ExcelColumnName("Job Start")]
        public DateTime? JobStart { get; set; } = DateTime.MinValue;

        [ExcelColumnName("Job Place")]
        public string JobPlace { get; set; } = string.Empty;

        [ExcelColumnName("Current Wage")] // 25
        public decimal? CurrentWage { get; set; } = 0.0m;

        [ExcelColumnName("Hours per Week")]
        public double? HoursPerWeek { get; set; } = 0.0;

        [ExcelColumnName("Job Loss")]
        public DateTime? JobLoss { get; set; } = DateTime.MinValue;

        [ExcelColumnName("Job Loss Reason")]
        public string JobLossReason { get; set; } = string.Empty;

        [ExcelColumnName("CLOSURE")]
        public DateTime? Closure { get; set; } = DateTime.MinValue;

        [ExcelColumnName("CLOSURE REASON")] // 30
        public string ClosureReason { get; set; } = string.Empty;


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
