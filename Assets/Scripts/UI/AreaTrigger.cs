using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Single.System.SceneManager.LoadScene(SceneDataType.Start);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Single.System.SceneManager.UnLoadScene();
        }
    }
}
