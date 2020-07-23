using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;
using UnityEngine.SceneManagement;

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
        path = Application.persistentDataPath + "/PlayerSave.Json";
    }
    #endregion

    // If true, will use the stats in save load, else will use stats from saved data on computer
    public bool useTheseStats = true;

    // if not first stage, will use data from json, thus, after respawning 
    // from death, or following scenes,
    // will have correct data as they will use stats carried over from previous scene
    bool firstStage = true;


    // Whether most updated data was loaded into the save load script and is set up in the variables, used in playerstats to know
    // if it can set up the data from the data here.
    public bool dataLoaded = false;

    public string Name;
    public int HP;
    //public int Armor;
    //public int Damage;
    public int Gold;
    public int Mana;
    public int SkillPoints;

    string path;
    
    public void Save()
    {
        GetData();
        SetData();
        // Assumes skillPoints is correct for next scene;
        dataLoaded = true;
    }

    public void SaveStatsBeforeAddingSkillPoints() {
        GetData();
        SetData();
        // false as skillpoints havent add 2, will be set to true in Load, after loading updated stats
        dataLoaded = false;
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
        if (firstStage && !useTheseStats) {
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
            
            Debug.Log("Load " + SceneManager.GetActiveScene().name + " " + playerJson.ToString());
            useTheseStats = true;
            dataLoaded = true;
            firstStage = false;
        } 
        else if (firstStage && useTheseStats)
        {
            GoldCounter.instance.SetGold(Gold);
            PlayerManager.instance.player.GetComponent<PlayerStats>().SetHealth(HP);
            PlayerManager.instance.player.GetComponent<PlayerStats>().SetMana(Mana);
            PlayerManager.instance.player.GetComponent<PlayerStats>().SetSkillPoints(SkillPoints);
            firstStage = false;
            dataLoaded = true;
            Debug.Log("load first stage and useTheseStats called");
        } 
        else if (!firstStage)
        {
            string jsonString = File.ReadAllText(path);
            JSONObject playerJson = (JSONObject)JSON.Parse(jsonString);
            Debug.Log("Load " + SceneManager.GetActiveScene().name + " " + playerJson.ToString());
            Debug.Log("add 2 to skill points");
            HP = playerJson["HP"];
            //Armor = playerJson["Armor"];
            //Damage = playerJson["Damage"];
            Gold = playerJson["Gold"];
            Mana = playerJson["Mana"];
            // adds two to skillpoints as it is loading new scene
            SkillPoints = playerJson["SkillPoints"];

            if(ShouldIncreaseSkillPoints()) {
                SkillPoints += 1;
            }

            GoldCounter.instance.SetGold(Gold);
            
            //health, mana and skill points will be set in playerstats script
            PlayerManager.instance.player.GetComponent<PlayerStats>().SetHealth(HP);
            PlayerManager.instance.player.GetComponent<PlayerStats>().SetMana(Mana);
            PlayerManager.instance.player.GetComponent<PlayerStats>().SetSkillPoints(SkillPoints);
            dataLoaded = true;
            Debug.Log("load non first stage called");
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

        Debug.Log("Set data in JSON"+ SceneManager.GetActiveScene().name + " " + playerJson.ToString());

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

    public void SaveForNewScene() {
        SaveStatsBeforeAddingSkillPoints();
    }


    //Returns true if the skill points should increase by two
    bool ShouldIncreaseSkillPoints() {
        return !(SceneManager.GetActiveScene().name == "Shop-CH") &&
         !(SceneManager.GetActiveScene().name == "AfterDefeatingBoss"); 
    }
}
