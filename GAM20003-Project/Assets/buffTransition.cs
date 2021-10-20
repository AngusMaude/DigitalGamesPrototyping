using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class buffTransition : MonoBehaviour { 

    public float BuffChosen;

    public void OnMouseDown()
    {
<<<<<<< Updated upstream
        gameObject.GetComponent<HealthBuff>().ApplyBuff();
=======
        gameObject.GetComponent<BuffHandler>ApplyBuff();
>>>>>>> Stashed changes
        SceneManager.LoadScene("Map2");
    }

}
