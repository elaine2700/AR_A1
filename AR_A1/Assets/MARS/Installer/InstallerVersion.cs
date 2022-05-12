using UnityEngine;
using UnityEditor;

namespace Unity.MARS.Installer
{
    [CreateAssetMenu(fileName = "InstallerVersion", menuName = "MARS/InstallerVersion")]
    public class InstallerVersion : ScriptableObject, ISerializationCallbackReceiver
    {
        static InstallerVersion s_Instance;

        [SerializeField]
        string m_GitCommit;

        public string GitCommit
        {
            get => m_GitCommit;
            set => m_GitCommit = value;
        }

        public static InstallerVersion Instance
        {
            get
            {
                if (!s_Instance)
                {
                    AssetDatabase.Refresh();
                    var assets = AssetDatabase.FindAssets("InstallerVersion");
                    if (assets.Length > 0)
                    {
                        var path = AssetDatabase.GUIDToAssetPath(assets[0]);
                        s_Instance = (InstallerVersion)AssetDatabase.LoadAssetAtPath(path, typeof(InstallerVersion));
                    }
                }

                return s_Instance;
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            Debug.Log($"MARS auto installer {m_GitCommit}");
        }
    }
}
