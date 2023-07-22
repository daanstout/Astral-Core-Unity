using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Core {
    public abstract class Rule {
        public abstract bool IsTrue { get; }
    }
}