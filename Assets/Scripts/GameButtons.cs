using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtons : MonoBehaviour {
    [SerializeField] GameManager gm;
    public static int tempPi;

    private void Start() {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Buy() {
        gm.players[gm.pi].GetComponent<Player>().Buy();
    }
    //----
    public void Auction() {
        gm.players[gm.pi].GetComponent<Player>().Auction();
    }
    public void OnePound()  //using tempPi instead of pi
    {
        gm.players[tempPi].GetComponent<Player>().OnePound();
    }
    public void TenPound() {
        gm.players[tempPi].GetComponent<Player>().TenPound();
    }
    public void HundredPound() {
        gm.players[tempPi].GetComponent<Player>().HundredPound();
    }
    public void Withdraw() {
        gm.players[tempPi].GetComponent<Player>().Withdraw();
    }
    //----
    public void PayForJail() {
        gm.players[tempPi].GetComponent<Player>().PaidForJail();
    }
    public void StayInJail() {
        gm.players[tempPi].GetComponent<Player>().StayingInJail();
    }
    //----
    public void ViewProperties() {
        gm.players[gm.pi].GetComponent<Player>().ViewProperties();
    }
    public void NextProperty() {
        gm.players[gm.pi].GetComponent<Player>().NextProperty();
    }
    public void UpgradeProperty() {
        gm.players[gm.pi].GetComponent<Player>().UpgradeProperty();
    }
    public void DowngradeProperty() {
        gm.players[gm.pi].GetComponent<Player>().DowngradeProperty();
    }
    public void BetweenTurns() {
        gm.players[gm.pi].GetComponent<Player>().SwitchPlayer();
        gm.players[gm.pi].GetComponent<Player>().BetweenTurns();
    }
    public void BeforeTurnEnd() {
        gm.players[gm.pi].GetComponent<Player>().BeforeTurnEnd();
    }
    //---
    public void TradeCancel() {
        gm.players[gm.pi].GetComponent<Player>().TradeCancel();
    }
    public void TradePlayerMenu() {
        gm.players[gm.pi].GetComponent<Player>().TradePlayerMenu();
    }
    public void TradeNextPlayer() {
        gm.players[gm.pi].GetComponent<Player>().TradeNextPlayer();
    }
    public void StartTrading() {
        gm.players[gm.pi].GetComponent<Player>().StartTrading();
    }
    public void MyNextTradeProperty() {
        gm.players[gm.pi].GetComponent<Player>().MyNextTradeProperty();
    }
    public void TheirNextTradeProperty() {
        gm.players[gm.pi].GetComponent<Player>().TheirNextTradeProperty();
    }
    public void AddMyProperty() {
        gm.players[gm.pi].GetComponent<Player>().AddMyProperty();
    }
    public void AddTheirProperty() {
        gm.players[gm.pi].GetComponent<Player>().AddTheirProperty();
    }
    public void AddMyMoney() {
        gm.players[gm.pi].GetComponent<Player>().AddMyMoney();
    }
    public void AddTheirMoney() {
        gm.players[gm.pi].GetComponent<Player>().AddTheirMoney();
    }
    public void OfferBack() {
        gm.players[gm.pi].GetComponent<Player>().OfferBack();
    }
    public void ConfirmOffer() {
        gm.players[gm.pi].GetComponent<Player>().ConfirmOffer();
    }
    public void AcceptTrade() {
        gm.players[gm.pi].GetComponent<Player>().TradingAgreed();
    }
    public void DenyTrade() {
        gm.players[gm.pi].GetComponent<Player>().TradingDenied();
    }
    //---
    public void Bankrupt() {
        gm.players[gm.pi].GetComponent<Player>().GoBankrupt();
    }
    public void Mortgage() {
        gm.players[gm.pi].GetComponent<Player>().Mortgage();
    }
    public void Sell() {
        gm.players[gm.pi].GetComponent<Player>().Sell();
    }
}
