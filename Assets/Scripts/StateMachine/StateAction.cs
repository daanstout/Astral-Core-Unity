using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Astral.Core {
    public abstract class StateAction {
        public virtual void Start() { }

        public virtual void Update() { }

        public virtual void Stop() { }
    }
}