using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }
    public List<Skill> Skills = new List<Skill>();
    public List<ObservableKeyValue> SkillLevels = new List<ObservableKeyValue>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        foreach(Skill skill in Skills)
        {
            ObservableKeyValue keyValue = new ObservableKeyValue(skill.SkillName.ToString(), "0");
            SkillLevels.Add(keyValue);
        }
        foreach (ObservableKeyValue keyValue in SkillLevels)
        {
            keyValue.OnValueChanged += newValue => { Debug.Log("testing::" + keyValue.Key + " " + newValue);};
        }
    }

    public void AddSkill(Skill skill)
    {
        if (!Skills.Contains(skill))
        {
            Skills.Add(skill);
            ObservableKeyValue keyValue = new ObservableKeyValue(skill.SkillName.ToString(), "0");
            SkillLevels.Add(keyValue);
        }
    }
    public void UpgradeSkill(Skill inputSkill){
        ObservableKeyValue skill = SkillLevels.Find(x => x.Key == inputSkill.SkillName);
        if(skill != null){
            int level = int.Parse(skill.Value);
            level++;
            skill.Value = level.ToString();
            inputSkill.SkillLevel = level;
        }
    }
}

[System.Serializable]
public class KeyValue
{
    public string Key;
    public string Value;
}
public class ObservableKeyValue
{
    public string Key { get; set; }
    private string _value;
    public string Value 
    { 
        get { return _value; } 
        set 
        {
            _value = value;
            OnValueChanged?.Invoke(_value);
        } 
    }

    public event Action<string> OnValueChanged;

    public ObservableKeyValue(string key, string value)
    {
        Key = key;
        Value = value;
    }
}