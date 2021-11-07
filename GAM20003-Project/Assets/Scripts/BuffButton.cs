using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuffButton : MonoBehaviour
{
    [SerializeField] private Buff[] buffList;
    [SerializeField] private TextMeshPro title;
    [SerializeField] private TextMeshPro description;

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

    public void BuffSelected(Player player) {
        GameManager gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
        if (gameManager.SelectedBuff(buff, player))
            gameObject.SetActive(false);
    }
}
