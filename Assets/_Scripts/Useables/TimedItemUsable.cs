using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimedItemUsable : ItemModeUsable
{
    protected float counter;

    public override void DefineDuringModeEnabled(){
        if(!Input.GetMouseButton(0) || !CheckAdditionalCondition()) {
            counter = 0;
            return;
        }

        counter += Time.deltaTime;
        if(counter < GetActionDuration()) return;

        OnTimerFinished();
    }

    protected abstract bool CheckAdditionalCondition();
    protected abstract void OnTimerFinished();
    protected abstract float GetActionDuration();
    protected override abstract void DefineOnModeEnabled(UsableInventoryItemSO so);
}
