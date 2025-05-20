using UnityEngine;

public class AttributeTag : MonoBehaviour
{
    public string _attributeName;
    [HideInInspector] public Vector3 originalPosition;
    
    public string AttributeName => _attributeName;
    void Awake()
    {
        _attributeName = CleanName(gameObject.name);
        originalPosition = this.gameObject.transform.position;
    }

    void OnEnable()
    {
        _attributeName = CleanName(gameObject.name);
    }
    private string CleanName(string name)
    {
        return name.Replace("(Clone)", "").Trim();
    }
}
