using UnityEngine;

public class AnimationTime : MonoBehaviour
{
    public float Duration;
    public float CurrentTime;
    public StateAnime State;
    public UnityEngine.Events.UnityEvent<float> Updated = new UnityEngine.Events.UnityEvent<float>();
    public UnityEngine.Events.UnityEvent<float> Stoped = new UnityEngine.Events.UnityEvent<float>();

    protected float _time;
    protected StateAnime _state;

    private void Start()
    {
        _state = State;
    }
    
    private void Update()
    {
        _state = State;
        _time = CurrentTime;
        
        if(_state == StateAnime.STARTED)
        {
            UpdateTime();
        }
    }

    protected void UpdateTime()
    {
        if(_time < Duration)
        {
            _time = Mathf.Clamp(_time + Time.deltaTime, 0, Duration);
            CurrentTime = _time;
            Updated.Invoke(_time);
        }
        else
        {
            _state = StateAnime.STOPED;
            State = StateAnime.STOPED;
            _time = Duration;
            CurrentTime = Duration;
            Stoped.Invoke(_time);
        }
    }

    public void StartAnimation()
    {
        _time = 0;
        CurrentTime = 0;
        _state = StateAnime.STARTED;
        State = StateAnime.STARTED;
    }
}