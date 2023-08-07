using System;

namespace Astral.Core {
    [Flags]
    public enum TypeFilter : uint{ 
        None = 0x00,
        Instantiatable = 0x01,
        Abstract= 0x02,
        Interface = 0x04,

        All = uint.MaxValue
    }
}
