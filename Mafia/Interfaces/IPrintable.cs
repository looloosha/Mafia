using System;
namespace Mafia.Interfaces
{
    /// <summary>
    /// Enforces that objects should be able to be printable to the console if implemented.
    /// </summary>
    public interface IPrintable
    {
        /// <summary>
        /// Prints out the Object as a useful summary to the console.
        /// </summary>
        void print();
    }
}