using System.Collections;
using System.Collections.Generic;
using Info;
using UnityEngine;

public class MusicManager : BaseMonoBehaviour {
    //背景音乐播放
    private AudioSource BGAudioSource;
    

    private Dictionary<MusicType, AudioClip> musicDic;
    //用于判断保证音乐只创建一次
    private static bool origional = true;

    public static MusicManager instance;
    
    protected override void Awake() {
        base.Awake();
        if (origional) {
            origional = false;
            instance = this;
            
            BGAudioSource = GetComponent<AudioSource>();
            //加载要播放的音乐
            musicDic = new Dictionary<MusicType, AudioClip>();
            musicDic.Add(MusicType.BG,Resources.Load<AudioClip>("Music/未闻花名"));
            musicDic.Add(MusicType.ButtonClick1,Resources.Load<AudioClip>("Music/ButtonClick1"));
            musicDic.Add(MusicType.ButtonClick2,Resources.Load<AudioClip>("Music/ButtonClick2"));
            musicDic.Add(MusicType.ButtonClick3,Resources.Load<AudioClip>("Music/ButtonClick3"));
            musicDic.Add(MusicType.SceneLoad,Resources.Load<AudioClip>("Music/SceneLoad"));
            BGAudioSource.clip = musicDic[MusicType.BG];
            BGAudioSource.Play();
            BGAudioSource.loop = true;
            
            DontDestroyOnLoad(this.gameObject);
        }else {
            //如果不是最初创建的那个就直接销毁
            Destroy(this.gameObject);
            // instance = null;
        }

        
    }

    protected override void FetchComponent() {
        
        // AudioClip audioClip = Resources.Load<AudioClip>("Music/未闻花名");
        // audioSource.clip = audioClip;
        // if (audioSource!=null&&!audioSource.isPlaying)
        // {
        //     audioSource.Play();
        // }
    }

    protected override void GetResources() {
        
        
    }

    /// <summary>
    /// 获得音乐
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public AudioClip GetAudioClip(MusicType type) {
        if (musicDic.ContainsKey(type)) {
            return musicDic[type];
        }

        return null;
    }


    public void PlayAudioClip(MusicType type) {
        if (musicDic.ContainsKey(type)) {
            AudioSource audio = gameObject.AddComponent<AudioSource>();
            audio.clip = musicDic[type];
            audio.Play();
        }
    }
  
    protected override void InitZoneLayout() {
        
    }
    
    
}
