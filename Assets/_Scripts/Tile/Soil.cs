using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Soil : RoundedTileObject<Soil>
{
    [Header("Config")]
    [SerializeField] private Transform plantParent;

    [Header("Data")]
    [SerializeField] private SeedBagItemSO currentSeed;
    [SerializeField] private bool growing = false;
    [SerializeField] private bool readyToHarvest;
    [SerializeField] private int currentStage = -1;
    [SerializeField] private float timeBeforeNextStageCounter;
    [SerializeField] private float timeBeforeNextStage;

    private Quaternion randomRotation;

    private void Awake() {
        randomRotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 359), 0);
    }

    public void PlantSeed(SeedBagItemSO seedBagItemSO) {
        currentSeed = seedBagItemSO;
        StartGrowing();
    }

    private void StartGrowing() {
        growing = true;
        currentStage = 0;
        GrowToCurrentStage();
    }

    private void Update() {
        if(!growing) return;
        HandleGrowingProcess();
    }

    private void HandleGrowingProcess(){
        timeBeforeNextStageCounter += Time.deltaTime;

        if(timeBeforeNextStageCounter >= timeBeforeNextStage) {
            currentStage++;
            GrowToCurrentStage();
        }
    }

    private void GrowToCurrentStage() {
        int randomSecondsOffset = 20;

        foreach(Transform child in plantParent) Destroy(child.gameObject);

        Instantiate(
            currentSeed.stages[currentStage], 
            plantParent.transform.position, 
            randomRotation,
            plantParent
        );

        if(currentStage == currentSeed.stages.Count-1) {
            EndGrowingProcess();
        }

        timeBeforeNextStage = OverhaulFormulas.GetStageDuration(currentSeed.baseGrowthValue, currentStage) + UnityEngine.Random.Range(0, randomSecondsOffset);
        timeBeforeNextStageCounter = 0;

    }

    public bool IsOccupied() => currentStage != -1;

    private void EndGrowingProcess(){
        growing = false;
        readyToHarvest = true;
    }
}
