using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject bombPrefab;
    public Text ScoreTxt;
    public Rigidbody2D rb;
    public static int hscore, score;
    public static float y; //bu y değerini gamedata ya göndereceğim
    public static float x; //bu x değerini gamedata ya göndereceğim
    public Transform ball;
    string path= "C:/Users/Merve/VizeProje/Assests";
    void Start()
    {
        string sahne = PlayerPrefs.GetString("sahne");
        PlayerPrefs.SetString("sahne", "null");//oyun her başladığımda new veya load göre hareket etmesin
        Debug.Log(sahne);
        if (sahne=="load")
        {
            Load();
            transform.position = new Vector3(x, y); //eski kaydedilen konumları aldım ve bu konumları player atadım
            ScoreTxt.text = score.ToString(); //eski kaydedilen score aldım ve atadım
        }
        else if (sahne == "new")
        {
            ScoreTxt.text = score.ToString(); //eğer yeni oyun seçildiyse score 0 dır ve bu ekrana yazılsın
        }
        else // oyun yeni değilse ve eski oyun açılmadıysa yani oyuncu topa yaklaştığı için oyun yeniden başlatılıyorsa buraya girecek
        {
            Load();
            transform.position = new Vector2(transform.position.x, -5.561102f); 
            ScoreTxt.text = score.ToString();
        }

    }

    void Update()
    {
             x = transform.position.x; //gamedatadan x çekileceği için sürekli güncelliyorum
             y = transform.position.y;  //gamedatadan y çekileceği için sürekli güncelliyorum
            float moveSpeed = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(1, 0) * moveSpeed * 5f;

            if (Input.GetKeyDown(KeyCode.Space)) //bomba atma
            {
                GameObject bomb = Instantiate(bombPrefab);
                bomb.transform.position = transform.position;
                bomb.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1) * 8f;
                Destroy(bomb.gameObject, 2f);
            }

            //oyun her an durdurulabilir bu yüzden sürekli kaydediyorum
            GameData data = new GameData(this);
            SaveLoadManager.Save(path, data);


            float mesafe = Mathf.Abs(transform.position.y - ball.position.y); //top ile player arasındaki mesafeyi aldım
            if (mesafe < 2) //eğer çok yakınsa oyunu baştan başlatacağım
            { 
                data.posy = -5.561102f; //y konumu ilk pozisyon olsun diye(ilk y pozisyonumuz -5.56)
                SaveLoadManager.Save(path, data);
                SceneManager.LoadScene(0); //oyun baştan başlasın
        }
    
    }
    private void Load()
    {
        GameData data = SaveLoadManager.Load(path);
        y = data.posy; //kaydedilen x al
        x = data.posx; //kaydedilen y al
        score = data.score; //kaydedilen score al
    }
    public void Score()
    {
        score++;
        ScoreTxt.text = score.ToString();
        if (hscore < score)
        {
            hscore = score;
            PlayerPrefs.SetInt("hscore", hscore);
        }
        if(score % 5 == 0){
            transform.position = new Vector3(transform.position.x, transform.position.y + 1);
            GameData data = new GameData(this);
            SaveLoadManager.Save(path, data);
        }
       
    }
   
}
