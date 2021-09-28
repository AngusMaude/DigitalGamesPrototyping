using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int maxPlayers;
    public List<PlayerController> activePlayers = new List<PlayerController>();

    public GameObject spawnEffect;
    private void Awake()
    {
        if(instance == null)
        {

       
        instance = this;
        DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddPlayer(PlayerController newPlayer)
    {
        if(activePlayers.Count < maxPlayers)
        {
            activePlayers.Add(newPlayer);
            Instantiate(spawnEffect, newPlayer.transform.position, newPlayer.transform.rotation);
        }
        else 
        {
            Destroy(newPlayer.gameObject);
        }
    }
}
