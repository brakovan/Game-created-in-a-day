using UnityEngine;
using System.Collections.Generic;

public class FoundObjectsManager : MonoBehaviour
{
    private static FoundObjectsManager instance;
    public static FoundObjectsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("FoundObjectsManager").AddComponent<FoundObjectsManager>();
            }
            return instance;
        }
    }

    private HashSet<Transform> foundObjects = new HashSet<Transform>();

    public void AddFoundObject(Transform obj)
    {
        foundObjects.Add(obj);
    }

    public bool IsObjectFound(Transform obj)
    {
        return foundObjects.Contains(obj);
    }

    public void RemoveFoundObject(Transform obj)
    {
        foundObjects.Remove(obj);
    }
}
