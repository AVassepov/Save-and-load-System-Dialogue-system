using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookUI : MonoBehaviour
{
    [Header("Images")] [SerializeField] private Image bookImage;
    [SerializeField] private Image backGround;

    [SerializeField] private SoundLibrary Library;

    [Header("Text fields")] [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField] private TextMeshProUGUI pageText;
    [SerializeField] private TextMeshProUGUI titleText;

    [SerializeField] private AudioSource pageFlip;
    public static BookUI instance { get; private set; }

    private BookScriptable data;
    private int currentPage;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        instance = this;
    }

    void Update()
    {
        if (data && (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) ||
                     Input.GetMouseButtonDown(0) ))
        {
            ContinueReading();
        }
        else if (data && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) ||
                          Input.GetMouseButtonDown(1)))
        {
            GoBack();
        }
    }

    public void StartReading(Sprite bookSprite, BookScriptable bookData)
    {
        bookImage.enabled = true;
        data = bookData;
        backGround.color = new Color(backGround.color.r, backGround.color.g, backGround.color.b, 0.8f);
        bookImage.sprite = bookSprite;
        pageText.text = "0 / " + bookData.Pages.Count.ToString();
        text.text = data.Title;
        pageFlip.clip = AudioManager.GetRandomAudio(Library.Sounds);
        pageFlip.pitch = AudioManager.RandomPitch(new Vector2(0.8f, 1.1f));
        pageFlip.Play();

        Player.Instance.AllowMovement(false);
    }

    public void ContinueReading()
    {
        if (currentPage < data.Pages.Count)
        {
            pageText.text = (currentPage + 1).ToString() + " / " + data.Pages.Count.ToString();
            text.text = data.Pages[currentPage];
            currentPage++;
            pageFlip.clip = AudioManager.GetRandomAudio(Library.Sounds);
            pageFlip.pitch = AudioManager.RandomPitch(new Vector2(0.8f, 1.1f));
            pageFlip.Play();
        }
        else
        {
            StopReading();
            currentPage = 0;
            print("Ended");
        }
    }

    public void GoBack()
    {
        currentPage--;
        if (currentPage < data.Pages.Count && currentPage >=0)
        {
            pageText.text = (currentPage ).ToString() + " / " + data.Pages.Count.ToString();
            if(currentPage>0){
            text.text = data.Pages[currentPage-1];
            }
            else
            {
                text.text = data.Title;
            }
            pageFlip.clip = AudioManager.GetRandomAudio(Library.Sounds);
            pageFlip.pitch = AudioManager.RandomPitch(new Vector2(0.8f, 1.1f));
            pageFlip.Play();
        }
        else
        {
            StopReading();
            currentPage = 0;
            print("Ended");
        }
    }
        
    
    
    public void StopReading()
    {
        backGround.color = new Color(backGround.color.r, backGround.color.g, backGround.color.b, 0f);
        bookImage.enabled = false;
        data = null;
        pageText.text = "";
        text.text = "";
        Player.Instance.AllowMovement(true);
    }
}