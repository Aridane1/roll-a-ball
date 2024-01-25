using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


public class NewBehaviourScript : MonoBehaviour
{
    public bool changeLevel;
    public int indexLevel;

    void Start()
    {
        
    }

    void Update()
    {
        if(changeLevel)
        {
            ChangeLevel(indexLevel);
        }
    }

    public void ChangeLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
}
