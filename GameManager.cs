using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
       public int coins;
    public int experience;
      public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;
    public RectTransform HPbar2;

    //Refrence
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public GameObject HUD;
    public GameObject menu;
    public Animator deathMenuAnim;

    private void Awake()
    {
        
        if(GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(HUD);
            Destroy(menu);
           
            return;
        }
            
          
        instance = this;
        player.Respawn();
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }

    //Keep track
  

    //variable logic to keep track
 

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    public bool TryUpgradeWeapon()
    {
        if (weaponPrices.Count <= weapon.weaponLevel)
            return false;

        if (coins >= weaponPrices[weapon.weaponLevel])
        {
            coins -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }
        return false;
    }

    public void OnHitpointChange()
    {
        float ratio = (float)player.hitpoints / (float)player.maxHitpoints;
        HPbar2.localScale = new Vector3(1, ratio, 1);
    }

    //Scene Loaded
    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        if(SceneManager.GetActiveScene().name != "Win")
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;
        while (experience >= add)
        {
            add += xpTable[r];
            r++;
            if( r == xpTable.Count)
            {
                return r;
            }
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
        player.OnLevelUp();
        OnHitpointChange();
    }
    /*
     * What is needed for savestate?:
     * preferred skins
     * coins
     * experience
     * weapon level
     */
    public void SaveState()
    {

        
        string saving = "";

        saving += "0" + "|";
        saving += coins.ToString() + "|";
        saving += experience.ToString() + "|";
        saving += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", saving);
    }
    public void LoadState(Scene s, LoadSceneMode mode)
    {

        SceneManager.sceneLoaded -= LoadState;
        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        //splits SaveState to differnet small strings within data

        //change skin

        //change coins
        coins = int.Parse(data[1]);
        //change experience
        experience = int.Parse(data[2]);
        if(GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());
        //change weapon level
       weapon.SetWeaponLevel(int.Parse(data[3]));

    }

    public void Respawn()
    {
        deathMenuAnim.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        player.Respawn();
        coins = 0;
        experience = 0;
        GameManager.instance.player.hitpoints=15;
        GameManager.instance.player.maxHitpoints=15;        
        weapon.SetWeaponLevel(0);
    }
    
}
