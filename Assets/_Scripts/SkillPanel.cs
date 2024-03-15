using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    [SerializeField]
    private Image _image;
    [SerializeField]
    private Text _skillName;
    [SerializeField]
    private Text _skillDescription;
    [SerializeField]
    private Text _skillLevel;
    [SerializeField]
    private Text _skillCost;
    [SerializeField]
    private Button _skillButton;
    private Skill _skill;
    public void Start()
    {
        _skillButton = GetComponentInChildren<Button>();
        ResetPanel();
    }
    private void OnButtonPressed(){
        SkillManager.Instance.UpgradeSkill(_skill);
        Time.timeScale = 1f;
        UIManager.Instance.HideSkillCanvas();
        BallController ballController = FindObjectOfType<BallController>();
        MenuManager.Instance.CloseMenu(MenuManager.Instance.SelectPowerupPanel);

        ResetPanel();
    }
    private void ResetPanel(){
        _skill = SkillManager.Instance.Skills[Random.Range(0, SkillManager.Instance.Skills.Count)];
        _image = GetComponentInChildren<Image>();
        _image.sprite = _skill.SkillIcon;
        Text[] texts = GetComponentsInChildren<Text>();
        if (texts.Length >= 3)
        {
            _skillName = texts[0];
            _skillName.text = _skill.SkillName;
            _skillLevel = texts[1];
            _skillLevel.text = _skill.SkillLevel.ToString();
            _skillDescription = texts[2];
            _skillDescription.text = _skill.SkillDescription;
        }
        if (texts.Length >= 4)
        {
            _skillCost = texts[3];
            _skillCost.text = _skill.SkillCost.ToString();
        }
        _skillButton = GetComponentInChildren<Button>();
        _skillButton.onClick.RemoveAllListeners();
        _skillButton.onClick.AddListener(OnButtonPressed);

    }
}
