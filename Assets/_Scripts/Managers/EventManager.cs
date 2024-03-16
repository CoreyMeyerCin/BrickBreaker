using System;

public class Events
{   
    public static Action<Block> OnBlockDestroyed;
    public static Action<int> OnPointsAdded;
    public static Action OnLeveLUp;
    public static Action OnStageComplete;

    //effects
    public static Action OnPowerupPaddleExpand;
    public static Action OnPowerupPowerIncrease;
    public static Action OnPowerupBallSpeedIncrease;
    public static Action OnPowerupBallSizeIncrease;


	public static Action OnPaddleDecrease;
    public static Action OnTimeStop;
}
