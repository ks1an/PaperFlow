using UnityEngine;

[RequireComponent (typeof(Animator))]
public sealed class FocusBackgroundPanel : MonoBehaviour
{
    public static GameObject FocusBackPanel;
    private Animator _anim;

    private void Awake()
    {
        FocusBackPanel = this.gameObject;
        _anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _anim.SetTrigger("In");
    }
}
