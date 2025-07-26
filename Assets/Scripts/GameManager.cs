using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int currency;
    [SerializeField] private int maxHp;
    [SerializeField] private int currentHp;

    private UI_InGame uiInGame;

    private void Awake()
    {
        uiInGame = FindFirstObjectByType<UI_InGame>(FindObjectsInactive.Include);
        if (uiInGame == null)
        {
            Debug.LogError("UI_InGame not found in the scene.");
        }
        else
        {
            uiInGame.UpdateHealthPoints(currentHp, maxHp);
        }
    }

    private void Start()
    {
        currentHp = maxHp;
        uiInGame.UpdateHealthPoints(currentHp, maxHp);
        uiInGame.UpdateCurrencyUI(currency);
    }

    public void UpdateHp(int value)
    {
        currentHp += value;
        uiInGame.UpdateHealthPoints(currentHp, maxHp);
    }

    public void UpdateCurrency(int value)
    {
        currency += value;
        uiInGame.UpdateCurrencyUI(currency);
    }

    public bool HasEnoughCurrency(int price)
    {
        if(price < currency)
        {
            currency -= price;
            uiInGame.UpdateCurrencyUI(currency);
            return true;
        }
        else
        {
            Debug.LogWarning("Not enough currency to perform this action.");
            return false;
        }
    }
}
