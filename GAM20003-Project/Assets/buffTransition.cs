using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buffTransition : MonoBehaviour
{
    public void OnMouseDown()
    {
        SceneManager.LoadScene("Map2");
    }

}
