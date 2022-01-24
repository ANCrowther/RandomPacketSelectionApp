using Microsoft.Win32;
using RandomPacketSelection.Model;
using RandomPacketSelection.Other;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace RandomPacketSelection.ViewModel
{
    public class UserVM : INotifyPropertyChanged
    {
        private ExcelReader excelReader;
        private OpenFileDialog openFileDialog;
        private bool isFileLoaded = false;
        private Random random;

        public UserVM()
        {
            selectRandomPacketCommand = new RelayCommand(LoadRandomClient);
            selectThreeRandomCommand = new RelayCommand(LoadThreeRandomClients);
            openFileCommand = new RelayCommand(OpenExcelFile);
            random = new Random();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        private readonly RelayCommand selectRandomPacketCommand;
        public RelayCommand SelectRandomPacketCommand
        {
            get { return selectRandomPacketCommand; }
        }

        private readonly RelayCommand selectThreeRandomCommand;
        public RelayCommand SelectThreeRandomCommand
        {
            get { return selectThreeRandomCommand; }
        }

        private readonly RelayCommand openFileCommand;
        public RelayCommand OpenFileCommand
        {
            get { return openFileCommand; }
        }

        // Populates the left datagrid panel with complete employee/client list.
        private ObservableCollection<ExcelInputs> excelInputList;
        public ObservableCollection<ExcelInputs> ExcelInputList
        {
            get { return excelInputList; }
            set { excelInputList = value; OnPropertyChanged("ExcelInputList"); }
        }

        // Populates the right datagrid panel with up to three randomly chosen clients.
        private ObservableCollection<ExcelInputs> threeClientList;
        public ObservableCollection<ExcelInputs> ThreeClientList
        {
            get { return threeClientList; }
            set { threeClientList = value; OnPropertyChanged("ThreeClientList"); }
        }

        // Used to help update the left datagrid, allowing the datagrid to change selected clients to a red font.
        private Client randomClient;

        private int totalClientCount;
        public int TotalClientCount
        {
            get { return totalClientCount; }
            set { totalClientCount = value; OnPropertyChanged("TotalClientCount"); }
        }

        private int totalEmployeeCount;
        public int TotalEmployeeCount
        {
            get { return totalEmployeeCount; }
            set { totalEmployeeCount = value; OnPropertyChanged("TotalEmployeeCount"); }
        }

        // Opens the Open File Dialog so the user can select excel file.
        private void OpenExcelFile()
        {
            openFileDialog = new OpenFileDialog();
            Nullable<bool> result = openFileDialog.ShowDialog();

            if (result == true)
            {
                excelReader = new ExcelReader(openFileDialog.FileName);
            }

            LoadList();
            TotalClientCount = GetTotalClientCount();
            TotalEmployeeCount = GetTotalEmployeeCount();
            isFileLoaded = true;
        }

        // Gets the inputs from the excel file. Prepares the datagrids for inputs.
        private void LoadList()
        {
            ExcelInputList = new ObservableCollection<ExcelInputs>(excelReader.ExcelInputsList);
            ThreeClientList = new ObservableCollection<ExcelInputs>();
            randomClient = new Client();
        }

        // Iterates through the ExcelInputsList and counts number of clients not yet checked.
        private int GetAvailableClientCount()
        {
            int count = 0;

            foreach (ExcelInputs ex in ExcelInputList)
            {
                if (ex.ClientChecked == false)
                {
                    count++;
                }
            }

            return count;
        }

        // SelectRandomPacketCommand calls this method. Checks to ensure excel was loaded first,
        // then checks if there are anymore available clients to randomly select. Calls GetRandomClient()
        // if there are, calls ResetClientCheckedStatus() if not.
        private void LoadRandomClient()
        {
            if(isFileLoaded)
            {
                if (GetAvailableClientCount() > 0)
                {
                    GetRandomClient();
                }
                else
                {
                    randomClient = null;
                    ResetClientCheckedStatus();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("You must open your Excel file first.");
            }
        }

        // Looks through the ExcelInputList for the randomly selected index, populates the
        // ThreeLClientList with randomly chosen employee/client names.
        private void GetRandomClient()
        {
            int randomNumber = RNG(GetAvailableClientCount());
            int index = 0;

            foreach(ExcelInputs ex in ExcelInputList)
            {
                if (ex.ClientChecked == false)
                {
                    index++;
                    if (index == randomNumber)
                    {
                        randomClient = new Client { FirstName = ex.ClientFirstName, 
                                                    LastName = ex.ClientLastName, 
                                                    IsChecked = ex.ClientChecked };
                        // Prevents the ThreeClientList from having more than three inputs.
                        if(ThreeClientList.Count < 3)
                        {
                            ThreeClientList.Add(ex);
                        }
                        else
                        {   
                            ThreeClientList.Clear();
                            ThreeClientList.Add(ex);
                        }
                    }
                }
            }

            UpdateDataGrid();
        }

        // Updates the ExcelInputList datagrid to reflect newly selected employee/client. The datagrid turns
        // the selected employee/client from black to red foreground color.
        private void UpdateDataGrid()
        {
            ExcelInputList.Where(c => c.ClientFullName == randomClient.FullName).Select(c => { c.ClientChecked = true; return c; }).ToList();
            List<ExcelInputs> tempList = new List<ExcelInputs>();
            tempList = ExcelInputList.ToList();
            ExcelInputList.Clear();
            ExcelInputList = new ObservableCollection<ExcelInputs>(tempList);
        }

        // SelectThreeRandomCommand calls this method. Checks to ensure excel was loaded first,
        // then checks if there are anymore available clients to randomly select. Calls GetThreeRandomClientList()
        // if there are, calls ResetClientCheckedStatus() if not.
        private void LoadThreeRandomClients()
        {
            if (isFileLoaded)
            {
                if (GetAvailableClientCount() > 0)
                {
                    GetThreeRandomClientList();
                }
                else
                {
                    randomClient = null;
                    ResetClientCheckedStatus();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("You must open your Excel file first.");
            }

            if(randomClient != null)
            {
                UpdateDataGrid();
            }
        }

        // Looks through the ExcelInputList for the randomly selected index, populates the
        // ThreeLClientList with randomly chosen employee/client names. Iterates up to three 
        // times, depending on how many names are already on the ThreeClientList.
        private void GetThreeRandomClientList()
        {
            List<ExcelInputs> output = new List<ExcelInputs>();
            int count = 0;

            if (ThreeClientList.Count == 3)
            {
                ThreeClientList.Clear();
            }

            int size = 3 - ThreeClientList.Count;

            for (int i = 0; i < size; i++)
            {
                count = GetAvailableClientCount();
                if(count > 0)
                {
                    output.Add(GetNewClient());
                }
                else
                {
                    randomClient = null;
                    ResetClientCheckedStatus();
                }
            }

            // Must populate the temporary output list first, then add it to the ThreeClientList.
            // The list overwrite otherwise( e.g., 1 name already on list, would remove it and add
            // 2 names, when it should have added to the 1st one for a total of 3).
            foreach(ExcelInputs input in output)
            {
                ThreeClientList.Add(input);
            }
        }

        // Find and returns the randomly selected client for the GetThreeRandomClientList() method.
        // Updates the randomClient for updating the ExcelInputList datagrid.
        private ExcelInputs GetNewClient()
        {
            ExcelInputs output = new ExcelInputs();

            int randomNumber = RNG(GetAvailableClientCount());
            int index = 0;

            foreach (ExcelInputs ex in ExcelInputList)
            {
                if (ex.ClientChecked == false)
                {
                    index++;
                    if (index == randomNumber)
                    {
                        output = new ExcelInputs { EmployeeName=ex.EmployeeName, ClientFirstName=ex.ClientFirstName, ClientLastName=ex.ClientLastName, ClientChecked=ex.ClientChecked };
                        randomClient = new Client { FirstName = ex.ClientFirstName, LastName = ex.ClientLastName, IsChecked = ex.ClientChecked };
                    }
                }
            }

            UpdateDataGrid();
            return output;
        }

        // Resets all inputs on ExcelInputList to ClientChecked=false. For the datagrid to update correctly,
        // must update all the statuses, transfer the ExcelInputsList to a temporary list, clear the 
        // ExcelInputsList, then repopulate it. Otherwise, the ExcelInputList datagrid would not refresh
        // correctly. Also, clears the ThreeClientList for future use.
        private void ResetClientCheckedStatus()
        {
            ExcelInputList.Where(c => c.ClientChecked).Select(c => { c.ClientChecked = false; return c; }).ToList();
            List<ExcelInputs> output = new List<ExcelInputs>();

            foreach(ExcelInputs ex in ExcelInputList)
            {
                output.Add(ex);
            }

            ExcelInputList.Clear();

            foreach(ExcelInputs ex in output)
            {
                ExcelInputList.Add(ex);
            }

            ThreeClientList.Clear();
        }

        // RNG between 1 and total available client for selection count.
        private int RNG(int count)
        {
            return random.Next(1, count);
        }

        // Return total number of clients on the ExcelInputList.
        private int GetTotalClientCount()
        {
            return ExcelInputList.Count;
        }

        // Return total number of employees on the ExcelInputList.
        private int GetTotalEmployeeCount()
        {
            int count = (from x in ExcelInputList select x.EmployeeName).Distinct().Count();
            return count;
        }
    }
}
