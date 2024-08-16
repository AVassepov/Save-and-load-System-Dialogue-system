using TMPro;
using UnityEngine;

public class DataUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Health;
    [SerializeField] private TextMeshProUGUI Hunger;
    [SerializeField] private TextMeshProUGUI Will;

    public CharacterData Character;


    public void OnEnable()
    {
        Character = GameStateManager.Instance.CurrentGameState.Characters[0];
        UpdateInfo();
    }


    public void UpdateInfo()
    {
        Health.text = "Health: " + Character.CharacterConditions.Health;
        Hunger.text = "Hunger: " + Character.CharacterConditions.Hunger;
        Will.text = "Will: " + Character.CharacterConditions.Will;

    }




}
