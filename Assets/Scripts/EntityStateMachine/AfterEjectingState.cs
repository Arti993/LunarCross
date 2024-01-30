using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AfterEjectingState : EntityBaseState
{
    protected AfterEjectingState(IEntityStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
    }
}
