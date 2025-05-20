using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace HTTP
{
    public abstract class Sample_Base :  MonoBehaviour
    {
        [SerializeField] protected Text Description;
        [SerializeField] protected Image Image;
        
        public void SendRequest()
        {
            Single.System.SceneManager.InGameCheck = true;
            Single.System.SceneManager.LoadScene(SceneDataType.Loading);
            StartCoroutine(RequestProcess());
            
        }

        protected abstract IEnumerator RequestProcess();
    }
}