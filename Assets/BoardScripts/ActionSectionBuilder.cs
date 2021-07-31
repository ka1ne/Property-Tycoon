using UnityEngine;

// concrete ActionSection builder class
class ActionSectionBuilder : SectionBuilder
{
    new ActionSection section;
    // gets ActionSection instance
    public new ActionSection Section 
    {
        get {return section;}
        set {section = value;}
    }

    // implement build methods here
    public override void SetFields(string[] fields, GameObject Field)
    {
        Section = Field.AddComponent(typeof(ActionSection)) as ActionSection;

        Section.Action = fields[4];
    }
}