using TMPro;
using UnityEngine;

//!  A test class. 
/*!
  A more elaborate class description.
*/
public class FieldsText : MonoBehaviour
{
    void Start()
    {
        transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = transform.GetComponent<PropertySection>().Id;
        transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = transform.GetComponent<PropertySection>().Price.ToString();
    }
}
