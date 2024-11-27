using UnityEditor;
using UnityEngine;

namespace Guinea.Core.Debug.Editor
{
    public static class FindMissingScripts
{
    [MenuItem("Tools/FindMissingScripts")]
    private static void FindMissingScriptsInCurrentScene()
    {
        GameObject[] allObjects = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        int missingScriptCount = 0;

        foreach (GameObject go in allObjects)
        {
            Component[] components = go.GetComponents<Component>();

            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    missingScriptCount++;
                    string path = go.name;
                    Transform t = go.transform;
                    while (t.parent != null)
                    {
                        path = t.parent.name + "/" + path;
                        t = t.parent;
                    }

                    UnityEngine.Debug.LogWarning("Missing script in GameObject: " + path, go);
                }
            }
        }

        UnityEngine.Debug.Log("Total missing scripts found: " + missingScriptCount);
    }
}

}