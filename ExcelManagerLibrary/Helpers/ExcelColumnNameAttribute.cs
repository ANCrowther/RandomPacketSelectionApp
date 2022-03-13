using System;

namespace ExcelManagerLibrary.Helpers
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    internal class ExcelColumnNameAttribute : Attribute
    {
        public string Name { get; set; }
        public ExcelColumnNameAttribute(string inputName)
        {
            Name = inputName;
        }
    }
}
