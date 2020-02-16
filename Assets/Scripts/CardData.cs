using System;

[Serializable]
public class CardData
{
    // TODO: implement level and experience
    // TODO: design interesting properties - placeholder values for now
    public string name;
    public string imageUrl;
    public int red;
    public int orange;
    public int yellow;
    public int green;
    public int blue;
    public int purple;
    public int pink;

    public CardData()
    {
        this.name = "card";
        this.imageUrl = "";
        this.red = 0;
        this.orange = 0;
        this.yellow = 0;
        this.green = 0;
        this.blue = 0;
        this.purple = 0;
        this.pink = 0;
    }

    public CardData(string name, string imageUrl, int r, int o, int y, int g, int b, int pu, int pi)
    {
        this.name = name;
        this.imageUrl = imageUrl;
        this.red = r;
        this.orange = o;
        this.yellow = y;
        this.green = g;
        this.blue = b;
        this.purple = pu;
        this.pink = pi;
    }
}
