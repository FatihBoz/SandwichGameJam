using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITutorial : TutorialBase
{
    [SerializeField] private float rotateAmount;
    [SerializeField] private Vector3 position;

    [SerializeField] private RectTransform arrowPointer;
    protected override bool CheckTutorialCompletion()
    {
        return Input.GetMouseButtonDown(0);
    }

    protected override void SetupTutorial()
    {
    }

    private void Update()
    {
        if (isActive && CheckTutorialCompletion())
        {
            CompleteTutorial();
        }
}
    public override void StartTutorial()
    {
        base.StartTutorial();
       arrowPointer.gameObject.SetActive(true);
       arrowPointer.eulerAngles=new Vector3(0,0,rotateAmount);
        arrowPointer.localPosition=position;
        Debug.Log(gameObject.name);
    }
    protected override void CompleteTutorial()
    {
        arrowPointer.gameObject.SetActive(false);
        base.CompleteTutorial();

    }
}
