using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class SaveLoad : MonoBehaviour
{
    #region Singleton
    public static SaveLoad instance;
    void Awake() {
        if (instance != null) {
            Debug.Log("more than one instance of SaveLoad found!");
            return;
        }

        instance = this;
    }
    #endregion

    // If true, will use the stats in save load, else will use stats from saved data on computer
    public bool useTheseStats = true;

    public string Name;
    public int HP;
    //public int Armor;
    //public int Damage;
    public int Gold;
    public int Mana;
    public int SkillPoints;
    
    public void Save()
    {
        GetData();
        JSONObject playerJson = new JSONObject();
        playerJson.Add("HP", HP);
        //playerJson.Add("Armor", Armor);
        //playerJson.Add("Damage", Damage);
        playerJson.Add("Gold", Gold);
        playerJson.Add("Mana", Mana);
        playerJson.Add("SkillPoints", SkillPoints);

        Debug.Log(playerJson.ToString());

        string path = Application.persistentDataPath + "/PlayerSave.Json";
        File.WriteAllText(path, playerJson.ToString());
    }


    void GetData()
    {
        if (PlayerManager.instance.player != null && PlayerManager.instance.player.GetComponent<PlayerStats>().enabled) {
            HP = PlayerManager.instance.player.GetComponent<PlayerStats>().currentHealth;
            //Armor = PlayerManager.instance.player.GetComponent<PlayerStats>().armor.GetValue();
            //Damage = PlayerManager.instance.player.GetComponent<PlayerStats>().damage.GetValue();
            Gold = GoldCounter.instance.gold;
            Mana = PlayerManager.instance.player.GetComponent<PlayerStats>().CurrentMana;
            SkillPoints = PlayerManager.instance.player.GetComponent<PlayerStats>().SkillPoints;
        }
    }


    public void Load()
    {
        if (!useTheseStats) {
            string path = Application.persistentDataPath + "/PlayerSave.Json";
            string jsonString = File.ReadAllText(path);
            JSONObject playerJson = (JSONObject)JSON.Parse(jsonString);
            HP = playerJson["HP"];
            //Armor = playerJson["Armor"];
            //Damage = playerJson["Damage"];
            Gold = playerJson["Gold"];
            Mana = playerJson["Mana"];
            SkillPoints = playerJson["SkillPoints"];
            GoldCounter.instance.SetGold(Gold);
            
            //health, mana and skill points will be set in playerstats script
            PlayerManager.instance.player.GetComponent<PlayerStats>().SetHealth(HP);
            PlayerManager.instance.player.GetComponent<PlayerStats>().SetMana(Mana);
            PlayerManager.instance.player.GetComponent<PlayerStats>().SetSkillPoints(SkillPoints);
            
            Debug.Log(playerJson.ToString());
        } else 
        {
            GoldCounter.instance.SetGold(Gold);
            PlayerManager.instance.player.GetComponent<PlayerStats>().SetHealth(HP);
            PlayerManager.instance.player.GetComponent<PlayerStats>().SetMana(Mana);
            // -2 skill points as in player stats start method, skill points will increase by 2
            PlayerManager.instance.player.GetComponent<PlayerStats>().IncreaseSkillPoints(SkillPoints - 2);
            useTheseStats = false;
        }
        
    }


    // can specify stats in saveload inspector, then it will be saved. 
    // The data can be loaded by calling Load()
    void SetData() {
        JSONObject playerJson = new JSONObject();
        playerJson.Add("HP", HP);
        //playerJson.Add("Armor", Armor);
        //playerJson.Add("Damage", Damage);
        playerJson.Add("Gold", Gold);
        playerJson.Add("Mana", Mana);
        playerJson.Add("SkillPoints", SkillPoints);

        Debug.Log(playerJson.ToString());

        string path = Application.persistentDataPath + "/PlayerSave.Json";
        File.WriteAllText(path, playerJson.ToString());
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //GetData();
        if (Input.GetKeyDown(KeyCode.G)) Save();
        if (Input.GetKeyDown(KeyCode.L)) Load();
        if (Input.GetKeyDown(KeyCode.F)) SetData();
    }
}
