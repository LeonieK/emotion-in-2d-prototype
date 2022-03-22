using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] private string loadScene;

    void OnTriggerEnter2D(Collider2D other) {
            SceneManager.LoadScene(loadScene);
    }

}
