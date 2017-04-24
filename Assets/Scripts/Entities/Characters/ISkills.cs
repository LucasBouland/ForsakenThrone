using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkills {

    void Jump(GameObject Character);
    void SimpleAttack(GameObject Character, bool direction);
    void Special1(GameObject Character, bool direction);

}
