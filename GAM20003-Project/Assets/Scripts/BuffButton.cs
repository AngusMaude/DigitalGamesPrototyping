using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuffButton : MonoBehaviour
{
    [SerializeField] private Buff[] buffList;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;

    private Buff buff;
    // Start is called before the first frame update
    void Start()
    {
        SelectBuff();
    }
    private void Update() {
        title.text = buff.GetTitle();
        description.text = buff.GetDescription()[0];
    }

    private void SelectBuff() {
        buff = Instantiate(buffList[Random.Range(0, buffList.Length)], transform);
    }

    public void ButtonPress() {
        GameManager gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
        gameManager.SelectedBuff(buff);
        gameObject.SetActive(false);
    }
}
