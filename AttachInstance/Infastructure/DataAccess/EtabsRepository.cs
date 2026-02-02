using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace AttachInstance.Infastructure.DataAccess
{
    public sealed class EtabsRepository
    {
        private readonly EtabsReader _reader;
        private readonly EtabsStore _store = new EtabsStore();

        public EtabsRepository(EtabsReader reader)
        {
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }
        public int GetStoryCount()
        {
            var model = _reader.RequireModel();
            int errorCode = model.Story.GetStories(ref _store.NumberStories, ref _store.StoryNames, 
                ref _store.StoryElevations, ref _store.StoryHeights, 
                ref _store.IsMasterStory, ref _store.SimilarToStory, 
                ref _store.SpliceAbove, ref _store.SpliceHeight);

            return _store.NumberStories;
        }
    }
}
