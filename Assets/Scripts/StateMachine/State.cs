using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Core {
    public abstract class State {
        public virtual void Start() { }

        public virtual void Update() { }

        public virtual void Stop() { }
    }
}