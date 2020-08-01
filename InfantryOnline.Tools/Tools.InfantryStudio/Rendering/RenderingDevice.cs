using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.InfantryStudio.Rendering
{
    /// <summary>
    /// Main rendering interface between Infantry Online and the underlying Direct3D 11 API.
    /// </summary>
    public class RenderingDevice
    {
        /// <summary>
        /// Returns the Direct3D 11 Device object.
        /// </summary>
        public SlimDX.Direct3D11.Device Device { get; private set; }

        /// <summary>
        /// Returns the Direct3D 11 Immediate Device Context object.
        /// </summary>
        public SlimDX.Direct3D11.DeviceContext ImmediateContext { get; private set; }

        /// <summary>
        /// Initializes the Rendering Device, display surface and other necessary components.
        /// </summary>
        public void Initialize()
        {

        }

        /// <summary>
        /// Releases all the handles held by the Direct3D11 interface.
        /// </summary>
        public void Destroy()
        {

        }

        #region Private



        #endregion  
    }
}
