using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// concrete PropertySection builder class
class PropertySectionBuilder : SectionBuilder
{
    new PropertySection section;
    // gets PropertySection instance
    public new PropertySection Section 
    {
        get {return section;}
        set {section = value;}
    }

    // implement build methods here
    public override void SetFields(string[] fields, GameObject Field)
    {
        Section = Field.AddComponent(typeof(PropertySection)) as PropertySection;
        
        Section.Position = ParseInt(fields[0]);
        Section.Id = fields[1];
        // [2] is blank
        Section.Group = fields[3];
        // [4] is action
        // [5] is canBeBought which is stored
        // [6] is blank
        Section.Price = ParseInt(fields[7]);
        section.Rent = ParseInt(fields[8]);
        // [9] is blank
        Section.OneHouse = ParseInt(fields[10]);
        Section.TwoHouse = ParseInt(fields[11]);
        Section.ThreeHouse = ParseInt(fields[12]);
        Section.FourHouse = ParseInt(fields[13]);
        Section.OneHotel = ParseInt(fields[14]);

        
    }
}
