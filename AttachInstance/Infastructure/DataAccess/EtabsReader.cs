using ETABSv1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttachInstance.Infastructure.DataAccess
{
    public sealed class EtabsReader
    {
        public cOAPI? EtabsObject { get; private set; }
        public cSapModel? SapModel { get; private set; }

        public bool IsInitialized => SapModel != null;

        public void Initialize(cOAPI etabsObject)
        {
            EtabsObject = etabsObject ?? throw new ArgumentNullException(nameof(etabsObject));
            SapModel = EtabsObject.SapModel ?? throw new InvalidOperationException("ETABS SapModel is null.");
        }

        public void Release()
        {
            EtabsObject = null;
            SapModel = null;
        }

        public cSapModel RequireModel()
            => SapModel ?? throw new InvalidOperationException("SapModel is not initialized. Call Connect() first.");
    }
}
