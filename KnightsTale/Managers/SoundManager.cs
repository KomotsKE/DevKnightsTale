namespace KnightsTale.Managers
{
    public class SoundManager
    {
        public SoundItem BackgroundMusic;

        public List<SoundItem> Sounds = new();

        public SoundManager(SoundEffect sound)
        {
            if (sound != null)
            {
                ChangeBackGroundMusic(sound);
            }
        }

        public virtual void AddSound(string name, float volume, SoundEffect sound)
        {
            Sounds.Add(new SoundItem(name, volume, sound));
        }


        public virtual void ChangeBackGroundMusic(SoundEffect sound)
        {
            BackgroundMusic?.SoundInstance.Stop();

            BackgroundMusic = new SoundItem("BackgroundMusic", 0.25f, sound);
            BackgroundMusic.CreateInstance();

            BackgroundMusic.SoundInstance.IsLooped = true;
            BackgroundMusic.SoundInstance.Play();
        }

        public virtual void PlaySound(string name)
        {
            foreach (var sound in Sounds)
            {
                if (sound.Name == name)
                {
                    sound.CreateInstance();
                    sound.SoundInstance.Play();
                }
            }
        }

    }
}
