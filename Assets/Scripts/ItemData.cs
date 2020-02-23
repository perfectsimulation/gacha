using System;

[Serializable]
public class ItemData
{
    public int id;
    public string name;
    public int quantity;

    public ItemData(int id, string name, int quantity)
    {
        this.id = id;
        this.name = name;
        this.quantity = quantity;
    }

    public void IncreaseQuantity(int quantity)
    {
        this.quantity += quantity;
    }
}
