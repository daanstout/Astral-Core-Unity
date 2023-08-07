using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astral.Core
{
    public interface IServiceLocator {
        object Get(Type type);
        T Get<T>() where T : class;
        void Inject(object obj);
    }
}
