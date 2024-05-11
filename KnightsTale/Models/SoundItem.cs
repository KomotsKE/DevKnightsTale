namespace KnightsTale.Models
{
    public class SoundItem
    {
        public float Volume;
        public string Name;
        public SoundEffect Sound;
        public SoundEffectInstance SoundInstance;

        public SoundItem(string name, float volume, SoundEffect sound)
        {
            Name = name;
            Volume = volume;
            Sound = sound;
            CreateInstance();
        }


        public virtual void CreateInstance()
        {
            SoundInstance = Sound.CreateInstance();
            SoundInstance.Volume = Volume;
        }
    }
}
