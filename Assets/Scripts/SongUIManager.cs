using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SongUIManager : MonoBehaviour
{
    [SerializeField] private SongLibrary library;
    [SerializeField] private Transform contentParent;
    [SerializeField] private GameObject songItemPrefab;
    [SerializeField] private AudioSource musicSource;

    [SerializeField] private Image Cover;
    [SerializeField] private TMP_Text Title;
    [SerializeField] private TMP_Text Artist;
    [SerializeField] private GameObject currentlyPlaying;

    [SerializeField] private Sprite Play;
    [SerializeField] private Sprite Pause;
    [SerializeField] private Button PlayPause;
    [SerializeField] private Slider Volume;
    [SerializeField] private Slider progressSlider;

    [SerializeField] private GameObject Pc;
    [SerializeField] private GameObject Tv;
    [SerializeField] private GameObject Notes;

    private SongData currentSong;


    private bool isDraggingSlider;
    private bool isUpdatingSlider;
    private bool paused = false;
    private bool isSwitchingSong;

    void Start()
    {
        GenerateList(); 
    }
   
    void GenerateList()
    {
        Debug.Log("Library: " + library);
        Debug.Log("ContentParent: " + contentParent);
        Debug.Log("Prefab: " + songItemPrefab);
        Debug.Log("Songs count: " + library?.songs.Count);
        foreach (SongData song in library.songs)
        {
            GameObject item = Instantiate(songItemPrefab, contentParent);

            item.transform.Find("Title").GetComponent<TMP_Text>().text = song.title;
            item.transform.Find("Cover").GetComponent<Image>().sprite = song.cover;
            item.transform.Find("Artist").GetComponent<TMP_Text>().text = song.artist;

            Button button = item.GetComponentInChildren<Button>();
            button.onClick.AddListener(() => PlaySong(song));
        }
    }

    void PlaySong(SongData song)
    {
        if (song == null || song.clip == null) return;

        isSwitchingSong = true;
        PlayPause.image.sprite = Pause;
        if (currentlyPlaying.activeInHierarchy == false)
        {
            currentlyPlaying.SetActive(true);
        }
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }

        progressSlider.minValue = 0;
        progressSlider.maxValue = 1;
        progressSlider.value = 0;

        musicSource.clip = song.clip;
        musicSource.Play();

        Cover.transform.GetComponent<Image>().sprite = song.cover;
        Title.text = song.title;
        Artist.text = song.artist;
        paused = false;
        Notes.SetActive(true);

        currentSong = song;
        isSwitchingSong = false;
        Debug.Log("Playing: " + song.title);
    }

    public void SkipSong()
    {
        if (library.songs.Count == 0) return;
        if (isSwitchingSong) return;

        isSwitchingSong = true;

        int currentIndex = library.songs.IndexOf(currentSong);
        currentIndex++;

        if (currentIndex >= library.songs.Count)
            currentIndex = 0;

        PlaySong(library.songs[currentIndex]);

        isSwitchingSong = false;
    }

    public void ReturnSong()
    {
        int currentIndex = library.songs.IndexOf(currentSong);

        if (currentIndex > 0)
        {
            PlaySong(library.songs[currentIndex - 1]);
        }
    }

    public void TogglePause()
    {
        if (paused)
        {
            musicSource.UnPause();
            Notes.SetActive(false);
            paused = false;
            PlayPause.image.sprite = Pause;
        }
        else
        {
            musicSource.Pause();
            Notes.SetActive(false);
            paused = true;
            PlayPause.image.sprite = Play;
        }
    }

    public void ChangeVolume()
    {
        musicSource.volume = Volume.value;
    }

    public void ReturnToRoom()
    {
        Pc.GetComponent<BoxCollider2D>().enabled = true;
        Tv.GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<Canvas>().enabled = false;
    }


    public void OnSliderChanged(float value)
    {
        if (musicSource.clip == null) return;
        if (isUpdatingSlider) return;

        musicSource.time = value * musicSource.clip.length;
    }

    void Update()
    {
        if (musicSource.clip == null) return;

        isUpdatingSlider = true;

        if (!isDraggingSlider)
        {
            progressSlider.value = musicSource.time / musicSource.clip.length;
        }

        isUpdatingSlider = false;

        if (!isSwitchingSong && !paused && musicSource.clip != null && !musicSource.isPlaying && musicSource.time > 0.1f)
        {
            SkipSong();
        }
    }

    public void BeginDrag()
    {
        isDraggingSlider = true;
    }

    public void EndDrag()
    {
        isDraggingSlider = false;
        OnSliderChanged(progressSlider.value); // apply final seek
    }
}