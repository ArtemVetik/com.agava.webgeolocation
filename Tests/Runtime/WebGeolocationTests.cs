using NUnit.Framework;

namespace Agava.WebGeolocation.Tests
{
    public class WebGeolocationTests
    {
        [Test]
        public void GetCurrentPositionShouldNotThrow()
        {
            Assert.DoesNotThrow(() =>
            {
                WebGeolocation.GetCurrentPosition();
            });
        }

        [Test]
        public void WatchPositionAndClearShouldNotThrow()
        {
            Assert.DoesNotThrow(() =>
            {
                WebGeolocation.WatchPosition(
                    onSuccessCallback: (position) => WebGeolocation.ClearWatch(position.watchId)
                );
            });
        }
    }
}
