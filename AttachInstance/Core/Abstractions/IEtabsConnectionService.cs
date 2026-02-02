using ETABSv1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttachInstance.Core.Abstractions
{
    public interface IEtabsConnectionService
    {
        bool IsConnected { get; }
        event EventHandler<bool>? ConnectionChanged;

        void Connect(cOAPI etabsObject);
        void Disconnect();
    }
}
