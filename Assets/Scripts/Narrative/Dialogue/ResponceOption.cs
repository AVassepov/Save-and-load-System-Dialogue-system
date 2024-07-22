using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResponceOption : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler
{
    public Responces Data;
   [SerializeField] private TextMeshProUGUI TextField;
    public DialogueManager DialogueManager;

    public void OnEnable()
    {
        TextField.text = Data.Text;   
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("ENtered");
        DialogueManager.CurrentOption = this;
    }
    public void OnPointerExit(PointerEventData eventData) {


        print("Exited");

        if(DialogueManager.CurrentOption == this)
        {
            DialogueManager.CurrentOption = null;
        }


    }
}
