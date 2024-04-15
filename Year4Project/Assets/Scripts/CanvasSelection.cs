using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CanvasSelection : MonoBehaviour
{
    public GameManager man;
    [System.Serializable]
    public class Meta //contains all relevant information regarding retrieval of json file
    {
        public string analyzer_version;
        public string platform;
        public string detailed_status;
        public int status_code;
        public int timestamp;
        public double analysis_time;
        public string input_process;
    }
    public class Track // https://stackoverflow.com/questions/45215320/how-to-take-a-particular-field-from-json-data-in-unity
    {
        public int num_samples;
        public double duration;
        public string sample_md5;
        public int offset_seconds;
        public int window_seconds;
        public int analysis_sample_rate;
        public int analysis_channels;
        public double end_of_fade_in;
        public double starta_of_fade_out;
        public float loudness;
        public float tempo;
        public double tempo_confidence;
        public int time_signature;
        public double time_signature_confidence;
        public int key;
        public double key_confidence;
        public int mode;
        public double mode_confidence;
        public string codestring;
        public double code_version;
        public string echoprintstring;
        public double echoprint_version;
        public string synchstring;
        public double synch_version;
        public string rhythmstring;
        public double rhythm_version;
    }
    public class MusicData
    {
        public List<Meta> m;
        public List<Track> t;
    }
    private Track m = new Track();
    // Start is called before the first frame update
    void Start()
    {
        man = GameManager.Instance;
        Debug.Log(man);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    double HealthSetter(double d)
    {
        double settingHealth = d / 60; //duration is in seconds
        return Math.Round(settingHealth);
    }
    public void MusicLoader(TextAsset pathName)
    {
        m = JsonUtility.FromJson<Track>(pathName.text);
        PlayerController.score = 0;
        Debug.Log(m.loudness);
        man.songBpm = m.tempo;
        man.duration = (float)m.duration;
        man.setTimer();
        man.setMargin(m.tempo);
        man.counter = 0;
        double health = HealthSetter(3);
        PlayerHealth.setHealth(3);
        WeaponController.durability = 5;
        WeaponController.currentWeapon = "Baton";
        SceneManager.LoadScene("GameScene");
    }
    public void ReloadMenu()
    {
        SceneManager.LoadScene("MusicSelection");
    }
    public void AudioLoader(AudioClip a)
    {
        man.music.clip = a;
        man.startMusic();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
