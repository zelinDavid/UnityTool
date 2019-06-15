using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    #region basic property
    private int basicStrength = 10;
    private int basicIntellect = 10;
    private int basicAgility = 10;
    private int basicStamina = 10;
    private int basicDamage = 10;

    public int BasicStrength {
        get {
            return basicStrength;
        }
    }
    public int BasicIntellect {
        get {
            return basicIntellect;
        }
    }
    public int BasicAgility {
        get {
            return basicAgility;
        }
    }
    public int BasicStamina {
        get {
            return basicStamina;
        }
    }
    public int BasicDamage {
        get {
            return basicDamage;
        }
    }
    #endregion
    private int coinAmount = 100;

    private Text coinText;

    public int CoinAmount {
        get {
            return coinAmount;
        }
        set {
            coinAmount = value;
            // coinText.text = coinAmount.ToString ();
        }
    }

    private void Start () {
        // coniText = GameObject.Find("Coin").getcom
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            int id = Random.Range(1,18);
            Knapsack.Instance.StoreItem(id);
        }
    }

}