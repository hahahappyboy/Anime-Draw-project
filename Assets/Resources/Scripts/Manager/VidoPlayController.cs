using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// 播放档案对应Video的控制器
/// </summary>
public class VidoPlayController : BaseMonoBehaviour {
    # region 组件
    private VideoPlayer _videoPlayer;
    private RawImage _rawImage;
    # endregion
    
    # region 属性
    //Video播放的信息类
    class VideoClipInfo {
        //Video播放的片段
        public VideoClip _videoClip;
        //Vido播放的时间
        public double _videoPlayTime;
        public VideoClipInfo(VideoClip clip, float time) {
            _videoClip = clip;
            _videoPlayTime = time;
        }
    }
    //存放video的url和该url已经播放的时间    
    private Dictionary<string,VideoClipInfo> videoUrlAndTimeDictionary;
    //目前播放的video
    private VideoClipInfo currentPlayVideo;
    //实例
    public static VidoPlayController instance;
    # endregion
    
    # region 生命周期
    protected override void Awake() {
        base.Awake();
        instance = this;
    }
    protected override void Start() {
        base.Start();
    }
    # endregion

    

    /// <summary>
    /// 获取组件
    /// </summary>
    protected override void FetchComponent() {
        _videoPlayer = this.GetComponent<VideoPlayer>();
        _rawImage = this.GetComponent<RawImage>();
       
    }
    //获取资源
    protected override void GetResources() {
        videoUrlAndTimeDictionary = new Dictionary<string, VideoClipInfo>();
    }
    //初始化界面
    protected override void InitZoneLayout() {
        _videoPlayer.isLooping = true;
        _videoPlayer.audioOutputMode = VideoAudioOutputMode.None;
        _videoPlayer.clip = null;
        _rawImage.enabled = false;
    }
    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="videoURL">播放动画的名字</param>
    public void PlayVideo(string videoURL) {
    
        
        if (!videoUrlAndTimeDictionary.ContainsKey(videoURL)) {//之前没播放过这个viode
            VideoClip videoClip = Resources.Load<VideoClip>(videoURL);
            //默认从第零秒开始播放
            currentPlayVideo = new VideoClipInfo(videoClip, 0f);
            videoUrlAndTimeDictionary.Add(videoURL,currentPlayVideo);
        } else {
            currentPlayVideo = videoUrlAndTimeDictionary[videoURL];
        }
        //设置播放片段
        _videoPlayer.clip = currentPlayVideo._videoClip;
        //设置播放时间
        _videoPlayer.time = currentPlayVideo._videoPlayTime;
        _rawImage.enabled = true;
        
        //释放视频缓存，为了防止切换视频第一帧是上个视频的结束帧
        _videoPlayer.targetTexture.Release();
        _videoPlayer.targetTexture.MarkRestoreExpected();
        //播放
        _videoPlayer.Play();
    }
    /// <summary>
    /// 暂停目前视频的播放
    /// </summary>
    public void PauseCurrentVideoPlay() {
        _videoPlayer.Pause();
        currentPlayVideo._videoPlayTime = _videoPlayer.time;
       
        //关闭显示
        _rawImage.enabled = false;
    }


}
