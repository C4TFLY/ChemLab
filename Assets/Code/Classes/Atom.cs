using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Atom
{
    public bool valid;
    private string name;
    private string isotope;
    private string chemicalName;
    private int protons;
    private int neutrons;
    private int massNumber;

    public string Name { get { return name; } }
    public string Isotope { get { return isotope; } }
    public string ChemicalName { get { return chemicalName; } }
    public int Protons { get { return protons; } }
    public int Neutrons { get { return neutrons; } }
    public int MassNumber { get { return massNumber; } }

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
