using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    /// <summary>
    ///   The value of the type a cell has ingame
    /// </summary>
    public enum MarkType
    {
        /// <summary>
        /// The cell hasn't been clicked yet
        /// </summary>
        Free,
        /// <summary>
        /// The cell is a O
        /// </summary>
        Nought,
        /// <summary>
        /// The cell is a X
        /// </summary>
        Cross
    }
}
