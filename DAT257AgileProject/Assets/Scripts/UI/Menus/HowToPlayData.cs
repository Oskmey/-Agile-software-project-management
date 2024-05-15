using UnityEngine;

[CreateAssetMenu(fileName = "NewHowToPlayScriptableObject", menuName = "ScriptableObjects/HowToPlayScriptableObject")]
public class HowToPlayData : ScriptableObject
{
    [SerializeField]
    private HowToPlayScreenType screenType;

    [TextArea(20, 40)]
    [SerializeField]
    private string contentText;

    [SerializeField]
    private Sprite topImage;

    [SerializeField]
    private Sprite bottomImage;

    public HowToPlayScreenType ScreenType => screenType;
    public string ContentText => contentText;
    public Sprite TopImage => topImage;
    public Sprite BottomImage => bottomImage;
}
