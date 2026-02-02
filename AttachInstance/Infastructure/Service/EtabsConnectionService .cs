using AttachInstance.Core.Abstractions;
using AttachInstance.Infastructure.DataAccess;
using ETABSv1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttachInstance.Infastructure.Service
{
    public sealed class EtabsConnectionService : IEtabsConnectionService
    {
        private readonly EtabsReader _reader;

        public EtabsConnectionService(EtabsReader reader)
        {
            _reader = reader;
        }

        public bool IsConnected => _reader.IsInitialized;

        public event EventHandler<bool>? ConnectionChanged;

        public void Connect(cOAPI etabsObject)
        {
            if (IsConnected) return;
            _reader.Initialize(etabsObject);
            ConnectionChanged?.Invoke(this, true);
        }

        public void Disconnect()
        {
            if (!IsConnected) return;
            _reader.Release();
            ConnectionChanged?.Invoke(this, false);
        }
    }
}
