using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astral.Core
{
    public class TestAction : StateAction
    {
        private class TestClass {
            [SerializeField] private readonly bool testBool;
            [SerializeField] private readonly string testString;
            [SerializeField] private readonly int[] testArray;
        }

        [SerializeField] private readonly bool testBool;
        [SerializeField] private readonly string testString;
        [SerializeField] private readonly int[] testArray;
        [SerializeField] private readonly TestClass testClass;
    }
}
