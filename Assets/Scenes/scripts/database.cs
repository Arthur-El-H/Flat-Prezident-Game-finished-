using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class database : MonoBehaviour
{
    public bool akdUnreliable { get; set; }
    public bool akdNoGeography { get; set; }
    public bool akdNeedsControl { get; set; }
    public bool noCountryInSouth { get; set; }
    public bool tunnelsNeeded { get; set; }
    public bool changePhysics { get; set; }
    public bool changeHistory { get; set; }
    public bool noPoll { get; set; }
    public bool helpForSouthAfrica { get; set; }
    public bool newReform { get; set; }


    public void init()
    {
        akdUnreliable = false;
        akdNoGeography = false;
        akdNeedsControl = false;
        noCountryInSouth = false;
        tunnelsNeeded = false;
        changePhysics = false;
        changeHistory = false;
        noPoll = false;
        helpForSouthAfrica = false;
        newReform = false;
    }

}
