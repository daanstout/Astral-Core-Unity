using System.Collections;
using System.Collections.Generic;

using UnityEditor.Build;
using UnityEditor.Build.Reporting;

using UnityEngine;

namespace Core.Editor
{
    public class PrebuildAssetCollector : IPreprocessBuildWithReport, IPostprocessBuildWithReport {
        public int callbackOrder => 1;

        public void OnPostprocessBuild(BuildReport report) {

        }

        public void OnPreprocessBuild(BuildReport report) {

        }
    }
}
