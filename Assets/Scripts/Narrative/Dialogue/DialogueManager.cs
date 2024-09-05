using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public bool WaitForDialogue;

    public Dialogue OpeningDialogue;
    public GameObject SavingUI; 
    public Dialogue NextDialogue;

    private string currentPage;
    bool canSkip;

    private bool isBusy;

    [SerializeField] private Canvas Canvas;
    [SerializeField] private TextMeshProUGUI TextField;
    [SerializeField] private TextMeshProUGUI SpeakerName;


    [SerializeField] private Image NotificationBackground;
    [SerializeField] private TextMeshProUGUI NotificationText;

    public ResponceOption CurrentOption;
    [SerializeField] List<ResponceOption> ResponceOptions;

    private int charCounter;
    [HideInInspector]public bool printing;
    [HideInInspector] public AgrippaCross CurrentCross;
    
    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        Instance = this;

        Canvas.enabled = false;
    }

    private void Update()
    {
       if(printing&& Time.frameCount % 10 == 0)
       {
            ScrollText();
            
       }

       if (Input.GetButtonDown("Jump"))
       {
           if (charCounter == 0 && !printing)
           {
               if (NextDialogue != null)
               {
                   ContinueDialogue();
               }
               else
               {
                   FinishDialogue();
               }
           }
           else
           {
                SkipScroll();
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
        if (!isBusy)
        {
            // prevent walking
            Player.Instance.Speed = 0;


            //Show UI 
            Canvas.enabled = true;
            if (OpeningDialogue)
            {
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
            isBusy = true;
        }
    }

    public void ContinueDialogue()
    {

        //Next dialogue if no responce options
        if (NextDialogue && NextDialogue.Responces.Count == 0)
        {
            NextDialogue = NextDialogue.NextDialogue;
            if(NextDialogue){
            currentPage = NextDialogue.Text;
            //SpeakerName
            SpeakerName.text = NextDialogue.Speaker;
            }
        }
        else
        {
            // show response options
            ShowResponces();
            currentPage = "";
        }



        //Show next text
        TextField.text = "";
        printing = true;
    }

    public void FinishDialogue()
    {
        //walk again
       Player.Instance.Speed =  Player.Instance.savedSpeed;

        //Close UI
        Canvas.enabled = false;


        if (!WaitForDialogue)
        {
            // Reset Indexes
            currentPage = "";
            NextDialogue = null;
            OpeningDialogue = null;
            isBusy = false;
            CurrentCross = null;
        }
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
        if (NextDialogue.Responces.Count > 0)
        {
            for (int i = 0; i < NextDialogue.Responces.Count; i++)
            {
                ResponceOptions[i].Data = NextDialogue.Responces[i];
                ResponceOptions[i].gameObject.SetActive(true);
                ResponceOptions[i].DialogueManager = this;
            }
        }
        else
        {
            FinishDialogue();
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

        Responces responce = CurrentOption.Data;
        if (responce.Outcome != Responces.ResponceOutcome.Exit || responce.Outcome != Responces.ResponceOutcome.Exit ||
            responce.Outcome != Responces.ResponceOutcome.None)
        {
            OpeningDialogue = DialogueSpecials(responce);
        }
        
        
        
        if (OpeningDialogue != null && !WaitForDialogue)
        {
            isBusy = false;
            InitiateDialogue();
            print("Continued as normal");
        }
        else 
        {
            FinishDialogue();
            print("that was the end");
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

    private void Skipbool()
    {
        if (canSkip)
        {
            canSkip = false;
        }
        else{
            canSkip = true;
        }
        
    }

    public Dialogue DialogueSpecials(Responces checkThis)
    {
        if (checkThis.Outcome == Responces.ResponceOutcome.CrossSave)
        {
            return CurrentCross.CheckSaveStatus();
        }


        return null;
    }

    public void ContinueUnfinihedDialogue()
    {
        WaitForDialogue = false;
        isBusy = false;
        InitiateDialogue();
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
