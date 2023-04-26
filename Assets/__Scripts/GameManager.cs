using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        PlayerPrefs.DeleteKey("SaveState");
    }

    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player player;
    public weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitPointBar;
    public GameObject hud;
    public GameObject menu;
    public Animator deathMenuAnim;
    //Logic
    public int pesos;
    public int experience;
    


    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }
    
    //Upgrade Weapon
    public bool TryUpgradeWeapon()
    {
        //is the weapon level max?
        if (weaponPrices.Count <= weapon.weaponLevel)
            return false;

        if (pesos >= weaponPrices[weapon.weaponLevel])
        {
            pesos -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }
        return false;
    }

    //Hitpointbar
    public void OnHitPointChange()
    {
        float ratio = (float)player.hitpoint / (float)player.maxHitpoint;
        hitPointBar.localScale = new Vector3(1, ratio, 1);

    }

    //Exp system
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;
        while (experience >= add)
        {
            add += xpTable[r];
            r++;
            if (r == xpTable.Count)//Max level
                return r;
        }
        
        return r;
    }

    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;
        while(r< level)
        {
            xp += xpTable[r];
            r++;
        }
        return xp;
    }

    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        experience += xp;
        if (currLevel < GetCurrentLevel())
            OnLevelUp();    
    }
    public void OnLevelUp()
    {
        Debug.Log("LVL UP!");
        player.OnLevelUp();
        GameManager.instance.OnHitPointChange();
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
        string s = "";

        s += 0 + "|";
        s+= pesos.ToString() + "|";
        s+= experience.ToString()+"|";
        s += weapon.weaponLevel.ToString() + "|";
        s += player.hitpoint.ToString();
        Debug.Log("Save");
        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        //SceneManager.sceneLoaded -= LoadState;
        if (!PlayerPrefs.HasKey("SaveState"))
            return;
        
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        pesos = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        if (GetCurrentLevel()!= 1)
            player.SetLevel(GetCurrentLevel());
        player.hitpoint = int.Parse(data[4]);
        GameManager.instance.OnHitPointChange();
        weapon.SetWeaponLevel(int.Parse(data[3]));
        if (SceneManager.GetActiveScene().name == "Dungeon_1")
            player.transform.position = new Vector3((-0.63f),0,0);
        if (SceneManager.GetActiveScene().name == "Main")
            player.transform.position = new Vector3((-0.13f), 0, 0);
        Debug.Log("Load");

    }   

    //Death menu and restart
    public void Restart()
    {
        deathMenuAnim.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        player.Respawn();

    }
}

