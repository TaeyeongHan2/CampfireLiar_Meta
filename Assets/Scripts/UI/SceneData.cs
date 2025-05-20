using System;
using UnityEngine;

public enum SceneDataType
{
    Start,
    Loading,
    Catagory,
    Warning,
    Game,
    GameResult,
    PostError,
    Duplicate,
    NoPrefab,
    
}
namespace Single
{
    [Serializable]
    public class SceneData
    {
        public SceneDataType SceneDataType;
        public GameObject SceneObject;
    }
}