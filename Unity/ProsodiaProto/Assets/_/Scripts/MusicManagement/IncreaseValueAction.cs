public class IncreaseValueAction : ActionHolder
{
    public IntVariable ValueToIncrease;
    public IntVariable IncreaseAmount;
    private bool hasBeenSolved = false;
    protected override void CreateAction()
    {
        Action = () =>
        {
            if (hasBeenSolved)
                return;
            ValueToIncrease.Value += IncreaseAmount.Value;
            hasBeenSolved = true;
        };
    }
}