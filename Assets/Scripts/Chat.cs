using Events;
using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    [SerializeField]
    private Text _text;
    [SerializeField]
    private int _limitMessage;

    private int _currentMessage;
    private CompositeDisposable _subscription;
        
    private void Awake()
    {
        _subscription = new CompositeDisposable
        {
            EventStreams.UserInterface.Subscribe<EventMessageFullStock>(DisplayMessage),
            EventStreams.UserInterface.Subscribe<EventResourceShortage>(DisplayMessage)
        };
    }
        
    private void DisplayMessage(EventMessageFullStock eventMessageFullStock)
    {
        DisplayMessage(eventMessageFullStock.Message);
    }
        
    private void DisplayMessage(EventResourceShortage eventResourceShortage)
    {
        DisplayMessage(eventResourceShortage.Message);
    }

    private void DisplayMessage(string message)
    {
        if (_currentMessage == _limitMessage)
        {
            _text.text = "";
            _currentMessage = 0;
        }
        
        _currentMessage++;
        _text.text += message + " \n";
    }

    private void OnDestroy()
    {
        _subscription?.Dispose();
    }
}