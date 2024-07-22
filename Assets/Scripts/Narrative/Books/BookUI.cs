using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookUI : MonoBehaviour
{
    [Header("Images")] [SerializeField] private Image bookImage;
    [SerializeField] private Image backGround;

    [Header("Text fields")] [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI pageText;
    public static BookUI instance { get; private set; }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        instance = this;
    }

    public void StartReading(Sprite bookSprite, BookScriptable bookData)
    {
        backGround.color = new Color( backGround.color.r, backGround.color.g, backGround.color.b,0.8f);
        bookImage.sprite = bookSprite;
    }
    
    
}
