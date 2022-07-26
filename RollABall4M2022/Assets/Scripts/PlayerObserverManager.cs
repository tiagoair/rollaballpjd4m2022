using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Isso seria o nosso Youtube.com
// modificador static diz que pode ser acessado
// de qualquer lugar no código
public static class PlayerObserverManager
{
    // Canal da variavel coins do PlayerController
    // 1 - Parte da inscrição
    public static Action<int> OnCoinsChanged;
    
    // 2 - Parte do sininho (notificação)
    public static void CoinsChanged(int value)
    {
        // Existe alguém inscrito em OnCoinsChanged?
        // caso tenha, mande o value para todos
        OnCoinsChanged?.Invoke(value);
    }
}
