using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RestartScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
            PerformRestart();
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    public void PerformRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
