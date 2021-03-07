using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeReactor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SceneLoader"))
        {
            other.GetComponent<SceneLoadDispetcher>().LoadNextScene();
            Destroy(other);
        }
    }
}
