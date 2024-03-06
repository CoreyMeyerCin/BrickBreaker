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
*/