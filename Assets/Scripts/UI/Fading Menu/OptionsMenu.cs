using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Curveball;

namespace LastStand
{
    /// <summary>
    /// Default options menu implementation, doing the typical configuration required in an
    /// options menu.
    /// </summary>
    public class OptionsMenu : FadingMenu
    {
        /// <summary>
        /// Resolution select dropdown.
        /// </summary>
        public TextMeshProDropdown ResolutionSelectDropdown;

        /// <summary>
        /// VSync toggle.
        /// </summary>
        public Toggle VSyncToggle;

        /// <summary>
        /// Fullscreen toggle.
        /// </summary>
        public Toggle FullscreenToggle;

        /// <summary>
        /// Music slider.
        /// </summary>
        public Slider MusicSlider;

        /// <summary>
        /// Sound effects slider.
        /// </summary>
        public Slider SoundEffectsSlider;

        /// <summary>
        /// Unity's Awake handler.
        /// </summary>
        protected virtual void Awake()
        {
            SetupResolutionSelect();
            SetupVSyncToggle();
            SetupFullscreenToggle();
            SetupMusicSlider();
            SetupSoundEffectSlider();
        }

        public override void Open(bool instant)
        {
            base.Open(instant);
        }

        public override void Close(bool instant, MenuScreen toOpen = null)
        {
            base.Close(instant, toOpen);
            EventSystem.Publish(new PersistPlayerSettingsEvent());
        }

        /// <summary>
        /// Sets up the resolution dropdown, populating options and binding callbacks.
        /// </summary>
        protected virtual void SetupResolutionSelect()
        {
            ResolutionSelectDropdown.ClearOptions();

            List<string> options = new List<string>();

            int currOpt = 0;
            int i = 0;
            int startIndex = 0;

            Resolution currentResolution = Screen.currentResolution;

            foreach (Resolution resolution in Screen.resolutions)
            {
                if (resolution.width < 800)
                {
                    startIndex++;
                    continue;
                }

                options.Add(resolution.width + "x" + resolution.height + " @ " + resolution.refreshRate + "hz");

                if (currentResolution.width == resolution.width && currentResolution.height == resolution.height
                    && currentResolution.refreshRate == resolution.refreshRate)
                {
                    currOpt = i;
                }

                i++;
            }

            ResolutionSelectDropdown.AddOptions(options);
            ResolutionSelectDropdown.value = currOpt;

            options = null;

            ResolutionSelectDropdown.onValueChanged.AddListener((int index) =>
            {
                Resolution selectedResolution = Screen.resolutions[startIndex + index];
                Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
            });
        }

        /// <summary>
        /// Sets up the vertical sync toggle, binding callbacks.
        /// </summary>
        protected virtual void SetupVSyncToggle()
        {
            VSyncToggle.isOn = PlayerPrefsController.VSyncEnabled;

            VSyncToggle.onValueChanged.AddListener((bool newVal) =>
            {
                EventSystem.Publish(new VSyncChangedEvent(newVal));
            });
        }

        /// <summary>
        /// Sets up the fullscreen toggle binding callbacks.
        /// </summary>
        protected virtual void SetupFullscreenToggle()
        {
            FullscreenToggle.isOn = Screen.fullScreen;

            FullscreenToggle.onValueChanged.AddListener((bool value) =>
            {
                Resolution selectedResolution = Screen.currentResolution;
                Screen.SetResolution(selectedResolution.width, selectedResolution.height, value, selectedResolution.refreshRate);
            });
        }

        /// <summary>
        /// Sets up the music slider, binding callbacks and events.
        /// </summary>
        protected virtual void SetupMusicSlider()
        {
            MusicSlider.value = PlayerPrefsController.MusicVolume;

            MusicSlider.onValueChanged.AddListener((float volume) =>
            {
                EventSystem.Publish(new MusicVolumeChangedEvent(volume));
            });
        }

        /// <summary>
        /// Sets up the sound effect slider, binding callbacks and events.
        /// </summary>
        protected virtual void SetupSoundEffectSlider()
        {
            SoundEffectsSlider.value = PlayerPrefsController.SoundVolume;

            SoundEffectsSlider.onValueChanged.AddListener((float volume) =>
            {
                EventSystem.Publish(new SoundVolumeChangedEvent(volume));
            });
        }
    }
}