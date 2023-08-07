using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astral.Core
{
    public class TestRule : Rule
    {
        public override bool IsTrue => true;

        [SerializeField] private readonly bool testBool;
        [SerializeField] private readonly string testString;
        [SerializeField] private readonly int[] testArray;
    }
}
