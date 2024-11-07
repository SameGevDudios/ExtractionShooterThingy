using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionLoadLevel : MonoBehaviour
{
    [SerializeField] private int _loadSceneIndex;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            SceneManager.LoadScene(_loadSceneIndex);
    }
}
