#if HAS_MARS && HAS_MARS_NAV_MESH && HAS_MARS_AR_FOUNDATION_PROVIDERS && HAS_AR_COMPANION_CORE && HAS_CONTENT_MANAGER && HAS_XR_MANAGEMENT && HAS_AR_FOUNDATION
#define HAS_ALL_PACKAGES
#endif

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.PackageManager.Requests;
using UnityEditor.PackageManager;

namespace Unity.MARS.Installer
{
    [InitializeOnLoad]
    public class AutoInstallMars
    {
        class InstallRequest
        {
            public string PackageName;
            public AddRequest Request;
        }

        static string k_AutoInstallDirectory = "Assets/MARS/Installer";
        static string[] s_PackagesToInstall =
        {
#if !HAS_MARS
            "com.unity.mars",
#endif

#if !HAS_CONTENT_MANAGER
            "com.unity.content-manager",
#endif

#if !HAS_MARS_NAV_MESH
            "com.unity.mars-nav-mesh",
#endif


#if !HAS_AR_COMPANION_CORE
            "com.unity.ar-companion-core",
#endif

#if !HAS_XR_MANAGEMENT
            "com.unity.xr.management",
#endif

#if !HAS_AR_FOUNDATION
            "com.unity.xr.arfoundation",
#endif

#if !HAS_MARS_AR_FOUNDATION_PROVIDERS
            "com.unity.mars-ar-foundation-providers",
#endif
        };

        static List<InstallRequest> s_Requests = new List<InstallRequest>();

        static AutoInstallMars()
        {
#if !DONT_RUN_INSTALLER
        EnsureInstalledMars();
#endif
        }

        static void AddMarsPackage()
        {
            foreach (var packageName in s_PackagesToInstall)
                AddPackage(packageName);

            if (s_Requests.Count == 0)
                return;

            Debug.Log("Adding MARS packages...");

            while (ProgressRequests())
            {
                System.Threading.Thread.Sleep(10);
            }

            RemoveInstaller();
        }

#if !HAS_ALL_PACKAGES
        [MenuItem("MARS/AutoInstall/Ensure MARS installed.")]
#endif
        static void EnsureInstalledMars()
        {
            if (DoesHaveMars())
            {
                RemoveInstaller();
                return;
            }

            AddMarsPackage();
        }

#if !HAS_ALL_PACKAGES
        [MenuItem("MARS/AutoInstall/Check MARS Auto Installed")]
#endif
        static bool DoesHaveMars()
        {
#if HAS_ALL_PACKAGES
        return true;
#else
            return false;
#endif
        }

        static void AddPackage(string packageName)
        {
            var request = new InstallRequest()
            {
                PackageName = packageName
            };
            s_Requests.Add(request);
        }

        static bool Progress(InstallRequest item)
        {
            if (item.Request == null)
            {
                item.Request = Client.Add(item.PackageName);
            }

            if (item.Request.IsCompleted)
            {
                Debug.Log($"Adding {item.PackageName} result: {item.Request.Status}");
                if (item.Request.Status == StatusCode.Success)
                {
                    Debug.Log("Installed: " + item.Request.Result.packageId);

                }
                else if (item.Request.Status >= StatusCode.Failure)
                    Debug.LogError(item.Request.Error.message);

                return false;
            }

            return true;
        }

        static bool ProgressRequests()
        {
            while (s_Requests.Count > 0)
            {
                if (Progress(s_Requests[0]))
                {
                    return true; // can only have 1 active at a time
                }

                s_Requests.RemoveAt(0);
                return true; // let a frame pass
            }

            return false;
        }

        static void RemoveInstaller()
        {
            EditorApplication.delayCall += DoRemoveInstaller;
        }

        static void DoRemoveInstaller()
        {
            if (EditorApplication.isCompiling)
                return;

            if (AssetDatabase.IsValidFolder(k_AutoInstallDirectory))
            {
                Debug.Log("Removing MARS auto installer...");
                FileUtil.DeleteFileOrDirectory(k_AutoInstallDirectory);
                FileUtil.DeleteFileOrDirectory(k_AutoInstallDirectory + ".meta");
            }

            EditorApplication.delayCall -= DoRemoveInstaller;
        }
    }
}
