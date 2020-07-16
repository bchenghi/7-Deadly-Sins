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

    public string Name;
    public int HP;
    public int Armor;
    public int Damage;
    public int Gold;
    public int Mana;
    
    public void Save()
    {
        GetData();
        JSONObject playerJson = new JSONObject();
        playerJson.Add("HP", HP);
        playerJson.Add("Armor", Armor);
        playerJson.Add("Damage", Damage);
        playerJson.Add("Gold", Gold);
        playerJson.Add("Mana", Mana);

        Debug.Log(playerJson.ToString());

        string path = Application.persistentDataPath + "/PlayerSave.Json";
        File.WriteAllText(path, playerJson.ToString());
    }


    void GetData()
    {
        if (PlayerManager.instance.player != null && PlayerManager.instance.player.GetComponent<PlayerStats>().enabled) {
            HP = PlayerManager.instance.player.GetComponent<PlayerStats>().currentHealth;
            Armor = PlayerManager.instance.player.GetComponent<PlayerStats>().armor.GetValue();
            Damage = PlayerManager.instance.player.GetComponent<PlayerStats>().damage.GetValue();
            Gold = GoldCounter.instance.gold;
            Mana = PlayerManager.instance.player.GetComponent<PlayerStats>().CurrentMana;
        }
    }


    public void Load()
    {
        string path = Application.persistentDataPath + "/PlayerSave.Json";
        string jsonString = File.ReadAllText(path);
        JSONObject playerJson = (JSONObject)JSON.Parse(jsonString);
        HP = playerJson["HP"];
        Armor = playerJson["Armor"];
        Damage = playerJson["Damage"];
        Gold = playerJson["Gold"];
        Mana = playerJson["Mana"];
        GoldCounter.instance.SetGold(Gold);
        PlayerManager.instance.player.GetComponent<PlayerStats>().SetHealth(HP);
        PlayerManager.instance.player.GetComponent<PlayerStats>().SetMana(Mana);
        Debug.Log(playerJson.ToString());
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
    }
}
