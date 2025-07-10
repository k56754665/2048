using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int Attack { get; private set; }
    public int Hp { get; private set; }

    [SerializeField] TMP_Text _attackText;
    [SerializeField] TMP_Text _hpText;

    public void Init(int attack, int hp, bool isEnemy)
    {
        Attack = attack;
        Hp = hp;
        UpdateText();
    }

    public void UpdateText()
    {
        _attackText.text = Attack.ToString();
        _hpText.text = Hp.ToString();
    }
}
