using UnityEngine;

[CreateAssetMenu(fileName = "new Page" , menuName = "Page")]
public class PageSO : ScriptableObject
{
    [TextArea]
    [SerializeField] private string subtitle;

    [SerializeField] private Sprite sprite;


    public string Subtitle => subtitle;

    public Sprite Sprite => sprite;
}
