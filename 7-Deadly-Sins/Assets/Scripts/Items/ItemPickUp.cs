using UnityEngine;

public class ItemPickUp : Interactable
{
    public Item item;
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }
    void PickUp()
    {
        DisplayTextManager.instance.Display("Picked up " + item.name, 3f);
        Debug.Log("Picking up " + item.name);
        if (item.GetType() == typeof(Equipment))
        {
            bool wasPickedUp = Inventory.instance.Add(item);
            if (wasPickedUp)
            {
                Destroy(gameObject);
            }
        }
        else if (item.GetType() == typeof(Currency))
        {
            Currency currency = new Currency(item); 
            GoldCounter.instance.Earn(currency.GetPickUpAmount());
            Destroy(gameObject);
        }
        else if (item is Consumables)
        {
            bool wasPickedUp = Inventory.instance.Add(item);
            if (wasPickedUp)
                Destroy(gameObject);
        } else if (item is Others)
        {
            bool wasPickedUp = Inventory.instance.Add(item);
            if (wasPickedUp)
                Destroy(gameObject);
        }
    }
}
