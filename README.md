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

## About

This application is built in C# .NET Framework 4.8

Nuget Packets used:

- ExcelDataReader.Mapping
- ExcelDataReaderHelper
- SYstem.Text.Encoding.CodePages
