using ExcelManagerLibrary.Managers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using WVSRandomizer.Model;
using WVSRandomizer.Utilities;

namespace WVSRandomizer.ViewModel
{
    public class RandomClientViewModel : INotifyPropertyChanged
    {
        private ExcelManager excelManager;
        private OpenFileDialog openFileDialog;
        private bool isFileLoaded = false;
        private Random random;

        public RandomClientViewModel()
        {
            selectRandomPacketCommand = new RelayCommand(LoadRandomClient);
            selectThreeRandomCommand = new RelayCommand(LoadThreeRandomClients);
            openFileCommand = new RelayCommand(OpenExcelFile);
            random = new Random();
        }

        #region RELAY COMMANDS
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

        private void LoadRandomClient()
        {
            if (isFileLoaded)
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
                MessageBoxMessage.SelectExcelFileFirst();
            }
        }

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
                MessageBoxMessage.SelectExcelFileFirst();
            }

            if (randomClient != null)
            {
                UpdateDataGrid();
            }
        }

        private void OpenExcelFile()
        {
            openFileDialog = new OpenFileDialog();
            Nullable<bool> result = openFileDialog.ShowDialog();

            if (result == true)
            {
                excelManager = new ExcelManager(openFileDialog.FileName,"DDMASTER");
                LoadList();
                TotalClientCount = GetTotalClientCount();
                TotalEmployeeCount = GetTotalEmployeeCount();
                isFileLoaded = true;
            }
            else
            {
                //TODO: add an exception
            }
        }

        private int GetTotalEmployeeCount()
        {
            throw new NotImplementedException();
        }

        private int GetTotalClientCount()
        {
            throw new NotImplementedException();
        }

        private void LoadList()
        {
            //TODO: connect to excel inputs.
            ExcelInputList = new ObservableCollection<ClientModel>(GetExcelList());
            ThreeClientList = new ObservableCollection<ClientModel>();
            randomClient = new ClientModel();
        }

        private List<ClientModel> GetExcelList()
        {
            List<ClientModel> list = new List<ClientModel>();
            foreach(var client in excelManager.Inputs)
            {
                list.Add(new ClientModel { FirstName = client.FirstName, 
                                                LastName = client.LastName,
                                                WVSC = client.WVSC});
            }

            return list;
        }
        #endregion

        #region VIEW DATA
        private ObservableCollection<ClientModel> excelInputList;
        public ObservableCollection<ClientModel> ExcelInputList
        {
            get { return excelInputList; }
            set { excelInputList = value; OnPropertyChanged("ExcelInputList"); }
        }

        // Populates the right datagrid panel with up to three randomly chosen clients.
        private ObservableCollection<ClientModel> threeClientList;
        public ObservableCollection<ClientModel> ThreeClientList
        {
            get { return threeClientList; }
            set { threeClientList = value; OnPropertyChanged("ThreeClientList"); }
        }

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
        #endregion

        private int GetAvailableClientCount()
        {
            int count = 0;

            foreach (ClientModel ex in ExcelInputList)
            {
                if (ex.ClientChecked == false)
                {
                    count++;
                }
            }

            return count;
        }

        private ClientModel randomClient;

        private void ResetClientCheckedStatus()
        {
            ExcelInputList.Where(c => c.ClientChecked).Select(c => { c.ClientChecked = false; return c; }).ToList();
            List<ClientModel> output = new List<ClientModel>();

            foreach (ClientModel ex in ExcelInputList)
            {
                output.Add(ex);
            }

            ExcelInputList.Clear();

            foreach (ClientModel ex in output)
            {
                ExcelInputList.Add(ex);
            }

            ThreeClientList.Clear();
        }

        private void GetRandomClient()
        {
            int randomNumber = RNG(GetAvailableClientCount());
            int index = 0;

            foreach (ClientModel ex in ExcelInputList)
            {
                if (ex.ClientChecked == false)
                {
                    index++;
                    if (index == randomNumber)
                    {
                        randomClient = new ClientModel
                        {
                            FirstName = ex.FirstName,
                            LastName = ex.LastName,
                            ClientChecked = ex.ClientChecked
                        };
                        // Prevents the ThreeClientList from having more than three inputs.
                        if (ThreeClientList.Count < 3)
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
            throw new NotImplementedException();
        }

        private int RNG(int count)
        {
            return random.Next(1, count);
        }

        private void UpdateDataGrid()
        {
            //ExcelInputList.Where(c => c.ClientFullName == randomClient.FullName).Select(c => { c.ClientChecked = true; return c; }).ToList();
            //List<ExcelInputs> tempList = new List<ExcelInputs>();
            //tempList = ExcelInputList.ToList();
            //ExcelInputList.Clear();
            //ExcelInputList = new ObservableCollection<ExcelInputs>(tempList);

            throw new NotImplementedException();
        }

        private void GetThreeRandomClientList()
        {
            List<ClientModel> output = new List<ClientModel>();
            int count = 0;

            if (ThreeClientList.Count == 3)
            {
                ThreeClientList.Clear();
            }

            int size = 3 - ThreeClientList.Count;

            for (int i = 0; i < size; i++)
            {
                count = GetAvailableClientCount();
                if (count > 0)
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
            foreach (ClientModel input in output)
            {
                ThreeClientList.Add(input);
            }
        }

        private ClientModel GetNewClient()
        {
            ClientModel output = new ClientModel();

            int randomNumber = RNG(GetAvailableClientCount());
            int index = 0;

            foreach (ClientModel ex in ExcelInputList)
            {
                if (ex.ClientChecked == false)
                {
                    index++;
                    if (index == randomNumber)
                    {
                        output = new ClientModel { FirstName = ex.FirstName, LastName = ex.LastName, WVSC = ex.WVSC, ClientChecked = ex.ClientChecked };
                        randomClient = new ClientModel { FirstName = ex.FirstName, LastName = ex.LastName, WVSC = ex.WVSC, ClientChecked = ex.ClientChecked };
                    }
                }
            }

            UpdateDataGrid();
            return output;
        }



        #region PROPERTY CHANGED EVENT HANDLERS
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
        #endregion
    }
}
