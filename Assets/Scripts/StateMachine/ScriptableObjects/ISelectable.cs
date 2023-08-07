using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astral.Core
{
    public interface ISelectable
    {
        string Type { get; }

        string SubType { get; }
    }
}
