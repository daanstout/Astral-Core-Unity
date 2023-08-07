using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Astral.Core {
    public class StateMachine : MonoBehaviour {
        [SerializeField] private ScriptableObject rootLayer;

        private void Start() {
            DontDestroyOnLoad(gameObject);
        }
    }
}