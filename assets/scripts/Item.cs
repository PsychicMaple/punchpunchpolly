using Godot;
using System;

public partial class Item : Node
{
    // fields
    public int id;
    public string name;
    public string infotext;

    // constructors
    public Item(int i, string n, string t)
    {
        id = i;
        name = n;
        infotext = t;
    }

    // methods
    public void Use()
    {
        GD.Print(name + " (" + id + ") has been used.");
    }
}
