using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public virtual void Use()
    {
        //DisplayTextManager.instance.Display("Using " + name, 3f);
        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory() {
        Inventory.instance.Remove(this);
    }

   public virtual int GetPrice() {
       return 0;
   }

   public override bool Equals(object other) {
       if (other is Item) {
           return this.name == ((Item) other).name;
       } else {
           return false;
       }
       
   }
}
