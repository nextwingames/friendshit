using UnityEngine;
using UnityEngine.SceneManagement;

namespace Friendshit
{
    namespace Main
    {
        public class ButtonController : MonoBehaviour
        {
            public void OnClickPlayOnMainServer()
            {
                GameObject.Find("Select Server Panel").GetComponent<Animator>().Play("Destroy");
                SceneManager.LoadScene("Main Server Scene");
            }

            public void OnClickPlayOnLocalServer()
            {
                GameObject.Find("Select Server Panel").GetComponent<Animator>().Play("Destroy");
                GameObject.Find("Login Panel").SetActive(false);
                SceneManager.LoadScene("Local Server Scene");
            }
        }
    }
}
