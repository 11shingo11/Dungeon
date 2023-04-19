using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player player;
    public FloatingTextManager floatingTextManager;

    //Logic
    public int pesos;
    public int experience;


    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }
    

    //Save state
    /*
     * INT preferedSkin
     * INT pesos
     * INT xp
     * INT weaponLevel
     */
    public void SaveState()
    {
        Debug.Log("Save");
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    { 
        Debug.Log("Load");
    }
}

