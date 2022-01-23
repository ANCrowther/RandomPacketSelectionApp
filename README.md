# RandomPacketSelectionApp

## About Application

This application takes in a list of employee/clients then randomly selects clients. The purpose is to assist management in randomly selecting client packets for monthly reviews, without reselecting previously selected clients earlier in the year.

## How to use

- Press the 'Open File' button.
- Select the desired excel form.
- Wait for the data from the excel form to populate the user window.
- Select either 'Select Random Packet' or 'Select 3 Packets' button.

## Notes

From the original request, this program only shows up to three randomly selected clients at a time. If the user selected 'Select Random Packet' once, then selects 'Select 3 Packets', it will still only show three randomly selected clients. The 'Select 3 Packets' will only select the number of clients needed to complete the list.

Do to the sensitive nature of the files being used from the original request, this application does not save the data, nor update the excel spreadsheet. The user will have to manually update the spreadsheet with the 'Client Checked' column with the results.

The excel file must have the following headings for the program to work

- 'First Name'		- client's first name
- 'Last Name'		- client's last name
- 'Employee Name'	- employee's name
- 'Client Checked'	- boolean TRUE or FALSE


## About

This application is built in C# .NET Framework 4.8

Nuget Packets used:

- ExcelDataReader.Mapping
- ExcelDataReaderHelper
- SYstem.Text.Encoding.CodePages
