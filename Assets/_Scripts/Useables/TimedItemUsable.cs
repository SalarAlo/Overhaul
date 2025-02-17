using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimedItemUsable : ItemModeUsable
{
    [SerializeField] private ProgressBarUI progressBarUIPrefab;
    protected float counter;
    private ProgressBarUI progressBarUI;

    public override void DuringModeEnabled(){
        if(!Input.GetMouseButton(0) || ShouldCancelCounter()) {
            counter = 0;
            if(progressBarUI != null) Destroy(progressBarUI.gameObject);
            return;
        }

        if(counter == 0) {
            progressBarUI = Instantiate(progressBarUIPrefab, GetProgressBarPos(), Quaternion.identity);
            progressBarUI.StartProgressing(GetActionDuration());
        }

        counter += Time.deltaTime;
        if(counter < GetActionDuration()) return;

        TimerFinished();
    }

    private void TimerFinished() {
        counter = 0;
        OnTimerFinished();
    }

    protected abstract Vector3 GetProgressBarPos();

    protected abstract bool ShouldCancelCounter();
    protected abstract void OnTimerFinished();
    protected abstract float GetActionDuration();
    protected override abstract void OnModeEnabled(UsableInventoryItemSO so);
}
