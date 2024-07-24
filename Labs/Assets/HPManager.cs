using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{

    private Player _player;
    private Slider _slider;
    private TMP_Text _hpText;
    
    private void Start()
    {
        var playerGameObject = GameObject.FindWithTag("Player");
        _player = playerGameObject.GetComponent<Player>();
        
        var sliderGameObject = GameObject.Find("Slider");
        _slider = sliderGameObject.GetComponent<Slider>();

        var hpTextGameObject = GameObject.Find("HpText");
        _hpText = hpTextGameObject.GetComponent<TMP_Text>();
    }

    private void Update()
    {
        HandlePlayerHp();
    }
    
    private void HandlePlayerHp()
    {
        _slider.value = (float)_player.Hp / Player.MaxHp;
        _hpText.text = $"{_player.Hp} / {Player.MaxHp}";
    }
}
