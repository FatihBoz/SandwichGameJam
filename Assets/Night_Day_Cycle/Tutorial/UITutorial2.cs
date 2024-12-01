using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITutorial2 : TutorialBase
{
      [SerializeField] private float rotateAmount;
    [SerializeField] private Vector3 position;

    [SerializeField] private RectTransform arrowPointer;

    private CycleManager cycleManager;
    protected override bool CheckTutorialCompletion()
    {
        return Input.GetMouseButtonDown(0);
    }

    protected override void SetupTutorial()
    {
        cycleManager=FindObjectOfType<CycleManager>();
        CycleManager.OnNightStarted+=CompleteTutorial;
    }

    private void Update()
    {
    
    }
    public override void StartTutorial()
    {
        base.StartTutorial();
       arrowPointer.gameObject.SetActive(true);
       arrowPointer.eulerAngles=new Vector3(0,0,rotateAmount);
        arrowPointer.localPosition=position;
        cycleManager.SetIsStopped(false);

    }
    protected override void CompleteTutorial()
    {
        arrowPointer.gameObject.SetActive(false);
        base.CompleteTutorial();

    }
}
