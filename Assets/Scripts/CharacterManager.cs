using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance;
    public static CharacterManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();
                // CharacterManager가 없으면 새로 생성하고 CharacterManager를 생성하는 로직. (방어 코드)
            }
            return _instance;
        }
    }

    public Player _player;
    public Player Player
    {
        get
        {
            return _player;
        }
        set
        {
            _player = value;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance == this)
            {
                Destroy(gameObject);
            }
        }
    }
}
