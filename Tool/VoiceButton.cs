using System;
using UnityEngine;
using System.Collections;
/// <summary>
/// 作者：鞠明明
/// 时间：2016.1.6
/// 用途：用于录音发送声音给网络中的其他人
/// </summary>
public class VoiceButton : MonoBehaviour
{
    private AudioClip audioClip;
    //通常的无损音质的采样率是44100 即每秒音频用44100个float数据表示，但是语音只需8000（通常移动电话是8000）就够了
    //不然音频数据太大，不利于传输和存储
    private int samplingRate = 8000;
    public UILabel messageLabel;
    public NetworkView nv;
    public AudioSource audioSource;


    void Awake()
    {
        nv = this.GetComponent<NetworkView>();
    }


    public void OnPress(bool isPress)
    {
        if (isPress)//点击
        {
            messageLabel.text = "";
            Microphone.End(null);//关闭录音.
            audioClip = Microphone.Start(null, false, 10, samplingRate);//开始录音
        }
        else//弹起按钮
        {
            int audioLength;//录音的长度，单位为秒，ui上可能需要显示
            int lastPos = Microphone.GetPosition(null);
            if (Microphone.IsRecording(null))
            {
                audioLength = lastPos / samplingRate;
            }
            else
            {
                audioLength = 10;
            }

            Microphone.End(null);//此时录音结束，audioClip可以播放

            if (audioLength < 1.0f)
            {
                messageLabel.text = "说话时间太短";
                return;
            } //录音小于1秒不处理

            //将声音转成float数组
            float[] samples = new float[audioLength * samplingRate];
            audioClip.GetData(samples, 0);
            int i = 0;
            while (i < samples.Length)
            {
                samples[i] = samples[i] * 0.5F;
                ++i;
            }

            //实例化M_AudioClip类 传入声音采样率 声道 音频 和float数组
            M_AudioClip mac = new M_AudioClip(audioLength * samplingRate, audioClip.frequency, samples);

            //向rpc发送M_AudioClip序列化后的byte数组
            nv.RPC("ReceiveAudio", RPCMode.Others, FormatterHelper.Serialize(mac));
        }

    }

    IEnumerator WaitForEnd()
    {
        yield return new WaitForSeconds(1f);
        messageLabel.text = "";
    }

    [Serializable]
    public class M_AudioClip
    {
        //采样率
        public int length;
        //音频
        public int frequency;
        //声音float数组
        public float[] audioData;

        public M_AudioClip(int len, int freq, float[] data)
        {
            this.length = len;
            this.frequency = freq;
            this.audioData = data;
        }
    }

    [RPC]
    private void ReceiveAudio(byte[] bytes)
    {
        //反序列化
        M_AudioClip mac = (M_AudioClip)FormatterHelper.Deserialize(bytes);
        //生成新的声音片段
        AudioClip ac = AudioClip.Create("s", mac.length, 1, mac.frequency, false);
        //float数组转audioclip
        ac.SetData(mac.audioData, 0);
        //播放
        audioSource.clip = ac;
        audioSource.Play();
    }
}
