using UnityEngine;
public class UIManager : Manager<UIManager>
{
    public GameObject SkillCanvasObject;
    public void ShowSkillCanvas()
    {
        //Debug.Log("ShowSkillCanvas TRUE");
        SkillCanvasObject.SetActive(true);
    }
    public void HideSkillCanvas()
    {
        //Debug.Log("ShowSkillCanvas FALSE");
        SkillCanvasObject.SetActive(false);
    }
}
