using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class buffTransition : MonoBehaviour { 

    public float BuffChosen;

    public void OnMouseDown()
    {
        gameObject.GetComponent<HealthBuff>().ApplyBuff();
        SceneManager.LoadScene("Map2");
    }

}
