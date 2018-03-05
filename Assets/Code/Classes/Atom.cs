using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atom
{
    public bool valid { get; }
    private string name { get; }
    private string isotope { get; }
    private string chemicalName { get; }
    private int protons { get; }
    private int neutrons { get; }
    private int massNumber { get; }

    //public string Name
    //{
    //    get
    //    {
    //        return name;
    //    }
    //}

    public Atom(bool valid)
    {
        this.valid = valid;
    }

    public Atom(bool valid, string name, string isotope, string chemicalName, int protons, int neutrons, int massNumber)
    {
        this.valid = valid;
        this.name = name;
        this.isotope = isotope;
        this.chemicalName = chemicalName;
        this.protons = protons;
        this.neutrons = neutrons;
        this.massNumber = massNumber;
    }
}
