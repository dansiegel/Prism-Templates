#if IncludeMocks
//-:cnd:noEmit
#if !MOCK
//+:cnd:noEmit
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileApp.Services
{
    public class ItemTemplate : IItemTemplate
    {

    }
}
#if IncludeMocks
//-:cnd:noEmit
#endif
//+:cnd:noEmit
#endif