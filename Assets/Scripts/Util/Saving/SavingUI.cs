using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SavingUI : MonoBehaviour
{
    [SerializeField] private string SaveName;
    [SerializeField] private TextMeshProUGUI SceneName;
    [SerializeField] private TextMeshProUGUI SaveNameUI;
    [SerializeField] private Image SaveFileImage;
    
    private string ImagePath;
    private void Start()
    {
        if (SaveNameUI)
        {
            SaveNameUI.text = SaveName;
        }

        string save = "";
        string path = Application.persistentDataPath + "/" + SaveName + ".json";
        if (System.IO.File.Exists(path))
        {
            save = System.IO.File.ReadAllText(path);
        }

        if (SceneName && save != "")
        {
            SceneName.text = JsonUtility.FromJson<Save>(save).Location;
        }

        if (SaveNameUI)
        {
            ImagePath = SceneName.text + "_" + SaveNameUI.text + ".png";
            LoadImage();
        }
    }

    public void SaveGame()
    {
        SaveSystem.Instance.CurrentSave.SaveName = SaveName;
        SaveSystem.Instance.SaveGame();
        if (SceneName)
        {
            SceneName.text = SaveSystem.Instance.CurrentSave.Location;
        }
        
        
        //save temp texture or later use and then take a screenshot
        transform.parent.parent.GetComponent<RectTransform>() .anchoredPosition  = new Vector3(5000,  -197f, 0);
    
        Invoke("TakeScreenShot",0.2f);
       
    }


    public void LoadGame()
    {
        SaveSystem.Instance.CurrentSave.SaveName = SaveName;
        SaveSystem.Instance.LoadGame();
    }

    private void ResetUI()
    {
        DialogueManager.Instance.ContinueUnfinihedDialogue();
        
        RectTransform parent = transform.parent.parent.GetComponent<RectTransform>();

        LoadImage();

        parent.anchoredPosition = new Vector3(0, -197f, 0);
        Camera.main.targetTexture = null;
        parent.gameObject.SetActive(false);
    }

    private void TakeScreenShot() {
        Camera.main.targetTexture = RenderTexture.GetTemporary(500, 500);
        RenderTexture texture = Camera.main.targetTexture;
        
        Texture2D result = new Texture2D(texture.width, texture.height, TextureFormat.ARGB32 , false);
        Rect rect = new Rect(0, 0, texture.width, texture.height);
        result.ReadPixels(rect,0,0);
        byte[] byteArray = result.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.persistentDataPath+"/"+ImagePath, byteArray);
                    
        print("saved screenshot");

        
        Invoke("ResetUI" , 0.1f);
        
   }

    private void LoadImage()
    {
        if (File.Exists(Application.persistentDataPath + "/" + ImagePath))
        {
            byte[] bytes = File.ReadAllBytes(Application.persistentDataPath + "/" + ImagePath);
            Texture2D texture  = new Texture2D(500, 500, TextureFormat.RGB24, false);
            texture.LoadImage(bytes);
            SaveFileImage.sprite = Sprite.Create(texture,new Rect(0, 0, texture.width, texture.height), Vector2.zero, 100f);
            print("TRIED TO LOAD IMAGE");
        }
    }
}