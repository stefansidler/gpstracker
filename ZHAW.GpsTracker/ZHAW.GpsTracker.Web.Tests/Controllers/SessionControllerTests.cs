using System.Web.Mvc;
using NUnit.Framework;
using ZHAW.GpsTracker.Web.Controllers;

namespace ZHAW.GpsTracker.Web.Tests.Controllers
{
    [TestFixture]
    public class SessionControllerTests
    {
        [Test]
        public void Index_WithoutSessionKey_ReturnsNotFoundResult()
        {
            var sut = new SessionController();

            ActionResult actual = sut.Index(null);

            Assert.That(actual, Is.TypeOf<HttpNotFoundResult>());
        }

        [Test]
        public void Index_WithEmptySessionKey_ReturnsNotFoundResult()
        {
            var sut = new SessionController();

            ActionResult actual = sut.Index(string.Empty);

            Assert.That(actual, Is.TypeOf<HttpNotFoundResult>());
        }

        [Test]
        public void Index_WithWitespaceSessionKey_ReturnsNotFoundResult()
        {
            var sut = new SessionController();

            ActionResult actual = sut.Index(" ");

            Assert.That(actual, Is.TypeOf<HttpNotFoundResult>());
        }

        [Test]
        public void Index_WithSessionKey_ReturnsViewResult()
        {
            var sut = new SessionController();

            ActionResult actual = sut.Index("asdf");

            Assert.That(actual, Is.TypeOf<ViewResult>());
        }
    }
}