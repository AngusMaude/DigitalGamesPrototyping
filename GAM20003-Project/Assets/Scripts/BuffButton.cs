using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffButton : MonoBehaviour
{
    [SerializeField] private Buff[] buffList;
    private Buff buff;
    // Start is called before the first frame update
    void Start()
    {
        SelectBuff();
    }

    private void SelectBuff() {
        buff = buffList[Random.Range(0, buffList.Length)];
    }

    private void ButtonPress() {
        GameManager gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
        gameManager.SelectedBuff(buff);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
