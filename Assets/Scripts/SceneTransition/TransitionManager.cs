using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{

    public Image Img;

    [SerializeField] int nextScene , lastScene;
    public int Key = -1;
    private Transform player;
    
    
    public static TransitionManager instance { get; private set; }
    
    
    
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        instance = this;
        
        DontDestroyOnLoad(this);
        
    }

    
    
   
    private void ChangedActiveScene(Scene current, Scene next)
    {

        print("Last was : " + current.name + " Loaded : " + next.name);

    }


    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1.3f; i >= -0.1f; i -= Time.deltaTime)
            {
                // set color with i as alpha
                Img.color = new Color(0, 0, 0, i);

                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 2f; i += Time.deltaTime)
            {
                // set color with i as alpha
                Img.color = new Color(0, 0, 0, i);

                if (i >= 1f )
                {
                    Img.color = new Color(0, 0, 0, 1);
                    SceneManager.LoadSceneAsync(nextScene); 
                    StartCoroutine(  FadeImage(true));
                    break;
                }
                yield return null;
               
                
                
            }
        }
            
    }

    public void BeginTransition(int newSceneIndex , int lastSceneIndex)
    {
        EnemySpawner enemies = EnemySpawner.Instance;

        if (enemies)
        {
            enemies.SavePositions();
        }
        
      StartCoroutine(  FadeImage(false));
        nextScene = newSceneIndex;
        lastScene = lastSceneIndex;
    }

    public void SelectTransitionPoint(Transform transform)
    {     
        player = FindObjectOfType<Player>().transform;
        player.position = new Vector3(transform.position.x,player.position.y, transform.position.z);
        player.rotation = transform.rotation;
    }
}
