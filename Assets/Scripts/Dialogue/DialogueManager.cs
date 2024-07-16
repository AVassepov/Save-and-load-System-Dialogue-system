using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Dialogue OpeningDialogue;

    public Dialogue NextDialogue;

    private string currentPage;
    bool canSkip;

    [SerializeField] private Canvas Canvas;
    [SerializeField] private TextMeshProUGUI TextField;
    [SerializeField] private TextMeshProUGUI SpeakerName;


    [SerializeField] private Image NotificationBackground;
    [SerializeField] private TextMeshProUGUI NotificationText;

    public ResponceOption CurrentOption;
    [SerializeField] List<ResponceOption> ResponceOptions;

    private int charCounter;
    [HideInInspector]public bool printing;
    private void Awake()
    {
        Canvas.enabled = false;
    }

    private void Update()
    {
       if(printing&& Time.frameCount % 20 == 0)
       {
            ScrollText();
            
       }

       if (Input.GetKeyDown(KeyCode.Space) && canSkip)
       {
           if (charCounter==0)
           {
               ContinueDialogue();
           }
       }
       
       
        if (Input.GetMouseButtonDown(0) && CurrentOption)
        {
            SelectOption();
            CurrentOption = null;
        }
    }

    public void InitiateDialogue()
    {
        // prevent walking
        GetComponent<Player>().Speed = 0;

        //Show UI 
        Canvas.enabled = true;
        if (OpeningDialogue) { 
        NextDialogue = OpeningDialogue;
        }
        charCounter = 0;
        printing = false;


        //SpekerName
        SpeakerName.text = NextDialogue.Speaker;



        // Show first text and set up index
        currentPage = NextDialogue.Text;


        //Later remove this and start the text scroll instead;
        //TextField.text = currentPage;
        TextField.text = "";
        printing = true;
    
    }

    public void ContinueDialogue()
    {

        //Next dialogue if no responce options
        if (NextDialogue.Responces.Count == 0)
        {
            NextDialogue = NextDialogue.NextDialogue;
            currentPage = NextDialogue.Text;
        }
        else
        {
            // show response options
            ShowResponces();
            currentPage = "";
        }

        //SpeakerName
        SpeakerName.text = NextDialogue.Speaker;


        //Show next text
        TextField.text = "";
        printing = true;
    }

    public void FinishDialogue()
    {
        //walk again
        GetComponent<Player>().Speed = GetComponent<Player>().savedSpeed;


        //Close UI
        Canvas.enabled = false;

        // Reset Indexes
        currentPage = "";
        NextDialogue = null;
        OpeningDialogue = null;
    }


    private void ScrollText()
    {
        //Reveal letter by letter

        if(charCounter < currentPage.Length)
        {   
            TextField.text += currentPage[charCounter];
            charCounter++;
            canSkip = true;
        }
        else
        {
            charCounter = 0;
            printing = false;
            canSkip = false;
        }
    }

    public void SkipScroll()
    {
        TextField.text = currentPage;

        charCounter = 0;
        printing = false;
    }

    public void ShowResponces()
    {
        for (int i = 0; i < NextDialogue.Responces.Count; i++)
        {
            ResponceOptions[i].Data = NextDialogue.Responces[i];
            ResponceOptions[i].gameObject.SetActive(true) ;
            ResponceOptions[i].DialogueManager = this;
        }
    }

    public void ClearResponces()
    {
        for (int i = 0; i < ResponceOptions.Count; i++)
        {
            ResponceOptions[i].gameObject.SetActive(false);
        }
    }

    private void SelectOption()
    {
        OpeningDialogue = null ;
        OpeningDialogue = CurrentOption.Data.NextDialogue;

        if (OpeningDialogue != null)
        {
            InitiateDialogue();
        }else
        {
            FinishDialogue();
        }

        ClearResponces();
    }

    public void Notification(string Text , float TimeShown)
    {
        NotificationText.text = Text;
        StartCoroutine(FadeTextToFullAlpha(1f, NotificationText));
        Invoke("StopNotification", TimeShown) ;
    }

    private void StopNotification()
    {
        StartCoroutine(FadeTextToZeroAlpha(1f, NotificationText));
    }




    public IEnumerator FadeTextToFullAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        NotificationBackground.color = new Color(NotificationBackground.color.r, NotificationBackground.color.g, NotificationBackground.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            NotificationBackground.color = new Color(NotificationBackground.color.r, NotificationBackground.color.g, NotificationBackground.color.b, NotificationBackground.color.a + (Time.deltaTime / t)/2);
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);

        NotificationBackground.color = new Color(NotificationBackground.color.r, NotificationBackground.color.g, NotificationBackground.color.b, NotificationBackground.color.a/2);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            NotificationBackground.color = new Color(NotificationBackground.color.r, NotificationBackground.color.g, NotificationBackground.color.b, NotificationBackground.color.a  - (Time.deltaTime / t));
            yield return null;
        }
    }
}
