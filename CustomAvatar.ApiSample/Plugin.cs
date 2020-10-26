using IPA;
using SiraUtil.Zenject;

namespace CustomAvatar.ApiSample
{
    [Plugin(RuntimeOptions.DynamicInit)]
    internal class Plugin
    {
        [Init]
        public Plugin(Zenjector zenjector)
        {
            zenjector.OnApp<SampleInstaller>();
        }
    }
}
