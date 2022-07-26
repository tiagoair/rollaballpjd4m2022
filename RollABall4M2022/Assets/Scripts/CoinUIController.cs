using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUIController : MonoBehaviour
{
    // referencia para o objeto de texto da interface
    [SerializeField] private TMP_Text coinText;

    private void OnEnable()
    {
        // Se inscreve no canal de coins
        PlayerObserverManager.OnCoinsChanged += UpdateCoinText;
    }

    private void OnDisable()
    {
        // Retira a inscrição no canal de coins
        PlayerObserverManager.OnCoinsChanged -= UpdateCoinText;
    }

    // funcao usada para tratar a notificacao do canal
    // de coins
    private void UpdateCoinText(int newCoinsValue)
    {
        // atualiza o valor das moedas na interface
        coinText.text = newCoinsValue.ToString();
    }
}
