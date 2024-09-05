using UnityEditor;
using UnityEngine;

public static class FindMissingScripts
{
    [MenuItem("Tools/FindMissingScripts")]
    private static void FindMissingScriptsInCurrentScene()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
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

                    Debug.LogWarning("Missing script in GameObject: " + path, go);
                }
            }
        }

        Debug.Log("Total missing scripts found: " + missingScriptCount);
    }
}
