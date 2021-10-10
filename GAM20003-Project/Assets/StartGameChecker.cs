using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartGameChecker : MonoBehaviour
{
    public string leveltoLoad;

    private int playersInZone;

    public TMP_Text startCountdownText;
    public TMP_Text startExtraText;

    public float timetoStart =3f;
    private float startCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playersInZone >= 1 && playersInZone == GameManager.instance.activePlayers.Count)
        {
            startCountdownText.gameObject.SetActive(true);
            startExtraText.gameObject.SetActive(true);

            startCounter -= Time.deltaTime;

            startCountdownText.text = Mathf.CeilToInt(startCounter).ToString();

            if(startCounter <= 0)
            {
                SceneManager.LoadScene(leveltoLoad);
            }
        }
        else
        {
            startCountdownText.gameObject.SetActive(false);
            startExtraText.gameObject.SetActive(false);
            startCounter = timetoStart;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            playersInZone++;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playersInZone--;

        }
    }
}
