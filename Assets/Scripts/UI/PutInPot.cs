using System;
using System.Collections.Generic;
using UnityEngine;

public class PutInPot : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _prefabsKeyWord = new();

    private void OnTriggerEnter(Collider other)
    {
        if (Single.System.SceneManager.InGameCheck == true)
            return;
        if (GameData.PutCount >= 3)
        {
            Single.System.SceneManager.Warning(SceneDataType.Warning, "");
            return;
        }

        AttributeTag tag = other.GetComponent<AttributeTag>();
        foreach (string prefab in GameData.PutPrefab)
        {
            if (tag.AttributeName == prefab)
            {
                Single.System.SceneManager.Warning(SceneDataType.Duplicate, tag.AttributeName);
                return;
            }
        }
        if (tag == null)
            return;
        Vector3 spawnPos = tag.originalPosition;
        GameData.PutPrefab.Add(tag.AttributeName);
        Destroy(other);
        foreach (GameObject prefab in _prefabsKeyWord)
        {
            if (tag.AttributeName == prefab.name)
            {
                Instantiate(prefab, spawnPos, Quaternion.identity);
            }
        }
        GameData.PutCount++;
    }
}