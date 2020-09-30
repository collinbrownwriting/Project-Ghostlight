using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GridMaster {
public class HealthBar : MonoBehaviour
{
    public Sprite hOne;
    public Sprite hTwo;
    public Sprite hThree;
    public Sprite hFour;
    public Sprite hFive;
    public Sprite hSix;
    public Sprite hSeven;
    public Sprite hEight;
    public Sprite hNine;
    public Sprite hTen;

    public Sprite hEleven;
    public Sprite hTwelve;
    public Sprite hThirteen;
    public Sprite hFourteen;
    public Sprite hFifteen;
    public Sprite hSixteen;
    public Sprite hSeventeen;
    public Sprite hEighteen;
    public Sprite hNineteen;
    public Sprite hTwenty;

    public Sprite hTwentyOne;
    public Sprite hTwentyTwo;
    public Sprite hTwentyThree;
    public Sprite hTwentyFour;
    public Sprite hTwentyFive;
    public Sprite hTwentySix;
    public Sprite hTwentySeven;
    public Sprite hTwentyEight;
    public Sprite hTwentyNine;
    public Sprite hThirty;
    public GameObject player;
    public int lastHealth;

    public static HealthBar instance;

    void Awake () {
        instance = this;
    }

    public void CheckHealth()
    {
        if (player.GetComponent<CharacterData>().health != lastHealth){
        if (player.GetComponent<CharacterData>().health <= 0) {
            lastHealth = 0;
            this.GetComponent<Image>().enabled = false;
        }
        
        if (player.GetComponent<CharacterData>().health == 1){
            lastHealth = 1;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hOne;
        }
        if (player.GetComponent<CharacterData>().health == 2){
            lastHealth = 2;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hTwo;
        }
        if (player.GetComponent<CharacterData>().health == 3){
            lastHealth = 3;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hThree;
        }
        if (player.GetComponent<CharacterData>().health == 4){
            lastHealth = 4;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hFour;
        }
        if (player.GetComponent<CharacterData>().health == 5){
            lastHealth = 5;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hFive;
        }
        if (player.GetComponent<CharacterData>().health == 6){
            lastHealth = 6;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hSix;
        }
        if (player.GetComponent<CharacterData>().health == 7){
            lastHealth = 7;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hSeven;
        }
        if (player.GetComponent<CharacterData>().health == 8){
            lastHealth = 8;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hEight;
        }
        if (player.GetComponent<CharacterData>().health == 9){
            lastHealth = 9;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hNine;
        }
        if (player.GetComponent<CharacterData>().health == 10){
            lastHealth = 10;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hTen;
        }

        if (player.GetComponent<CharacterData>().health == 11){
            lastHealth = 11;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hEleven;
        }
        if (player.GetComponent<CharacterData>().health == 12){
            lastHealth = 12;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hTwelve;
        }
        if (player.GetComponent<CharacterData>().health == 13){
            lastHealth = 13;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hThirteen;
        }
        if (player.GetComponent<CharacterData>().health == 14){
            lastHealth = 14;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hFourteen;
        }
        if (player.GetComponent<CharacterData>().health == 15){
            lastHealth = 15;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hFifteen;
        }
        if (player.GetComponent<CharacterData>().health == 16){
            lastHealth = 16;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hSixteen;
        }
        if (player.GetComponent<CharacterData>().health == 17){
            lastHealth = 17;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hSeventeen;
        }
        if (player.GetComponent<CharacterData>().health == 18){
            lastHealth = 18;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hEighteen;
        }
        if (player.GetComponent<CharacterData>().health == 19){
            lastHealth = 19;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hNineteen;
        }
        if (player.GetComponent<CharacterData>().health == 20){
            lastHealth = 20;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hTwenty;
        }

        if (player.GetComponent<CharacterData>().health == 21){
            lastHealth = 21;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hTwentyOne;
        }
        if (player.GetComponent<CharacterData>().health == 22){
            lastHealth = 22;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hTwentyTwo;
        }
        if (player.GetComponent<CharacterData>().health == 23){
            lastHealth = 23;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hTwentyThree;
        }
        if (player.GetComponent<CharacterData>().health == 24){
            lastHealth = 24;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hTwentyFour;
        }
        if (player.GetComponent<CharacterData>().health == 25){
            lastHealth = 25;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hTwentyFive;
        }
        if (player.GetComponent<CharacterData>().health == 26){
            lastHealth = 26;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hTwentySix;
        }
        if (player.GetComponent<CharacterData>().health == 27){
            lastHealth = 27;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hTwentySeven;
        }
        if (player.GetComponent<CharacterData>().health == 28){
            lastHealth = 28;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hTwentyEight;
        }
        if (player.GetComponent<CharacterData>().health == 29){
            lastHealth = 29;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hTwentyNine;
        }
        if (player.GetComponent<CharacterData>().health == 30){
            lastHealth = 30;
            this.GetComponent<Image>().color = Color.black;
            Invoke("Whitening",0.15f);
            this.GetComponent<Image>().sprite = hThirty;
        }
        }
    }

    public void Whitening () {
        this.GetComponent<Image>().color = Color.white;
    }
}
}
