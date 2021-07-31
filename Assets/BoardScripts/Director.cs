using System.Linq;
using System.Collections.Generic;
using UnityEngine;

// director class
class Director
{
    public void ConstructSection(string[] data, SectionBuilder sectionBuilder, GameObject Field)
    {
        sectionBuilder.SetFields(data, Field);
    }
    public void ConstructCard(string[] data, CardBuilder cardBuilder, string type)
    {
        cardBuilder.SetFields(data, type);
    }
}
