using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class DynamicWeather : MonoBehaviour {

    public Transform _Player; //player gameobject transform
    private Transform _Weather; //Weather gameobject transform
    public float WeatherHeight = 15.0f; //defines height from ground for weather bject.
        
    //creates slot in inspector to asign particle system
    public ParticleSystem SunCloudsParticleSystem;
    public ParticleSystem StormCloudParticleSystem;
    public ParticleSystem ThunderParticleSystem;
    public ParticleSystem FogParticleSystem;
    public ParticleSystem OvercastCloudsParticleSystem;
    public ParticleSystem RainParticleSystem;
    //-----------------------------------------

    
    //defines naming convention for weather emisiion modules
    private ParticleSystem.EmissionModule SunCloudsEmission;
    private ParticleSystem.EmissionModule StormCloudEmission;
    private ParticleSystem.EmissionModule ThunderEmission;
    private ParticleSystem.EmissionModule FogEmission;
    private ParticleSystem.EmissionModule OvercastEmission;
    private ParticleSystem.EmissionModule RainEmission;
    //------------------------------------------------------
    

    public float SwitchWeatherTimer = 0.0f;//switch weather timer = 0
    public float ResetWeatherTimer = 20.0f;//defines value to reset weather timer

    public float AudioFadeTime = 0.25f; //defines rate for fading audio
    
    //creates slot in inspector to asign audio
    public AudioClip SunnyAudio;
    public AudioClip ThunderStormAudio;
    public AudioClip FogAudio;
    public AudioClip OvercastAudio;
    public AudioClip RainAudio;
    //----------------------------------------

    public float LightDimTime = 0.10f; //defines rate for dimming light
    
    //defines light intensity for different states
    public float ThunderStormIntensity = 0.0f;
    public float SunnyIntensity = 1.0f;
    public float FogIntensity = 0.5f;
    public float OvercastIntensity = 0.25f;
    public float RainIntensity = 0.25f;
    //---------------------------------------------

    public WeatherStates CurrentWeatherState; //defines naming convention for weather state
    private int _SwitchWeather; //defines naming convention of switch range
    
    public enum WeatherStates //defines all weather states
    {
        PickWeatherState,
        Sunny,
        Thunder,
        Fog,
        Overcast,
        Rain
    }


	// Use this for initialization
	void Start () {

        GameObject _PlayerGameObject = GameObject.FindGameObjectWithTag("Moa");
        _Player = _PlayerGameObject.transform; //caches players position

        GameObject _WeatherGameObject = GameObject.FindGameObjectWithTag("Weather");
        _Weather = _WeatherGameObject.transform; //caches weather position

        //set emission module to particle systems
        SunCloudsEmission = SunCloudsParticleSystem.emission;
        StormCloudEmission = StormCloudParticleSystem.emission;
        ThunderEmission = ThunderParticleSystem.emission;
        FogEmission = FogParticleSystem.emission;
        OvercastEmission = OvercastCloudsParticleSystem.emission;
        RainEmission = RainParticleSystem.emission;      
        //--------------------------------------------------------

        StartCoroutine(WeatherFSM()); //start finite state machine
	}
	
	// Update is called once per frame
	void Update () {
        WeatherTimer();

        _Weather.transform.position = new Vector3(_Player.position.x, _Player.position.y + WeatherHeight, _Player.position.z); //weather equals players position + WeatherHeight for players Y position
	}

    void WeatherTimer()
    {
        
        SwitchWeatherTimer -= Time.deltaTime; //decrease timer

        if(SwitchWeatherTimer < 0) //if SwitchWeatherTimer is less than 0, set back to 0
        {
            SwitchWeatherTimer = 0;
        }
        if(SwitchWeatherTimer > 0)//if SwitchWeatherTimer is greater than 0, do nothing
        {
            return;
        }

        if(SwitchWeatherTimer == 0)//if SwitchWeatherTiemer is = 0, set CurrentWeatherState to PickWeatherState to change the weather
        {
            CurrentWeatherState = DynamicWeather.WeatherStates.PickWeatherState;
        }

        SwitchWeatherTimer = ResetWeatherTimer;
    }

    IEnumerator WeatherFSM()//weather finite state machine
    {
        while(true)//while weather state is active 
        {
            switch (CurrentWeatherState)//switch weather states
            {
                case WeatherStates.PickWeatherState:
                    PickWeatherState();
                    break;
                case WeatherStates.Sunny:
                    Sunny();
                    break;
                case WeatherStates.Thunder:
                    Thunder();
                    break;
                case WeatherStates.Fog:
                    Fog();
                    break;
                case WeatherStates.Overcast:
                    Overcast();
                    break;
                case WeatherStates.Rain:
                    Rain();
                    break;
            }
            yield return null;
        }
    }

    void PickWeatherState()
    {
        //disable particle systems        
        SunCloudsEmission.enabled = false;
        StormCloudEmission.enabled = false;
        ThunderEmission.enabled = false;
        FogEmission.enabled = false;
        OvercastEmission.enabled = false;
        RainEmission.enabled = false;

        _SwitchWeather = Random.Range(0, 5);//_SwitchWeather is = to random range between 0,5     

        switch (_SwitchWeather)
        {
            case 0:
                CurrentWeatherState = DynamicWeather.WeatherStates.Sunny;
                break;
            case 1:
                CurrentWeatherState = DynamicWeather.WeatherStates.Thunder;
                break;
            case 2:
                CurrentWeatherState = DynamicWeather.WeatherStates.Fog;
                break;
            case 3:
                CurrentWeatherState = DynamicWeather.WeatherStates.Overcast;
                break;
            case 4:
                CurrentWeatherState = DynamicWeather.WeatherStates.Rain;
                break;
        }

    }

    void Sunny()
    {
        Debug.Log("Sunny");
        SunCloudsEmission.enabled = true;

        //if light intensity is greater than SunnyIntensity, decrease by time.deltatime * LightDimTime
        if(GetComponent<Light>().intensity > SunnyIntensity) 
        {
            GetComponent<Light>().intensity -= Time.deltaTime * LightDimTime;
        }
        //if light intensity is less than SunnyIntensity, increase by time.deltatime * LightDimTime
        if (GetComponent<Light>().intensity < SunnyIntensity)
        {
            GetComponent<Light>().intensity += Time.deltaTime * LightDimTime;
        }
        
        //if volume is greater than 0 and audio doesn't equal current weather audio, start reducing volume by AudioFadeTime
        if(GetComponent<AudioSource>().volume > 0 && GetComponent<AudioSource>().clip != SunnyAudio)
        {
            GetComponent<AudioSource>().volume -= Time.deltaTime * AudioFadeTime;
        }
        
        //if audio form previous weather state = 0
        if (GetComponent<AudioSource>().volume == 0)
        {
            GetComponent<AudioSource>().Stop(); //stop playing audio
            GetComponent<AudioSource>().clip = SunnyAudio;//set audio to sunny
            GetComponent<AudioSource>().loop = true; //set to loop
            GetComponent<AudioSource>().Play(); //start playing
        }

        //if volume is less than 1 and audio equals current weather audio, start increasing volume by AudioFadeTime
        if (GetComponent<AudioSource>().volume < 1 && GetComponent<AudioSource>().clip == SunnyAudio)
        {
            GetComponent<AudioSource>().volume += Time.deltaTime * AudioFadeTime;
        }
    }

    void Thunder()
    {
        Debug.Log("Thunder");
        StormCloudEmission.enabled = true;
        ThunderEmission.enabled = true;
        RainEmission.enabled = true;

        //if light intensity is greater than ThunderStormIntensity, decrease by time.deltatime * LightDimTime
        if (GetComponent<Light>().intensity > ThunderStormIntensity)
        {
            GetComponent<Light>().intensity -= Time.deltaTime * LightDimTime;
        }
        //if light intensity is less than ThunderStormIntensity, increase by time.deltatime * LightDimTime
        if (GetComponent<Light>().intensity < ThunderStormIntensity)
        {
            GetComponent<Light>().intensity += Time.deltaTime * LightDimTime;
        }

        //if volume is greater than 0 and audio doesn't equal current weather audio, start reducing volume by AudioFadeTime
        if (GetComponent<AudioSource>().volume > 0 && GetComponent<AudioSource>().clip != ThunderStormAudio)
        {
            GetComponent<AudioSource>().volume -= Time.deltaTime * AudioFadeTime;
        }

        //if audio form previous weather state = 0
        if (GetComponent<AudioSource>().volume == 0)
        {
            GetComponent<AudioSource>().Stop(); //stop playing audio
            GetComponent<AudioSource>().clip = ThunderStormAudio;//set audio to thunderstorm
            GetComponent<AudioSource>().loop = true; //set to loop
            GetComponent<AudioSource>().Play(); //start playing
        }

        //if volume is less than 1 and audio equals current weather audio, start increasing volume by AudioFadeTime
        if (GetComponent<AudioSource>().volume < 1 && GetComponent<AudioSource>().clip == ThunderStormAudio)
        {
            GetComponent<AudioSource>().volume += Time.deltaTime * AudioFadeTime;
        }
    }

    void Fog()
    {
        Debug.Log("Fog");
        FogEmission.enabled = true;
        _Weather.transform.position = new Vector3(_Player.position.x, _Player.position.y, _Player.position.z);

        //if light intensity is greater than FogIntensity, decrease by time.deltatime * LightDimTime
        if (GetComponent<Light>().intensity > FogIntensity)
        {
            GetComponent<Light>().intensity -= Time.deltaTime * LightDimTime;
        }
        //if light intensity is less than FogIntensity, increase by time.deltatime * LightDimTime
        if (GetComponent<Light>().intensity < FogIntensity)
        {
            GetComponent<Light>().intensity += Time.deltaTime * LightDimTime;
        }

        //if volume is greater than 0 and audio doesn't equal current weather audio, start reducing volume by AudioFadeTime
        if (GetComponent<AudioSource>().volume > 0 && GetComponent<AudioSource>().clip != FogAudio)
        {
            GetComponent<AudioSource>().volume -= Time.deltaTime * AudioFadeTime;
        }

        //if audio form previous weather state = 0
        if (GetComponent<AudioSource>().volume == 0) 
        {
            GetComponent<AudioSource>().Stop(); //stop playing audio
            GetComponent<AudioSource>().clip = FogAudio;//set audio to fog
            GetComponent<AudioSource>().loop = true; //set to loop
            GetComponent<AudioSource>().Play(); //start playing
        }

        //if volume is less than 1 and audio equals current weather audio, start increasing volume by AudioFadeTime
        if (GetComponent<AudioSource>().volume < 1 && GetComponent<AudioSource>().clip == FogAudio)
        {
            GetComponent<AudioSource>().volume += Time.deltaTime * AudioFadeTime;
        }
    }

    void Overcast()
    {
        Debug.Log("Overcast");
        OvercastEmission.enabled = true;

        //if light intensity is greater than OvercastIntensity, decrease by time.deltatime * LightDimTime
        if (GetComponent<Light>().intensity > OvercastIntensity)
        {
            GetComponent<Light>().intensity -= Time.deltaTime * LightDimTime;
        }
        //if light intensity is less than OvercastIntensity, increase by time.deltatime * LightDimTime
        if (GetComponent<Light>().intensity < OvercastIntensity)
        {
            GetComponent<Light>().intensity += Time.deltaTime * LightDimTime;
        }

        //if volume is greater than 0 and audio doesn't equal current weather audio, start reducing volume by AudioFadeTime
        if (GetComponent<AudioSource>().volume > 0 && GetComponent<AudioSource>().clip != OvercastAudio)
        {
            GetComponent<AudioSource>().volume -= Time.deltaTime * AudioFadeTime;
        }

        //if audio form previous weather state = 0
        if (GetComponent<AudioSource>().volume == 0)
        {
            GetComponent<AudioSource>().Stop(); //stop playing audio
            GetComponent<AudioSource>().clip = OvercastAudio;//set audio to overcast
            GetComponent<AudioSource>().loop = true; //set to loop
            GetComponent<AudioSource>().Play(); //start playing
        }

        //if volume is less than 1 and audio equals current weather audio, start increasing volume by AudioFadeTime
        if (GetComponent<AudioSource>().volume < 1 && GetComponent<AudioSource>().clip == OvercastAudio)
        {
            GetComponent<AudioSource>().volume += Time.deltaTime * AudioFadeTime;
        }
    }

    void Rain()
    {
        Debug.Log("Rain");
        RainEmission.enabled = true;
        OvercastEmission.enabled = true;

        //if light intensity is greater than RainIntensity, decrease by time.deltatime * LightDimTime
        if (GetComponent<Light>().intensity > RainIntensity)
        {
            GetComponent<Light>().intensity -= Time.deltaTime * LightDimTime;
        }
        //if light intensity is less than RainIntensity, increase by time.deltatime * LightDimTime
        if (GetComponent<Light>().intensity < RainIntensity)
        {
            GetComponent<Light>().intensity += Time.deltaTime * LightDimTime;
        }

        //if volume is greater than 0 and audio doesn't equal current weather audio, start reducing volume by AudioFadeTime
        if (GetComponent<AudioSource>().volume > 0 && GetComponent<AudioSource>().clip != RainAudio)
        {
            GetComponent<AudioSource>().volume -= Time.deltaTime * AudioFadeTime;
        }

        //if audio form previous weather state = 0
        if (GetComponent<AudioSource>().volume == 0)
        {
            GetComponent<AudioSource>().Stop(); //stop playing audio
            GetComponent<AudioSource>().clip = RainAudio;//set audio to Rain
            GetComponent<AudioSource>().loop = true; //set to loop
            GetComponent<AudioSource>().Play(); //start playing
        }

        //if volume is less than 1 and audio equals current weather audio, start increasing volume by AudioFadeTime
        if (GetComponent<AudioSource>().volume < 1 && GetComponent<AudioSource>().clip == RainAudio)
        {
            GetComponent<AudioSource>().volume += Time.deltaTime * AudioFadeTime;
        }
    }
}
