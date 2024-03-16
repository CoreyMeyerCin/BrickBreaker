/*
    Right now there can only be 1 text element or the Score will break.
        search ScoreManager.cs for "//TODO: ScoreText" to see where this reference
        is if you need to add more text elements.

    Please put all inputs into InputManager.cs; it is okay to make more
        inputs outside of there but then they should be refactored into there.

    I find it helpful to use gameObject.AddComponent<>() to add components
        instead of adding them in the inspector. This way you can see
        the origin of the component in the script instead of hoping that it
        was added on Unity's side.

    Events/Actions should be defined in EventManager
        Calling an event: Events.MethodName();
        Subscribe to event: in OnEnable -- Events.OnBlockDestroyed += BlockDestroyed;
        use -= for unsub in OnDisable 
        "BlocksDestroyed" is the name of the method that should be executed within the class that has subscribed to the event

    Adding a level up power choice
        1) add action to EventManager, then subscribe to event and add the executing method into the file wherever it should happen
        2) add a UI TMP button under SelectPowerupPanel -> Main Panel. Tag the button with "powerup_button".
        3) add on-click event in button inspector and drag SelectPowerupPanel into it, then select the method from MenuManager.

    To FindObject the Object MUST be active. If you want to load in inactive objects, use Resources.Load<GameObject>("path/to/prefab") or AssetDatabase.LoadAssetAtPath<GameObject>("path/to/prefab")
*/