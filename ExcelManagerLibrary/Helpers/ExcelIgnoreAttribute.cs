using System;

namespace ExcelManagerLibrary.Helpers
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class ExcelIgnoreAttribute : Attribute
    {
    }
}
