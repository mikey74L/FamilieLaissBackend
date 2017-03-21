using Breeze.ContextProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilieLaissBackend.Data.Interface
{
    public interface iBreezeValidator
    {
        bool BeforeSaveEntity(EntityInfo entityInfo);
        Dictionary<Type, List<EntityInfo>> BeforeSaveEntities(Dictionary<Type, List<EntityInfo>> saveMap);
        void AfterSaveEntities(Dictionary<Type, List<EntityInfo>> saveMap, List<KeyMapping> keyMappings);
    }
}
