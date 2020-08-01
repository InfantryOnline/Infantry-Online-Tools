using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.LvbRebase
{
    /// <summary>
    /// Loads in a level file and prints out to standard output the lists of lvb.blo CFS entries that have, and do not have,
    /// a corresponding non-lvb.blo entry somewhere in the current working directory.
    /// 
    /// The user can furthermore pass in a command to update the level file so that it remaps the lvb.blo entries over to the
    /// corresponding non-lvb.blo ones.
    /// </summary>
    /// <remarks>
    /// This program is useful for cleaning out lvb.blo files which cause bloat to occur.
    /// </remarks>
    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
